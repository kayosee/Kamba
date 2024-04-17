using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public abstract class SessionResponse : SessionRequest
    {
        protected long _responseId;
        protected long _responseTime;
        protected SessionResponse(DataType dataType, int clientId, long requestId, long responseId) : base(dataType, clientId, requestId)
        {
            _responseId = responseId;
            _responseTime = DateTime.Now.Ticks;
        }
        public long ResponseId { get => _responseId; set => _responseId = value; }
        public SessionResponse(ByteArrayStream stream):base(stream) 
        {
            _responseId = stream.ReadInt64();
            _responseTime = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_responseId);
            stream.Write(_responseTime);
            return stream;
        }
    }
}
