using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class SetFileAttributesResponse : FileResponse
    {
        private int attributes;
        public FileAttributes Attributes
        {
            get
            {
                return (FileAttributes)attributes;
            }
            set
            {
                attributes = (int)value;
            }
        }
        public SetFileAttributesResponse(int clientId, long requestId, string fileName, FileAttributes attributes, IDokanFileInfo info) : base(DataType.SetFileAttributesResponse, clientId, requestId, fileName, info)
        {
            this.attributes = (int)attributes;
        }
        public SetFileAttributesResponse(SetFileAttributesRequest request) : base(DataType.SetFileAttributesResponse, request) { }
        public SetFileAttributesResponse(ByteArrayStream stream) : base(stream)
        {
            attributes = stream.ReadInt32();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(attributes);
            return stream;
        }
    }
}