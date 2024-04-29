using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class Packet : ISerialization
    {
        private static byte[] _flag = new byte[] { 75, 65, 89, 79 };

        private long _totalLength;
        private int _sequence;
        private short _sliceLength;
        private byte[] _sliceData;
        public const ushort MaxLength = ushort.MaxValue;
        public long TotalLength { get => _totalLength; set => _totalLength = value; }
        public short SliceLength { get => _sliceLength; set => _sliceLength = value; }
        public byte[] SliceData { get => _sliceData; set => _sliceData = value; }
        public int Sequence { get => _sequence; set => _sequence = value; }

        private Packet() { }
        public Packet(long totalLength, int sequence, short sliceLength, byte[] data)
        {
            _totalLength = totalLength;
            _sequence = sequence;
            _sliceLength = sliceLength;
            _sliceData = data;
        }
        public Packet(byte[] buffer)
        {
            using (var stream = new ByteArrayStream(buffer))
            {
                var _ = new byte[_flag.Length];
                stream.Read(_, 0, _.Length);

                _totalLength = stream.ReadInt64();
                _sequence = stream.ReadInt32();
                _sliceLength = stream.ReadInt16();
                _sliceData = new byte[_sliceLength];
                stream.Read(_sliceData, 0, _sliceLength);
            }
        }
        public byte[] Serialize()
        {
            using (var stream = new ByteArrayStream())
            {
                stream.Write(_flag, 0, _flag.Length);
                stream.Write(_totalLength);
                stream.Write(_sequence);
                stream.Write(_sliceLength);
                stream.Write(_sliceData, 0, _sliceData.Length);
                var result=stream.GetBuffer();
                File.WriteAllBytes("d:\\1.data",result);
                return result;
            }
        }
        public static Packet FromSocket(Socket socket, int maxWaitSeconds = -1)
        {
            if (maxWaitSeconds > 0)
                socket.ReceiveTimeout = (int)TimeSpan.FromSeconds(maxWaitSeconds).TotalMilliseconds;
            else
                socket.ReceiveTimeout = -1;

            Packet packet = new Packet();

            var buffer = new byte[_flag.Length];
            socket.Receive(buffer, buffer.Length, SocketFlags.None);
            if (System.Text.Encoding.UTF8.GetString(buffer) != System.Text.Encoding.UTF8.GetString(_flag))
                throw new InvalidDataException("包头标志不正确");

            buffer = new byte[sizeof(Int64)];
            socket.Receive(buffer, sizeof(Int64), SocketFlags.None);
            packet.TotalLength = BitConverter.ToInt64(buffer);

            buffer = new byte[sizeof(Int32)];
            socket.Receive(buffer, sizeof(Int32), SocketFlags.None);
            packet.Sequence = BitConverter.ToInt32(buffer);
            if (packet.Sequence < 0)
                throw new InvalidDataException(nameof(packet.Sequence) + "序号无效");

            buffer = new byte[sizeof(Int16)];
            socket.Receive(buffer, sizeof(Int16), SocketFlags.None);
            packet.SliceLength = BitConverter.ToInt16(buffer);
            if (packet.SliceLength <= 0)
                throw new InvalidDataException(nameof(packet.SliceLength) + "长度无效");

            packet.SliceData = new byte[packet.SliceLength];
            socket.Receive(packet.SliceData, packet.SliceLength, SocketFlags.None);
            return packet;
        }
    }
}
