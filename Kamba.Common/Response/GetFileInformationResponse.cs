using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class GetFileInformationResponse : FileResponse
    {
        public FileInformation FileInfo { get; set; }

        public GetFileInformationResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.GetFileInformationResponse, clientId, requestId, fileName, info)
        {
        }
        public GetFileInformationResponse(GetFileInformationRequest request) : base(DataType.GetFileInformationResponse, request) { }
        public GetFileInformationResponse(ByteArrayStream stream) : base(stream) { }
    }
}