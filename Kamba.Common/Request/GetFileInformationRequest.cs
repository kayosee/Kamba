using DokanNet;

namespace Kamba.Common.Request
{
    internal class GetFileInformationRequest : FileRequest
    {
        public GetFileInformationRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.GetFileInformationRequest, clientId, requestId, fileName, info)
        {
        }
        public GetFileInformationRequest(ByteArrayStream stream) : base(stream) { }
    }
}