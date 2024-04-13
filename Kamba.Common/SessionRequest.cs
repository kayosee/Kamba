using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public abstract class SessionRequest : SessionData
    {
        private int _requestId;
        private long _requestTime;
        protected SessionRequest(int clientId, int requestId) : base(clientId)
        {
            _requestId = requestId;
            _requestTime = DateTime.Now.Ticks;
        }
        public int RequestId { get => _requestId; set => _requestId = value; }
        public long RequestTime { get => _requestTime; set => _requestTime = value; }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_requestId);
            stream.Write(_requestTime);
            return stream;
        }
    }
}
