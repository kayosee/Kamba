﻿using System;
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
        public int Id { get => _id; set => _id = value; }
        public AuthenticateStatus Authenticated { get => _authenticateStatus; }
        public Socket Socket { get => _socket; }
        public void ReadAndProcess()
        {
            var data = ReadPacket();
            if (data != null)
            {
                Process(data);
            }
        }
        public SessionData? ReadPacket()
        {
            long totalLength = 0;
            var packets = new List<Packet>();
            do
            {
                try
                {
                    var packet = Packet.FromSocket(_socket);
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

            return SessionData.FromPackets(packets.ToArray());
        }
        public abstract void Process(SessionData data);
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