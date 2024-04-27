using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common.Request
{
    public class AuthenticateRequest : SessionRequest
    {
        private string _username;
        private string _password;
        private int _usernameLength;
        private int _passwordLength;
        public AuthenticateRequest(int clientId, long requestId, string username, string password) : base(DataType.AuthenticateRequest, clientId, requestId)
        {
            _username = username;
            _password = password;
        }
        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(_username);
            _usernameLength = buffer.Length;
            stream.Write(_usernameLength);
            stream.Write(buffer, 0, buffer.Length);

            buffer = Encoding.UTF8.GetBytes(_password);
            _passwordLength = buffer.Length;
            stream.Write(_passwordLength);
            stream.Write(buffer, 0, buffer.Length);

            return stream;
        }
        public AuthenticateRequest(ByteArrayStream stream) : base(stream)
        {
            _usernameLength = stream.ReadInt32();
            byte[] buffer = new byte[_usernameLength];
            stream.Read(buffer, 0, _usernameLength);
            _username = Encoding.UTF8.GetString(buffer).TrimEnd('\0');

            _passwordLength = stream.ReadInt32();
            buffer = new byte[_passwordLength];
            stream.Read(buffer, 0, _passwordLength);
            _password = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
        }
    }
}
