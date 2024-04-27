using DokanNet;

namespace Kamba.Common.Request
{
    public class LockFileRequest : FileRequest
    {
        private long offset;
        private long length;
        public LockFileRequest(int clientId, long requestId, string fileName, long offset, long length, IDokanFileInfo info) : base(DataType.LockFileRequest, clientId, requestId, fileName, info)
        {
            this.offset = offset;
            this.length = length;
        }
        public LockFileRequest(ByteArrayStream stream) : base(stream)
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