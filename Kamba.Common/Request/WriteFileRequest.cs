using DokanNet;

namespace Kamba.Common.Request
{
    public class WriteFileRequest : FileRequest
    {
        private int bufferLength;
        private byte[] buffer;
        private long offset;
        public WriteFileRequest(int clientId, long requestId, string fileName, byte[] buffer, long offset, IDokanFileInfo info) : base(DataType.WriteFileRequest, clientId, requestId, fileName, info)
        {
            this.buffer = buffer;
            this.offset = offset;
        }
        public WriteFileRequest(ByteArrayStream stream) : base(stream)
        {
            bufferLength = stream.ReadInt32();
            buffer = new byte[bufferLength];
            stream.Read(buffer, 0, bufferLength);
            offset = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(offset);
            stream.Write(buffer.Length);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}