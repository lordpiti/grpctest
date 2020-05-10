using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using GrpcTest.GoogleDrive;
using Microsoft.Extensions.Logging;

namespace GrpcTest
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly TestGoogleDrive _testGoogleDrive;

        public GreeterService(ILogger<GreeterService> logger, TestGoogleDrive testGoogleDrive)
        {
            _logger = logger;
            _testGoogleDrive = testGoogleDrive;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var listString = string.Join(",",request.List.ToArray());

            var bubu = _testGoogleDrive.DownloadFile("");


            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " " + request.Surname +": "+ listString,
                Content = ByteString.CopyFrom(bubu.ToArray())
            });
        }

        public override Task<DownloadFileResponse> DownloadFile(DownloadFileRequest request, ServerCallContext context)
        {
            try
            {
                var bubu = _testGoogleDrive.DownloadFile(request.FileId);


                return Task.FromResult(new DownloadFileResponse
                {
                    Content = ByteString.CopyFrom(bubu.ToArray())
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override Task<FileListResponse> GetFileList(FileListRequest request, ServerCallContext context)
        {
            var bubu = _testGoogleDrive.GetFileList();

            var fileListResponse = new FileListResponse();

            foreach (var item in bubu)
            {
                fileListResponse.FileList.Add(new FileData() { FileName = item.Name, Id = item.Id });
            }

            return Task.FromResult(fileListResponse);
        }
    }
}
