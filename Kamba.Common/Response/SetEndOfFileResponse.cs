using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class SetEndOfFileResponse : FileResponse
    {
        private long length;
        public long Length { get { return length; } set { length = value; } }
        public SetEndOfFileResponse(int clientId, long requestId, string fileName, long length, IDokanFileInfo info) : base(DataType.SetEndOfFileResponse, clientId, requestId, fileName, info)
        {
            this.length = length;
        }
        public SetEndOfFileResponse(SetEndOfFileRequest request) : base(DataType.SetEndOfFileResponse, request) { }
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