using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class ReadFileResponse : FileResponse
    {
        private long offset;
        private int length;
        private byte[] buffer;

        public byte[] Buffer { get => buffer; set => buffer = value; }
        public long Offset { get { return offset; } set { offset = value; } }

        public ReadFileResponse(int clientId, long requestId, string fileName, long offset, IDokanFileInfo info) : base(DataType.ReadFileResponse, clientId, requestId, fileName, info)
        {
            this.offset = offset;
            this.length = 0;
            this.buffer = new byte[0];
        }
        public ReadFileResponse(ReadFileRequest request) : base(DataType.ReadFileResponse, request)
        {
            buffer = new byte[0];
        }
        public ReadFileResponse(ByteArrayStream stream) : base(stream)
        {
            offset = stream.ReadInt64();
            length = stream.ReadInt32();
            buffer = new byte[length];
            stream.Read(buffer, 0, length);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(offset);
            length = buffer.Length;
            stream.Write(length);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}