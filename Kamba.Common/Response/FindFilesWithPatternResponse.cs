using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class FindFilesWithPatternResponse : FileResponse
    {
        private int searchPatternLength;
        private string searchPattern;
        private int fileSize;
        private FileInformation[] files;

        public FileInformation[] Files { get => files; set => files = value; }
        public string SearchPattern { get => searchPattern; set => searchPattern = value; }

        public FindFilesWithPatternResponse(int clientId, long requestId, string fileName, string searchPattern, IDokanFileInfo info) : base(DataType.FindFilesWithPatternResponse, clientId, requestId, fileName, info)
        {
            this.searchPattern = searchPattern;
            this.files = new FileInformation[0];
        }
        public FindFilesWithPatternResponse(FindFilesWithPatternRequest request) : base(DataType.FindFilesWithPatternResponse, request)
        {
            this.searchPattern = string.Empty;
            this.files = new FileInformation[0];
        }
        public FindFilesWithPatternResponse(ByteArrayStream stream) : base(stream)
        {
            searchPatternLength = stream.ReadInt32();
            var buffer = new byte[searchPatternLength];
            stream.Read(buffer, 0, buffer.Length);
            searchPattern = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            fileSize = stream.ReadInt32();
            files = new FileInformation[fileSize];
            for (int i = 0; i < fileSize; i++)
            {
                files[i] = stream.ReadFileInformation();
            }
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(searchPatternLength);
            var buffer = System.Text.Encoding.UTF8.GetBytes(searchPattern);
            stream.Write(buffer, 0, buffer.Length);
            fileSize = files.Length;
            stream.Write(fileSize);
            foreach (var file in files)
                stream.WriteFileInformation(file);
            return stream;
        }
    }
}