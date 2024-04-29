using DokanNet;

namespace Kamba.Common.Request
{
    public class GetDiskFreeSpaceRequest : FileRequest
    {
        public GetDiskFreeSpaceRequest(int clientId, long requestId, IDokanFileInfo info) : base(DataType.GetDiskFreeSpaceRequest, clientId, requestId, "", info)
        {
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