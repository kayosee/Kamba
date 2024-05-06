using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class GetVolumeInformationResponse : FileResponse
    {
        private int volumeLabelLength;
        private string volumeLabel;
        private uint features;
        private int fileSystemNameLength;
        private string fileSystemName;
        private uint maximumComponentLength;

        public string VolumeLabel { get => volumeLabel; set => volumeLabel = value; }
        public FileSystemFeatures Features { get => (FileSystemFeatures)features; set => features = (uint)value; }
        public string FileSystemName { get => fileSystemName; set => fileSystemName = value; }
        public uint MaximumComponentLength { get => maximumComponentLength; set => maximumComponentLength = value; }

        public GetVolumeInformationResponse(int clientId, long requestId, IDokanFileInfo info) : base(DataType.GetVolumeInformationResponse, clientId, requestId, "", info)
        {
            volumeLabel = string.Empty;
            fileSystemName = string.Empty;
        }
        public GetVolumeInformationResponse(GetVolumeInformationRequest request) : base(DataType.GetVolumeInformationResponse, request) 
        {
            volumeLabel = string.Empty;
            fileSystemName = string.Empty;
        }
        public GetVolumeInformationResponse(ByteArrayStream stream) : base(stream)
        {
            volumeLabelLength = stream.ReadInt32();
            var buffer = new byte[volumeLabelLength];
            stream.Read(buffer, 0, volumeLabelLength);
            volumeLabel = System.Text.Encoding.UTF8.GetString(buffer);

            features = stream.ReadUInt32();
            fileSystemNameLength = stream.ReadInt32();
            buffer = new byte[fileSystemNameLength];
            stream.Read(buffer, 0, fileSystemNameLength);
            fileSystemName = System.Text.Encoding.UTF8.GetString(buffer);

            maximumComponentLength = stream.ReadUInt32();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            var buffer = System.Text.Encoding.UTF8.GetBytes(volumeLabel);
            volumeLabelLength = buffer.Length;
            stream.Write(volumeLabelLength);
            stream.Write(buffer, 0, volumeLabelLength);

            stream.Write(features);

            buffer = System.Text.Encoding.UTF8.GetBytes(fileSystemName);
            fileSystemNameLength = buffer.Length;
            stream.Write(fileSystemNameLength);
            stream.Write(buffer, 0, fileSystemNameLength);
            stream.Write(maximumComponentLength);
            return stream;
        }
    }
}