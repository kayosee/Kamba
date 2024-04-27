using DokanNet;

namespace Kamba.Common.Request
{
    public class UnmountedRequest : SessionRequest
    {
        private IDokanFileInfo info;

        public UnmountedRequest(int clientId, long requestId, IDokanFileInfo info):base(DataType.UnmountedRequest,clientId,requestId)
        {
            this.info = info;
        }
        public UnmountedRequest(ByteArrayStream stream) : base(stream)
        {
        }
    }
}