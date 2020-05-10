using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcTest.Models
{
    public class GoogleDriveFileInfo
    {
        public string FileName { get; set; }

        public byte[] Content { get; set; }

        public string Id { get; set; }
    }
}
