using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class FileReadRequest : SessionRequest
    {
        private int _pathLength;
        private string _path;
        private long _position;
        private int _size;

        public long Position { get => _position; set => _position = value; }
        public string Path { get => _path; set => _path = value; }
        public int Size { get => _size; set => _size = value; }

        public FileReadRequest(int clientId, long requestId, string path, long position, int size) : base(DataType.FileReadRequest, clientId, requestId)
        {
            _path = path;
            _position = position;
            _size = size;
        }
        public FileReadRequest(ByteArrayStream stream) : base(stream)
        {
            _position = stream.ReadInt64();
            _size = stream.ReadInt32();
            _pathLength = stream.ReadInt32();
            var buffer = new byte[_pathLength];
            stream.Read(buffer, 0, _pathLength);
            _path = System.Text.Encoding.UTF8.GetString(buffer);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            var buffer = System.Text.Encoding.UTF8.GetBytes(_path);
            _pathLength = buffer.Length;
            stream.Write(_position);
            stream.Write(_size);
            stream.Write(_pathLength);
            stream.Write(buffer, 0, _pathLength);
            return stream;
        }
    }
}
