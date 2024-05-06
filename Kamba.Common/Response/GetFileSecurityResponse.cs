using Kamba.Common.Request;
using System.Security.AccessControl;

namespace Kamba.Common.Response
{
    public class GetFileSecurityResponse : FileResponse
    {
        private int sections;
        public GetFileSecurityResponse(int clientId, long requestId, string fileName, AccessControlSections sections, DokanNet.IDokanFileInfo info) : base(DataType.GetFileSecurityResponse, clientId, requestId, fileName, info)
        {
            this.sections = (int)sections;
        }
        public GetFileSecurityResponse(GetFileSecurityRequest request) : base(DataType.GetFileSecurityResponse, request) { }
        public GetFileSecurityResponse(ByteArrayStream stream) : base(stream)
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