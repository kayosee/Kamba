using DokanNet;

namespace Kamba.Common.Request
{
    public class FindFilesWithPatternRequest : FileRequest
    {
        private int searchPatternLength;
        private string searchPattern;

        public FindFilesWithPatternRequest(int clientId, long requestId, string fileName, string searchPattern, IDokanFileInfo info) : base(DataType.FindFilesWithPatternRequest, clientId, requestId, fileName, info)
        {
            this.searchPattern = searchPattern;
        }
        public FindFilesWithPatternRequest(ByteArrayStream stream) : base(stream)
        {
            searchPatternLength = stream.ReadInt32();
            var buffer = new byte[searchPatternLength];
            stream.Read(buffer, 0, buffer.Length);
            searchPattern = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(searchPatternLength);
            var buffer = System.Text.Encoding.UTF8.GetBytes(searchPattern);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}