using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class MoveFileResponse : FileResponse
    {
        private int oldNameLength;
        private int newNameLength;
        private string oldName;
        private string newName;
        private byte replace;

        public bool Replace { get => replace == 1; set => replace = (byte)(value ? 1 : 0); }

        public MoveFileResponse(int clientId, long requestId, string oldName, string newName, bool replace, IDokanFileInfo info) : base(DataType.MoveFileResponse, clientId, requestId, "", info)
        {
            this.oldName = oldName;
            this.newName = newName;
            this.replace = (byte)(replace ? 0 : 1);
        }
        public MoveFileResponse(MoveFileRequest request) : base(DataType.MoveFileResponse, request)
        {
            oldName = string.Empty;
            newName = string.Empty;
        }
        public MoveFileResponse(ByteArrayStream stream) : base(stream)
        {
            oldNameLength = stream.ReadInt32();
            var buffer = new byte[oldNameLength];
            stream.Read(buffer, 0, buffer.Length);
            oldName = System.Text.Encoding.UTF8.GetString(buffer);

            newNameLength = stream.ReadInt32();
            buffer = new byte[newNameLength];
            stream.Read(buffer, 0, buffer.Length);
            newName = System.Text.Encoding.UTF8.GetString(buffer);

            replace = stream.ReadByte();
        }
    }
}