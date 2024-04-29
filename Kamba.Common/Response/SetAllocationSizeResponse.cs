using DokanNet;

namespace Kamba.Common.Response
{
    public class SetAllocationSizeResponse : FileResponse
    {
        private long length;
        public SetAllocationSizeResponse(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetAllocationSizeResponse, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetAllocationSizeResponse(ByteArrayStream stream) : base(stream)
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