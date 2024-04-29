using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public abstract class Session
    {
        protected int _id;
        protected AuthenticateStatus _authenticateStatus;
        protected Socket _socket;
        public Session(Socket socket)
        {
            _id = (int)socket.Handle;
            _authenticateStatus = AuthenticateStatus.Unauthenticate;
            _socket = socket;
        }
        public int Id { get => _id; }
        public AuthenticateStatus Authenticated { get => _authenticateStatus; }
        public Socket Socket { get => _socket; }
        public T? ReadPacket<T>(int maxWaitSeconds = -1) where T : SessionData
        {
            long totalLength = 0;
            var packets = new List<Packet>();
            do
            {
                try
                {
                    var packet = Packet.FromSocket(_socket, maxWaitSeconds);
                    totalLength += packet.SliceLength;
                    packets.Add(packet);
                    if (totalLength >= packet.TotalLength)
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return default(T);
                }
            } while (true);

            return (T?)SessionData.FromPackets(packets.ToArray());
        }
        protected void WritePacket(SessionData data)
        {
            var packets = data.ToPackets();
            foreach (var packet in packets)
            {
                Socket.Send(packet.Serialize());
            }
        }
        public void Close()
        {
            _socket?.Close();
        }
    }
}
