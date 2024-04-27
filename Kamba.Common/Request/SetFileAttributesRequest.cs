using DokanNet;

namespace Kamba.Common.Request
{
    public class SetFileAttributesRequest : FileRequest
    {
        private int attributes;

        public SetFileAttributesRequest(int clientId, long requestId, string fileName, FileAttributes attributes, IDokanFileInfo info) : base(DataType.SetFileAttributesRequest, clientId, requestId, fileName, info)
        {
            this.attributes = (int)attributes;
        }
        public SetFileAttributesRequest(ByteArrayStream stream) : base(stream)
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