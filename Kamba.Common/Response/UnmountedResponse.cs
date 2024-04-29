using DokanNet;

namespace Kamba.Common.Response
{
    public class UnmountedResponse : FileResponse
    {
        public UnmountedResponse(int clientId, long requestId, IDokanFileInfo info):base(DataType.UnmountedResponse,clientId,requestId,"",info)
        {
        }
        public UnmountedResponse(ByteArrayStream stream) : base(stream)
        {
        }
    }
}