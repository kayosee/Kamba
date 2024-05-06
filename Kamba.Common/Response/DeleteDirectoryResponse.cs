using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class DeleteDirectoryResponse : FileResponse
    {
        public DeleteDirectoryResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.DeleteDirectoryResponse, clientId, requestId, fileName, info)
        {
        }
        public DeleteDirectoryResponse(DeleteDirectoryRequest request) : base(DataType.DeleteDirectoryResponse, request) { }
        public DeleteDirectoryResponse(ByteArrayStream stream) : base(stream) { }
    }
}