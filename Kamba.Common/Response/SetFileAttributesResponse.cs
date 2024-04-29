using DokanNet;

namespace Kamba.Common.Response
{
    public class SetFileAttributesResponse : FileResponse
    {
        private int attributes;

        public SetFileAttributesResponse(int clientId, long requestId, string fileName, FileAttributes attributes, IDokanFileInfo info) : base(DataType.SetFileAttributesResponse, clientId, requestId, fileName, info)
        {
            this.attributes = (int)attributes;
        }
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