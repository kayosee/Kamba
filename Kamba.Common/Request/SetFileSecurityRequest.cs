using DokanNet;
using System.Security.AccessControl;

namespace Kamba.Common.Request
{
    public class SetFileSecurityRequest : FileRequest
    {
        private FileSystemSecurity security;
        private int sections;
        public SetFileSecurityRequest(int clientId, long requestId, string fileName, FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info):base(DataType.SetFileSecurityRequest,clientId,requestId,fileName,info)
        {
            this.security = security;
            this.sections = (int)sections;
        }
        public SetFileSecurityRequest(ByteArrayStream stream):base(stream)
        {            
        }
    }
}