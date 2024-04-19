using Kamba.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        private SessionManager _sessionManager;
        public Server() 
        {
            _sessionManager = new SessionManager();
        }
        public bool Start(int port, string folder)
        {
            _folder = folder;
            _port = port;
            _sessionManager = new SessionManager();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
            _listener.Start();

            _acceptor = new Thread((ThreadStart) =>
            {
                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    _sessionManager.Add(client.Client);
                }
            });
            _acceptor.Name = "acceptor";
            _acceptor.Start();

            return true;
        }

    }
}
