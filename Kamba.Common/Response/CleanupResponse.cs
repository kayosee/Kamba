using DokanNet;
using Kamba.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common.Response
{
    public class CleanupResponse : FileResponse
    {
        public CleanupResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.CleanupResponse, clientId, requestId, fileName, info)
        {
        }
        public CleanupResponse(CleanupRequest request):base(DataType.CleanupResponse, request) { }
        public CleanupResponse(ByteArrayStream stream):base(stream) { }
    }
}
