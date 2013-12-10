using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lishate.Test
{
    public class UdpServer
    {
        private IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 12188);
        private UdpClient client;

        private int _isStart = 0;

        private int _refCount = 0;
        public int RefCount
        {
            get { return _refCount; }
        }

        public int ServerBufferSize
        {
            get { return client.Client.ReceiveBufferSize; }
            set { client.Client.ReceiveBufferSize = value; }
        }

        public UdpServer()
        {
            try
            {
                client = new UdpClient(remotePoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            //client.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoChecksum, true);
        }

        public void SetRcvBufferSize(int buffersize)
        {
            client.Client.ReceiveBufferSize = buffersize;
        }

        private byte[] buf = new byte[1024 * 1024];

        public void StartRcv()
        {
            if (_isStart == 0)
            {
                _isStart = 1;
                client.BeginReceive(new AsyncCallback(AsyEndRecive), client);
            }
        }

        private void AsyEndRecive(IAsyncResult iar)
        {
            IPEndPoint ipp = new IPEndPoint(IPAddress.Any,0);
            client.EndReceive(iar,ref ipp);
            if (_isStart != 0)
            {
                client.BeginReceive(new AsyncCallback(AsyEndRecive), client);
                Interlocked.Increment(ref _refCount);
            }
        }
    }
}
