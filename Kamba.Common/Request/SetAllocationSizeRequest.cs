using DokanNet;

namespace Kamba.Common.Request
{
    public class SetAllocationSizeRequest : FileRequest
    {
        private long length;
        public SetAllocationSizeRequest(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetAllocationSizeRequest, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetAllocationSizeRequest(ByteArrayStream stream) : base(stream)
        {
            length = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(length);
            return stream;
        }
    }
}