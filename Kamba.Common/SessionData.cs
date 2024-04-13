using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public abstract class SessionData : ISerialization
    {
        private int _clientId;
        public int ClientId { get { return _clientId; } set { _clientId = value; } }
        public SessionData(int clientId) { _clientId = clientId; }

        protected virtual ByteArrayStream GetStream()
        {
            using (var stream = new ByteArrayStream())
            {
                stream.Write(_clientId);
                return stream;
            }
        }
        public byte[] Serialize()
        {
            return GetStream().GetBuffer();
        }
    }
}
