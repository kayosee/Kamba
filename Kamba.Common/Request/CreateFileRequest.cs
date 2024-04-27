using DokanNet;

namespace Kamba.Common.Request
{
    public class CreateFileRequest : FileRequest
    {
        private long access;
        private int share;
        private int mode;
        private int options;
        private int attributes;

        public CreateFileRequest(int clientId, long requestId, string fileName, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, IDokanFileInfo info) : base(DataType.CreateFileRequest, clientId, requestId, fileName, info)
        {
            this.access = (long)access;
            this.share = (int)share;
            this.mode = (int)mode;
            this.options = (int)options;
            this.attributes = (int)attributes;
        }
        public CreateFileRequest(ByteArrayStream stream) : base(stream)
        {
            access = stream.ReadInt64();
            share = stream.ReadInt32();
            mode = stream.ReadInt32();
            options = stream.ReadInt32();
            attributes = stream.ReadInt32();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(access);
            stream.Write(share);
            stream.Write(mode);
            stream.Write(options);
            stream.Write(attributes);
            return stream;
        }
    }
}