using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class AuthenticateResponse : SessionResponse
    {
        private byte _status;

        public AuthenticateResponse(int clientId, long requestId, long responseId, AuthenticateStatus status) : base(DataType.AuthenticateResponse, clientId, requestId, responseId)
        {
            _status = (byte)(status);
        }
        public AuthenticateResponse(ByteArrayStream stream) : base(stream)
        {
            _status = stream.ReadByte();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_status);
            return stream;
        }
        public AuthenticateStatus Status { get => (AuthenticateStatus)_status; set => _status = (byte)value; }

    }
}
