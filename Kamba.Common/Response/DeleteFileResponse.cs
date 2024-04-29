using DokanNet;

namespace Kamba.Common.Response
{
    public class DeleteFileResponse : FileResponse
    {
        public DeleteFileResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.DeleteFileResponse, clientId, requestId, fileName, info)
        {
        }
        public DeleteFileResponse(ByteArrayStream stream) : base(stream) { }
    }
}