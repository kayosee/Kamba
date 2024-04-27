using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class FileReadResponse : FileReadRequest
    {
        protected byte[] _fileData;
        protected int _fileDataLength;
        protected byte _fileResponseType;
        private string _error;
        protected int _errorLength;
        public byte[] FileData { get => _fileData; set { _fileData = value; _fileDataLength = value.Length; } }
        public FileResponseType FileResponseType { get => (FileResponseType)_fileResponseType; set => _fileResponseType = (byte)value; }
        public string Error { get => _error; set => _error = value; }

        public FileReadResponse(int clientId, long requestId, string path, long position, int size, FileResponseType fileResponseType) : base(clientId, requestId, path, position, size)
        {
            _dataType = (byte)DataType.FileReadResponse;
            _fileResponseType = (byte)fileResponseType;
            _error = "";
            _errorLength = 0;
        }
        public FileReadResponse(ByteArrayStream stream) : base(stream)
        {
            _fileResponseType = stream.ReadByte();
            _fileDataLength = stream.ReadInt32();
            _fileData = new byte[_fileDataLength];
            stream.Read(_fileData, 0, _fileDataLength);
            _errorLength = stream.ReadInt32();
            var buffer = new byte[_errorLength];
            stream.Read(buffer, 0, buffer.Length);
            _error = Encoding.UTF8.GetString(buffer);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(_fileResponseType);
            stream.Write(_fileDataLength);
            stream.Write(_fileData, 0, _fileDataLength);
            var buffer = Encoding.UTF8.GetBytes(_error);
            _errorLength = buffer.Length;
            stream.Write(_errorLength);
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}
