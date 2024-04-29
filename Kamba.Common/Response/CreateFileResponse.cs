using DokanNet;
using Kamba.Common.Request;
using FileAccess = DokanNet.FileAccess;

namespace Kamba.Common.Response
{
    public class CreateFileResponse : FileResponse
    {
        private long access;
        private int share;
        private int mode;
        private int options;
        private int attributes;

        public FileAccess Access { get => (FileAccess)access; set => access = (int)value; }
        public FileShare Share { get => (FileShare)share; set => share = (int)value; }
        public FileMode Mode { get => (FileMode)mode; set => mode = (int)value; }
        public FileOptions Options { get => (FileOptions)options; set => options = (int)value; }
        public FileAttributes Attributes { get => (FileAttributes)attributes; set => attributes = (int)value; }

        public CreateFileResponse(int clientId, long requestId, string fileName, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, IDokanFileInfo info) : base(DataType.CreateFileResponse, clientId, requestId, fileName, info)
        {
            this.access = (long)access;
            this.share = (int)share;
            this.mode = (int)mode;
            this.options = (int)options;
            this.attributes = (int)attributes;
        }
        public CreateFileResponse(ByteArrayStream stream) : base(stream)
        {
            access = stream.ReadInt64();
            share = stream.ReadInt32();
            mode = stream.ReadInt32();
            options = stream.ReadInt32();
            attributes = stream.ReadInt32();
        }

        public CreateFileResponse(CreateFileRequest request) : base(DataType.CreateFileResponse, request.ClientId, request.RequestId, request.FileName, request.Info)
        {
            this.access = (long)request.Access;
            this.share = (int)request.Share;
            this.mode = (int)request.Mode;
            this.options = (int)request.Options;
            this.attributes = (int)request.Attributes;
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