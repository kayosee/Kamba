using DokanNet;

namespace Kamba.Common.Response
{
    public class FlushFileBuffersResponse : FileResponse
    {
        public FlushFileBuffersResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FlushFileBuffersResponse, clientId, requestId, fileName, info)
        {
        }
        public FlushFileBuffersResponse(ByteArrayStream stream) : base(stream) { }
    }
}