using DokanNet;

namespace Kamba.Common.Request
{
    public class CleanupRequest : FileRequest
    {
        public CleanupRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.CleanupRequest, clientId, requestId, fileName, info)
        {
        }       
    }
}