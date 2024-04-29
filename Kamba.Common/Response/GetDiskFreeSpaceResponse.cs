using DokanNet;

namespace Kamba.Common.Response
{
    public class GetDiskFreeSpaceResponse : FileResponse
    {
        private long freeBytesAvailable;
        private long totalNumberOfBytes;
        private long totalNumberOfFreeBytes;

        public long FreeBytesAvailable { get => freeBytesAvailable; set => freeBytesAvailable = value; }
        public long TotalNumberOfBytes { get => totalNumberOfBytes; set => totalNumberOfBytes = value; }
        public long TotalNumberOfFreeBytes { get => totalNumberOfFreeBytes; set => totalNumberOfFreeBytes = value; }

        public GetDiskFreeSpaceResponse(int clientId, long requestId, long responseId, IDokanFileInfo info) : base(DataType.GetDiskFreeSpaceResponse, clientId, requestId, "", info)
        {
        }
        public GetDiskFreeSpaceResponse(ByteArrayStream stream) : base(stream)
        {
            stream.Write(freeBytesAvailable);
            stream.Write(totalNumberOfBytes); 
            stream.Write(totalNumberOfFreeBytes);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(freeBytesAvailable);
            stream.Write(totalNumberOfBytes);
            stream.Write(totalNumberOfFreeBytes);
            return stream;
        }
    }
}