using DokanNet;

namespace Kamba.Common.Request
{
    public class GetVolumeInformationRequest : SessionRequest
    {
        private IDokanFileInfo info;

        public GetVolumeInformationRequest(int clientId, long requestId, IDokanFileInfo info) : base(DataType.GetVolumeInformationRequest, clientId, requestId)
        {
            this.info = info;
        }
        public GetVolumeInformationRequest(ByteArrayStream stream) : base(stream)
        {
        }

    }
}