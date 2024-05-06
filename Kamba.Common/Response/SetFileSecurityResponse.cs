using DokanNet;
using Kamba.Common.Request;
using System.Security.AccessControl;

namespace Kamba.Common.Response
{
    public class SetFileSecurityResponse : FileResponse
    {
        private FileSystemSecurity security;
        private int sections;

        public FileSystemSecurity Security { get => security; set => security = value; }
        public int Sections { get => sections; set => sections = value; }

        public SetFileSecurityResponse(int clientId, long requestId, string fileName, FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info):base(DataType.SetFileSecurityResponse,clientId,requestId,fileName,info)
        {
            this.security = security;
            this.sections = (int)sections;
        }
        public SetFileSecurityResponse(SetFileSecurityRequest request) : base(DataType.SetFileSecurityResponse, request) 
        {
        }
        public SetFileSecurityResponse(ByteArrayStream stream):base(stream)
        {            
        }
    }
}