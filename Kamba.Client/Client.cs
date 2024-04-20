using Kamba.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kamba.Client
{
    public class Client : Session
    {
        private Thread _thread;
        public Client(Socket socket) : base(socket)
        {
            _authenticateStatus = AuthenticateStatus.Unauthenticate;
            var request = new AuthenticateRequest(0, 0, "kao", "123");
            WritePacket(request);

            _thread = new Thread(() =>
            {
                while (Socket.Poll(-1, SelectMode.SelectRead))
                {
                    ReadAndProcess();
                }
            });
            _thread.Start();
        }
        public override void Process(SessionData data)
        {
            switch (data.DataType)
            {
                case DataType.AuthenticateResponse:
                    {
                        DoAuthenticateResponse((AuthenticateResponse)data);
                        break;
                    }
                case DataType.FileReadResponse:
                    {
                        break;
                    }
            }
        }

        private void DoAuthenticateResponse(AuthenticateResponse data)
        {
            _id = data.ClientId;
            _authenticateStatus = data.Status;
            Console.WriteLine(Enum.GetName(typeof(AuthenticateStatus), data.Status));
        }
    }
}
