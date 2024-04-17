using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class FileReadResponse : FileReadRequest
    {
        protected byte[] _fileData;
        protected int _fileDataLength;
        public FileReadResponse(int clientId, int requestId, string path, long position, int size) : base(clientId, requestId, path, position, size)
        {
            _dataType = (byte)DataType.FileReadResponse;
        }
        public FileReadResponse(ByteArrayStream stream) : base(stream)
        {
            _fileDataLength = stream.ReadInt32();
            _fileData = new byte[_fileDataLength];
            stream.Read(_fileData, 0, _fileDataLength);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_fileDataLength);
            stream.Write(_fileData, 0, _fileDataLength);
            return stream;
        }
    }
}
