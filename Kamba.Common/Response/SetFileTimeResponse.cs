using DokanNet;

namespace Kamba.Common.Response
{
    public class SetFileTimeResponse : FileResponse
    {
        private long creationTime;
        private long lastAccessTime;
        private long lastWriteTime;
        public SetFileTimeResponse(int clientId, long requestId, string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, IDokanFileInfo info) : base(DataType.SetFileTimeResponse, clientId, requestId, fileName, info)
        {
            this.creationTime = creationTime.GetValueOrDefault().Ticks;
            this.lastAccessTime = lastAccessTime.GetValueOrDefault().Ticks;
            this.lastWriteTime = lastWriteTime.GetValueOrDefault().Ticks;
        }
        public SetFileTimeResponse(ByteArrayStream stream) : base(stream)
        {
            creationTime = stream.ReadInt64();
            lastAccessTime = stream.ReadInt64();
            lastWriteTime = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(creationTime);
            stream.Write(lastAccessTime);
            stream.Write(lastWriteTime);
            return stream;
        }
    }
}