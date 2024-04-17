using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class SessionData : ISerialization
    {
        protected byte _dataType;
        protected int _clientId;
        public int ClientId { get { return _clientId; } set { _clientId = value; } }
        public DataType DataType { get => (DataType)_dataType; set => _dataType = (byte)value; }
        private SessionData() { }
        public SessionData(ByteArrayStream stream)
        {
            _dataType = stream.ReadByte();
            _clientId = stream.ReadInt32();
        }
        public SessionData(DataType dataType, int clientId) { _dataType = (byte)dataType; _clientId = clientId; }
        protected virtual ByteArrayStream GetStream()
        {
            using (var stream = new ByteArrayStream())
            {
                stream.Write(_dataType);
                stream.Write(_clientId);
                return stream;
            }
        }
        public byte[] Serialize()
        {
            return GetStream().GetBuffer();
        }
        private static SessionData? Construct(ByteArrayStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var buffer = new byte[1];
            stream.Peek(buffer, 0, buffer.Length);
            var dataType = buffer[0];
            if (!Enum.IsDefined(typeof(DataType), (int)dataType))
                throw new InvalidDataException("包类型不正确");

            var type = typeof(SessionData).Assembly.GetTypes().First(f => f.Name == Enum.GetName((DataType)dataType));
            Debug.Assert(type != null);
            var constructor = type.GetConstructors().First(f => f.GetParameters().Any(f => f.ParameterType == typeof(ByteArrayStream)));
            SessionData? sessionData = constructor.Invoke(new object[] { stream }) as SessionData;
            return sessionData;
        }
        public static SessionData? FromPackets(Packet[] packets)
        {
            SessionData data = null;
            using (var stream = new ByteArrayStream())
            {
                foreach (var packet in packets.OrderBy(f => f.Sequence))
                {
                    stream.Write(packet.SliceData, 0, packet.SliceLength);
                }

                data = Construct(stream);
                return data;
            }            
        }

        public Packet[] ToPackets()
        {
            var stream = GetStream();
            var total = stream.Length;
            if (total < Packet.MaxLength)
            {
                return new Packet[1]
                {
                    new Packet(total,0,(short)total,stream.GetBuffer())
                };
            }

            var num = Math.Ceiling((double)stream.Length / Packet.MaxLength);
            var size = (short)Math.Floor(stream.Length / num);
            var result = new Packet[(int)num];
            for (var i = 0; i < num; i++)
            {
                result[i].TotalLength = stream.Length;
                result[i].Sequence = i;
                result[i].SliceData = new byte[size];
                result[i].SliceLength = size;
                stream.Read(result[i].SliceData, 0, size);
            }
            return result;
        }
    }
}
