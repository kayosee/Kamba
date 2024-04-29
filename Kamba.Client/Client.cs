using Kamba.Common;
using Kamba.Common.Request;
using Kamba.Common.Response;
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
        private string _username;
        private string _password;
        public Client(Socket socket, string username, string password) : base(socket)
        {
            _authenticateStatus = AuthenticateStatus.Unauthenticate;
            _username = username;
            _password = password;
            DoAuthenticate();
        }

        private void DoAuthenticate()
        {
            var request = new AuthenticateRequest(0, 0, _username, _password);
            var response = MakeRequest<AuthenticateResponse>(request) as AuthenticateResponse;
            if (response != null)
            {
                _id = response.ClientId;
                _authenticateStatus = response.Status;
                Console.WriteLine(Enum.GetName(typeof(AuthenticateStatus), response.Status));
            }
            else
            {
                Console.WriteLine("connect error");
            }
        }

        public T? MakeRequest<T>(SessionRequest request, int maxWaitSeconds = -1) where T : SessionData
        {
            WritePacket(request);
            return ReadPacket<T>(maxWaitSeconds);
        }
    }
}
