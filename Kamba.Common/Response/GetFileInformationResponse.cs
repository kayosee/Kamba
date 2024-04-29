using DokanNet;

namespace Kamba.Common.Response
{
    public class GetFileInformationResponse : FileResponse
    {
        public FileInformation FileInfo { get; set; }

        public GetFileInformationResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.GetFileInformationResponse, clientId, requestId, fileName, info)
        {
        }
        public GetFileInformationResponse(ByteArrayStream stream) : base(stream) { }
    }
}