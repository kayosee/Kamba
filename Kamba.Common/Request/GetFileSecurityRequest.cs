using System.Security.AccessControl;

namespace Kamba.Common.Request
{
    public class GetFileSecurityRequest : FileRequest
    {
        private int sections;
        public GetFileSecurityRequest(int clientId, long requestId, string fileName, AccessControlSections sections, DokanNet.IDokanFileInfo info) : base(DataType.GetFileSecurityRequest, clientId, requestId, fileName, info)
        {
            this.sections = (int)sections;
        }
        public GetFileSecurityRequest(ByteArrayStream stream) : base(stream)
        {
            sections = stream.ReadInt32();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(sections);
            return stream;
        }
    }
}