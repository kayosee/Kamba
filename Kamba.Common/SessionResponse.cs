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
        private int _responseId;
        private long _responseTime;
        protected SessionResponse(int clientId, int requestId, int responseId,DataType dataType) : base(clientId, requestId,dataType)
        {
            _responseId = responseId;
            _responseTime = DateTime.Now.Ticks;
        }
        public int ResponseId { get => _responseId; set => _responseId = value; }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_responseId);
            stream.Write(_responseTime);
            return stream;
        }
        protected override void SetStream(ByteArrayStream stream)
        {
            base.SetStream(stream);
            _responseId = stream.ReadInt32();
            _responseTime = stream.ReadInt64();
        }

    }
}
