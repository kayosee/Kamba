using Kamba.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Server
{
    public class SessionManager
    {
        private ConcurrentDictionary<int, ServerSession> _sessions;
        private ManualResetEvent _ready;
        private Thread _reader;
        public SessionManager()
        {
            _ready = new ManualResetEvent(false);
            _sessions = new ConcurrentDictionary<int, ServerSession>();
            _reader = new Thread((e) =>
            {
                while (_ready.WaitOne())
                {
                    var sockets = _sessions.ToArray().Select(f => f.Value.Socket).ToList();
                    Socket.Select(sockets, null, null, -1);
                    foreach (var socket in sockets)
                    {
                        _sessions[(int)socket.Handle].ReadPacket();
                    }
                }
            });
            _reader.Name = "reader";
            _reader.IsBackground = true;
            _reader.Start();
        }
        public void Add(Socket socket)
        {
            _sessions.TryAdd((int)socket.Handle, new ServerSession(socket));
            _ready.Set();
        }
        public void Close(int clientId)
        {
            if (_sessions.TryRemove(clientId, out var session))
            {
                session.Close();
                if (_sessions.Count == 0)
                    _ready.Reset();
            }
        }
    }
}
