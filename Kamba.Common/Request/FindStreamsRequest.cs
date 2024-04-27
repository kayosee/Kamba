using DokanNet;

namespace Kamba.Common.Request
{
    public class FindStreamsRequest : FileRequest
    {
        public FindStreamsRequest(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FindStreamsRequest, clientId, requestId, fileName, info)
        {
        }
        public FindStreamsRequest(ByteArrayStream stream) : base(stream) { }
    }
}