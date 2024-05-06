using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class CloseFileResponse : FileResponse
    {
        public CloseFileResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.CloseFileResponse, clientId, requestId, fileName, info)
        {
        }
        public CloseFileResponse(CloseFileRequest request) : base(DataType.CloseFileResponse, request) { }
        public CloseFileResponse(ByteArrayStream stream) : base(stream) { }
    }
}