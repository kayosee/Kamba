using DokanNet;

namespace Kamba.Common.Request
{
    public class GetDiskFreeSpaceRequest : SessionRequest
    {
        private IDokanFileInfo info;

        public GetDiskFreeSpaceRequest(int clientId, long requestId, IDokanFileInfo info) : base(DataType.GetDiskFreeSpaceRequest, clientId, requestId)
        {
            this.info = info;
        }
        public GetDiskFreeSpaceRequest(ByteArrayStream stream) : base(stream)
        {

        }
        protected override ByteArrayStream GetStream()
        {
            return base.GetStream();
        }
    }
}