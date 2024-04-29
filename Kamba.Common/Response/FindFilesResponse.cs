using DokanNet;

namespace Kamba.Common.Response
{
    public class FindFilesResponse : FileResponse
    {
        private long fileSize;
        private FileInformation[] files;
        public FileInformation[] Files { get { return files; } set { files = value; } }

        public FindFilesResponse(int clientId, long requestId, string fileName, IDokanFileInfo info) : base(DataType.FindFilesResponse, clientId, requestId, fileName, info)
        {
            files = new FileInformation[0];
        }
        public FindFilesResponse(ByteArrayStream stream) : base(stream)
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