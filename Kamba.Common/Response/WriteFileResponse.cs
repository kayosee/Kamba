using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class WriteFileResponse : FileResponse
    {
        private int bufferLength;
        private byte[] buffer;
        private long offset;
        private int bytesWritten;

        public int BytesWritten { get => bytesWritten; set => bytesWritten = value; }
        public long Offset { get => offset; set => offset = value; }
        public byte[] Buffer { get => buffer; set => buffer = value; }

        public WriteFileResponse(int clientId, long requestId, string fileName, byte[] buffer, long offset, IDokanFileInfo info) : base(DataType.WriteFileResponse, clientId, requestId, fileName, info)
        {
            this.buffer = buffer;
            this.offset = offset;
        }
        public WriteFileResponse(WriteFileRequest request) : base(DataType.WriteFileResponse, request) 
        {            
        }
        public WriteFileResponse(ByteArrayStream stream) : base(stream)
        {
            offset = stream.ReadInt64();
            bytesWritten = stream.ReadInt32();
            bufferLength = stream.ReadInt32();
            buffer = new byte[bufferLength];
            stream.Read(buffer, 0, bufferLength);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(offset);
            stream.Write(bytesWritten);
            stream.Write(buffer.Length);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}