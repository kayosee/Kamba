using DokanNet;

namespace Kamba.Common.Request
{
    public class UnmountedRequest : FileRequest
    {
        public UnmountedRequest(int clientId, long requestId, IDokanFileInfo info):base(DataType.UnmountedRequest,clientId,requestId,"",info)
        {
        }
        public UnmountedRequest(ByteArrayStream stream) : base(stream)
        {
        }
    }
}