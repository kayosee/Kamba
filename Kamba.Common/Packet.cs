using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class Packet : ISerialization
    {
        private int _totalLength;
        private int _sliceLength;
        private byte[] _sliceData;
        public Packet(int length, int sequence, byte[] data)
        {
            _totalLength = length;
            _sliceLength = sequence;
            _sliceData = data;
        }
        public Packet(byte[] buffer)
        {
            using (var stream = new ByteArrayStream(buffer))
            {
                _totalLength = stream.ReadInt32();
                _sliceLength = stream.ReadInt32();
                _sliceData = new byte[_sliceLength];
                stream.Read(_sliceData, 0, _sliceLength);
            }
        }
        public byte[] Serialize()
        {
            using (var stream = new ByteArrayStream())
            {
                stream.Write(_totalLength);
                stream.Write(_sliceLength);
                stream.Write(_sliceData, 0, _sliceData.Length);
                return stream.GetBuffer();
            }
        }
    }
}
