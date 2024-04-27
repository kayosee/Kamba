using DokanNet;

namespace Kamba.Common.Request
{
    public class UnlockFileRequest : FileRequest
    {
        private long offset;
        private long length;

        public UnlockFileRequest(int clientId, long requestId, string fileName, long offset, long length, IDokanFileInfo info) : base(DataType.UnlockFileRequest, clientId, requestId, fileName, info)
        {
            this.offset = offset;
            this.length = length;
        }
        public UnlockFileRequest(ByteArrayStream stream) : base(stream)
        {
            offset = stream.ReadInt64();
            length = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(offset);
            stream.Write(length);
            return stream;
        }
    }
}