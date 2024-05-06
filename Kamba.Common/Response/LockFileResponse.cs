using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class LockFileResponse : FileResponse
    {
        private long offset;
        private long length;
        public LockFileResponse(int clientId, long requestId, string fileName, long offset, long length, IDokanFileInfo info) : base(DataType.LockFileResponse, clientId, requestId, fileName, info)
        {
            this.offset = offset;
            this.length = length;
        }
        public LockFileResponse(LockFileRequest request) : base(DataType.LockFileResponse, request) { }
        public LockFileResponse(ByteArrayStream stream) : base(stream)
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