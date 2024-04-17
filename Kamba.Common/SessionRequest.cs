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
        protected long _requestId;
        protected long _requestTime;
        public SessionRequest(ByteArrayStream stream) : base(stream)
        {
            _requestId = stream.ReadInt64();
            _requestTime = stream.ReadInt64();
        }
        protected SessionRequest(DataType dataType, int clientId, long requestId) : base(dataType, clientId)
        {
            _requestId = requestId;
            _requestTime = DateTime.Now.Ticks;
        }
        public long RequestId { get => _requestId; set => _requestId = value; }
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
