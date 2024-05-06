using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class FindStreamsResponse : FileResponse
    {
        private int fileSize;
        private FileInformation[] files;

        public FileInformation[] Files { get => files; set => files = value; }

        public FindStreamsResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FindStreamsResponse, clientId, requestId, fileName, info)
        {
            files = new FileInformation[0];
        }
        public FindStreamsResponse(FindStreamsRequest request) : base(DataType.FindStreamsRequest, request)
        {
            files = new FileInformation[0];
        }
        public FindStreamsResponse(ByteArrayStream stream) : base(stream)
        {
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
            fileSize = files.Length;
            stream.Write(fileSize);
            foreach (var file in files)
                stream.WriteFileInformation(file);
            return stream;
        }
    }
}