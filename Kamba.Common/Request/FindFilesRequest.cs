using DokanNet;

namespace Kamba.Common.Request
{
    public class FindFilesRequest : FileRequest
    {
        public FindFilesRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FindFilesRequest, clientId, requestId, fileName, info)
        {
        }
        public FindFilesRequest(ByteArrayStream stream) : base(stream) { }
    }
}