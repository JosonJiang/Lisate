using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Lishate.Message;

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

        /*
        public void Start()
        {
            _serverClient.BeginReceive(new AsyncCallback(AsyEndRecive), _serverClient);
        }

        private void AsyEndRecive(IAsyncResult iar)
        {
            IPEndPoint ipp = new IPEndPoint(IPAddress.Any, 0); 
            byte[] buf = _serverClient.EndReceive(iar, ref ipp);
            _serverClient.BeginReceive(new AsyncCallback(AsyEndRecive), _serverClient);
            if (BaseMessage.CheckIsMessage(buf, 0))
            {
                BaseMessage bm = new BaseMessage();
                bm.SetupBuf(buf, 0);
                IpMessage im = new IpMessage();
                im.Msg = bm;
                im.EndPoint = ipp;
                MessageInstance.getInstance().RecvList.Add(im);
            }
            
        }
         * */
    }
}
