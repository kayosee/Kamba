using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    internal class FileReadRequest : SessionRequest
    {
        private int _pathLength;
        private string _path;
        public FileReadRequest(int clientId, int requestId, string path) : base(clientId, requestId, DataType.FileReadRequest)
        {
            _path = path;
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            var buffer = System.Text.Encoding.UTF8.GetBytes(_path);
            _pathLength = buffer.Length;
            stream.Write(_pathLength);
            stream.Write(buffer, 0, _pathLength);
            return stream;
        }
        protected override void SetStream(ByteArrayStream stream)
        {
            base.SetStream(stream);
            _pathLength = stream.ReadInt32();
            var buffer = new byte[_pathLength];
            stream.Read(buffer, 0, _pathLength);
            _path = System.Text.Encoding.UTF8.GetString(buffer);
        }
    }
}
