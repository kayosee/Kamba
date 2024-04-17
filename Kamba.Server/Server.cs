using Kamba.Common;
using System;
using System.Collections;
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
        private List<Socket> _clientsList;
        private ManualResetEvent _connected;
        public bool Start(int port, string folder)
        {
            _clientsList = new List<Socket>();
            _connected = new ManualResetEvent(false);
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
                    _connected.Set();
                    _clientsList.Add(client.Client);
                    _clients.TryAdd(_clients.Count, client);
                }
            });
            _acceptor.Name = "acceptor";
            _acceptor.Start();

            _reader = new Thread((ThreadStart) =>
            {
                while (_connected.WaitOne())
                {
                    var temp = _clientsList.ToList();
                    Socket.Select(temp, null, null, -1);
                    foreach (Socket client in temp)
                    {
                        ReadPacket(client);
                    }
                }
            });
            _reader.Name = "reader";
            _reader.Start();

            return true;
        }

        private void ReadPacket(Socket socket)
        {
            long totalLength = 0;
            var packets = new List<Packet>();
            do
            {
                try
                {
                    var packet = Packet.FromSocket(socket);
                    totalLength += packet.SliceLength;
                    packets.Add(packet);
                    if (totalLength >= packet.TotalLength)
                        break;
                }
                catch (Exception ex)
                {
                    continue;
                }
            } while (true);

            var sessionData = SessionData.FromPackets(packets.ToArray());
        }
    }
}
