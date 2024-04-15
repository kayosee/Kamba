using Kamba.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Server
{
    public class Server
    {
        private int _port;
        private string _folder;
        private TcpListener _listener;
        private Thread _acceptor;
        private Thread _reader;
        private ConcurrentDictionary<int, TcpClient> _clients;
        public bool Start(int port, string folder)
        {
            _clients = new ConcurrentDictionary<int, TcpClient>();
            _folder = folder;
            _port = port;

            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
            _listener.Start();

            _acceptor = new Thread((ThreadStart) =>
            {
                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    _clients.TryAdd(_clients.Count, client);
                }
            });
            _acceptor.Name = "acceptor";
            _acceptor.Start();

            _reader = new Thread((ThreadStart) =>
            {
                while (true)
                {
                    foreach (var client in _clients.Values)
                    {
                        if(client.Client.Poll(TimeSpan.FromSeconds(5),SelectMode.SelectRead))
                        {
                            ThreadPool.QueueUserWorkItem(f=>ReadPacket(client));
                        }
                        else if (client.Client.Poll(TimeSpan.FromSeconds(5), SelectMode.SelectError))
                        {

                        }
                    }
                }
            });
            _reader.Name = "reader";
            _reader.Start();

            return true;
        }

        private void ReadPacket(TcpClient client)
        {
            long totalLength = 0;
            var packets = new List<Packet>();
            do
            {
                var packet = Packet.FromSocket(client.Client);
                totalLength += packet.SliceLength;
                packets.Add(packet);
                if (totalLength >= packet.TotalLength)
                    break;
            } while (true);

            var sessionData = SessionData.FromPackets(packets.ToArray());

        }
    }
}
