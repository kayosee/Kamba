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
        public Client(Socket socket) : base(socket)
        {
            _authenticateStatus = AuthenticateStatus.Unauthenticate;
            var request = new AuthenticateRequest(0, 0, "kao", "123");
            MakeRequest(request, (r) =>
            {
                if (r is AuthenticateResponse)
                    DoAuthenticateResponse((AuthenticateResponse)r);
            });
        }

        private void DoAuthenticateResponse(AuthenticateResponse data)
        {
            _id = data.ClientId;
            _authenticateStatus = data.Status;
            Console.WriteLine(Enum.GetName(typeof(AuthenticateStatus), data.Status));
        }

        public SessionResponse? MakeRequest(SessionRequest request)
        {
            WritePacket(request);
            return (SessionResponse?)ReadPacket();
        }
    }
}
