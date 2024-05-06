using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class SetAllocationSizeResponse : FileResponse
    {
        private long length;
        public long Length { get { return length; } set { length = value; } }
        public SetAllocationSizeResponse(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetAllocationSizeResponse, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetAllocationSizeResponse(SetAllocationSizeRequest request) : base(DataType.SetAllocationSizeResponse, request) { }
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