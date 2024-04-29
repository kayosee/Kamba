using DokanNet;

namespace Kamba.Common.Response
{
    public class SetEndOfFileResponse : FileResponse
    {
        private long length;

        public SetEndOfFileResponse(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetEndOfFileResponse, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetEndOfFileResponse(ByteArrayStream stream) : base(stream)
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