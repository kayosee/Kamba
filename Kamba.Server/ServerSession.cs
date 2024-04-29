using Kamba.Common;
using Kamba.Common.Request;
using Kamba.Common.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Server
{
    public class ServerSession : Session
    {
        public ServerSession(Socket socket) : base(socket)
        {
        }
        public void Process()
        {
            var packet = ReadPacket<SessionData>();
            if (packet == null)
            {
                return;
            }
            if (packet.DataType == DataType.AuthenticateRequest)
            {
                DoAuthenticateRequest((AuthenticateRequest)packet);
                return;
            }

            if (_authenticateStatus != AuthenticateStatus.Success)
            {
                var request = (SessionRequest)packet;
                var response = new AuthenticateResponse(request.ClientId, request.RequestId, DateTime.Now.Ticks, _authenticateStatus);
                Socket.Send(response.Serialize());
                return;
            }

            switch (packet.DataType)
            {
                case DataType.FileReadRequest:
                    {
                        DoFileReadRequest((FileReadRequest)packet);
                        break;
                    }
            }
        }
        private void DoFileReadRequest(FileReadRequest data)
        {
            var folder = ConfigurationManager.AppSettings["folder"];
            var path = Path.Combine(folder, data.Path);
            FileReadResponse response = null;
            if (File.Exists(path))
            {
                try
                {
                    using (var file = File.OpenRead(path))
                    {
                        var buffer = new byte[data.Size];
                        file.Seek(data.Position, SeekOrigin.Begin);
                        var nret = file.Read(buffer, 0, data.Size);
                        response = new FileReadResponse(data.ClientId, data.RequestId, data.Path, data.Position, nret, FileResponseType.FileContent);
                        response.FileData = buffer;
                    }
                }
                catch (Exception ex)
                {
                    response = new FileReadResponse(data.ClientId, data.RequestId, data.Path, data.Position, 0, FileResponseType.FileReadError);
                    response.Error = ex.Message;
                }
            }
            else
            {
                response = new FileReadResponse(data.ClientId, data.RequestId, data.Path, data.Position, 0, FileResponseType.FileNotExists);
            }

            if (response != null)
                WritePacket(response);

        }
        private void DoAuthenticateRequest(AuthenticateRequest data)
        {
            var username = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            var response = new AuthenticateResponse(data.ClientId, data.RequestId, DateTime.Now.Ticks, AuthenticateStatus.Fail);
            response.Status = (username == data.Username && password == data.Password) ? AuthenticateStatus.Success : AuthenticateStatus.Fail;
            WritePacket(response);
        }
    }
}
