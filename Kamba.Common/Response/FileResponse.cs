using DokanNet;
using Kamba.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common.Response
{
    public abstract class FileResponse : FileRequest
    {
        private long responseCode;
        private int responseMessageLength;
        private string responseMessage;

        public NtStatus ResponseCode { get => (NtStatus)responseCode; set => responseCode = (long)value; }
        public string ResponseMessage { get => responseMessage; set => responseMessage = value; }

        public FileResponse(DataType dataType, int clientId, long requestId, string fileName, IDokanFileInfo info) : base(dataType, clientId, requestId, fileName, info)
        {
            responseCode = 0;
            responseMessageLength = 0;
            responseMessage = string.Empty;
        }
        public FileResponse(ByteArrayStream stream) : base(stream)
        {
            responseCode = stream.ReadInt64();
            responseMessageLength = stream.ReadInt32();
            var buffer = new byte[responseMessageLength];
            stream.Read(buffer, 0, buffer.Length);
            responseMessage = System.Text.Encoding.UTF8.GetString(buffer);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(responseCode);
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);
            responseMessageLength = buffer.Length;
            stream.Write(responseMessageLength);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }

    }
}
