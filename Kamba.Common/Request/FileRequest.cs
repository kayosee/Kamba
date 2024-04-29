using DokanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common.Request
{
    public abstract class FileRequest : SessionRequest
    {
        private int fileNameLength;
        private string fileName;
        private IDokanFileInfo info;

        public string FileName { get => fileName; set => fileName = value; }
        public IDokanFileInfo Info { get => info; set => info = value; }

        public FileRequest(DataType dataType, int clientId, long requestId, string fileName, IDokanFileInfo info) : base(dataType, clientId, requestId)
        {
            this.fileName = fileName;
            this.info = info;
        }
        public FileRequest(ByteArrayStream stream) : base(stream)
        {
            fileNameLength = stream.ReadInt32();
            var buffer = new byte[fileNameLength];
            stream.Read(buffer, 0, fileNameLength);
            fileName = System.Text.Encoding.UTF8.GetString(buffer);
            info = stream.ReadDokanFileInfo();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            var buffer = System.Text.Encoding.UTF8.GetBytes(fileName);
            fileNameLength = buffer.Length;
            stream.Write(fileNameLength);
            stream.Write(buffer, 0, buffer.Length);
            stream.WriteDokanFileInfo(info);
            return stream;
        }

    }
}
