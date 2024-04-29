using DokanNet;

namespace Kamba.Common.Response
{
    public class UnlockFileResponse : FileResponse
    {
        private long offset;
        private long length;

        public UnlockFileResponse(int clientId, long requestId, string fileName, long offset, long length, IDokanFileInfo info) : base(DataType.UnlockFileResponse, clientId, requestId, fileName, info)
        {
            this.offset = offset;
            this.length = length;
        }
        public UnlockFileResponse(ByteArrayStream stream) : base(stream)
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