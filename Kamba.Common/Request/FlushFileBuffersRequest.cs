using DokanNet;

namespace Kamba.Common.Request
{
    public class FlushFileBuffersRequest : FileRequest
    {
        public FlushFileBuffersRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FlushFileBuffersRequest, clientId, requestId, fileName, info)
        {
        }
        public FlushFileBuffersRequest(ByteArrayStream stream) : base(stream) { }
    }
}