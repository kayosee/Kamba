using DokanNet;

namespace Kamba.Common.Request
{
    public class SetEndOfFileRequest : FileRequest
    {
        private long length;

        public SetEndOfFileRequest(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetEndOfFileRequest, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetEndOfFileRequest(ByteArrayStream stream) : base(stream)
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