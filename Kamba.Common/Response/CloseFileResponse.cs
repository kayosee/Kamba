using DokanNet;

namespace Kamba.Common.Response
{
    public class CloseFileResponse : FileResponse
    {
        public CloseFileResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.CloseFileResponse, clientId, requestId, fileName, info)
        {
        }
        public CloseFileResponse(ByteArrayStream stream) : base(stream) { }
    }
}