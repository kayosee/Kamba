using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class SessionData : ISerialization
    {
        private int _clientId;
        private byte _dataType;
        public int ClientId { get { return _clientId; } set { _clientId = value; } }

        public DataType DataType { get => (DataType)_dataType; set => _dataType = (byte)value; }
        private SessionData() { }
        public SessionData(int clientId, DataType dataType) { _clientId = clientId; _dataType = (byte)dataType; }

        protected virtual ByteArrayStream GetStream()
        {
            using (var stream = new ByteArrayStream())
            {
                stream.Write(_clientId);
                stream.Write(_dataType);
                return stream;
            }
        }
        protected virtual void SetStream(ByteArrayStream stream)
        {
            _clientId = stream.ReadInt32();
            _dataType = stream.ReadByte();
        }
        public byte[] Serialize()
        {
            return GetStream().GetBuffer();
        }

        public static SessionData FromPackets(Packet[] packets)
        {
            var data = new SessionData();
            using (var stream = new ByteArrayStream())
            {
                foreach (var packet in packets.OrderBy(f=>f.Sequence))
                {
                    stream.Read(packet.SliceData, 0, packet.SliceLength);
                }
                data.SetStream(stream);
                return data;
            }
        }
    }
}
