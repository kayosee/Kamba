using DokanNet;

namespace Kamba.Common.Request
{
    public class CloseFileRequest : FileRequest
    {
        public CloseFileRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.CloseFileRequest, clientId, requestId, fileName, info)
        {
        }
        public CloseFileRequest(ByteArrayStream stream) : base(stream) { }
    }
}