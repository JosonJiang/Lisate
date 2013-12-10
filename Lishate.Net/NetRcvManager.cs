using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Lishate.Net
{
    public class NetRcvManager
    {
        private UdpClient _serverClient;
        public UdpClient ServerClient
        {
            get { return _serverClient; }
            set { _serverClient = value; }
        }

        public int RecvBufferSize
        {
            get { return _serverClient.Client.ReceiveBufferSize; }
            set { _serverClient.Client.ReceiveBufferSize = value; }
        }

        public NetRcvManager(UdpClient uc)
        {
            _serverClient = uc;
        }


    }
}
