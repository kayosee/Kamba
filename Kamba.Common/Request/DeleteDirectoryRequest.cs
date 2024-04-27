using DokanNet;

namespace Kamba.Common.Request
{
    public class DeleteDirectoryRequest : FileRequest
    {
        public DeleteDirectoryRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.DeleteDirectoryRequest, clientId, requestId, fileName, info)
        {
        }
        public DeleteDirectoryRequest(ByteArrayStream stream) : base(stream) { }
    }
}