using DokanNet;

namespace Kamba.Common.Request
{
    public class GetVolumeInformationRequest : FileRequest
    {
        public GetVolumeInformationRequest(int clientId, long requestId, IDokanFileInfo info) : base(DataType.GetVolumeInformationRequest, clientId, requestId,"",info)
        {
        }
        public GetVolumeInformationRequest(ByteArrayStream stream) : base(stream)
        {
        }

    }
}