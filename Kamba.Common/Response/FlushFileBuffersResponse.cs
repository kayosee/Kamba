using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class FlushFileBuffersResponse : FileResponse
    {
        public FlushFileBuffersResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FlushFileBuffersResponse, clientId, requestId, fileName, info)
        {
        }
        public FlushFileBuffersResponse(FlushFileBuffersRequest request) : base(DataType.FlushFileBuffersResponse, request) { }
        public FlushFileBuffersResponse(ByteArrayStream stream) : base(stream) { }
    }
}