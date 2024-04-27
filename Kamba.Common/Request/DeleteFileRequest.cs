using DokanNet;

namespace Kamba.Common.Request
{
    internal class DeleteFileRequest : FileRequest
    {
        public DeleteFileRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.DeleteFileRequest, clientId, requestId, fileName, info)
        {
        }
        public DeleteFileRequest(ByteArrayStream stream) : base(stream) { }
    }
}