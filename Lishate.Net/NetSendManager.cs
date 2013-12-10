using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Lishate.Log;
using Lishate.Message;

namespace Lishate.Net
{
    public class NetSendManager : NetManager
    {
        private int _sendSize = 8192;
        public int SendSize
        {
            get { return _sendSize; }
            set { _sendSize = value; }
        }

        private bool _isContinue = false;

        private int _byteRate = 128 * 1024;
        private int _perMSBitRate = 131;
        public int ByteRate
        {
            get { return _byteRate; }
            set
            {
                _byteRate = value;
                _perMSBitRate = _byteRate / 1000;
            }
        }

        private int _state = SERVER_IDLE;

        //private int _SendSocketSize = 8192;
        public int SendSocketSize
        {
            get { return _serverUdp.Client.SendBufferSize; }
            set { _serverUdp.Client.SendBufferSize = value; }
        }

        private UdpClient _serverUdp;
        public UdpClient UdpServer
        {
            get { return _serverUdp; }
            set { _serverUdp = value; }
        }

        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        public AutoResetEvent ResetEvent
        {
            get { return _resetEvent; }
            set { _resetEvent = value; }
        }

        public NetSendManager(UdpClient uc)
        {
            _serverUdp = uc;
        }


        private long _lastTicks = DateTime.Now.Ticks;
        private int _lastHasSize = 0;
        private int _flag = 0;

        private bool canSend(int size)
        {
            long temp = (DateTime.Now.Ticks - _lastTicks) / 10000;
            if (temp > 400)
            {
                temp = 400;
            }
            if (temp > 0)
            {
                if ((_perMSBitRate * temp) > (_lastHasSize + size))
                {
                    return true;
                }
            }
            return true;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    IpMessage im = (IpMessage)MessageInstance.getInstance().SendList.Get();
                    if (canSend(im.Msg.Length + 42))
                    {
                        _serverUdp.Send(im.Msg.Content, im.Msg.Length, im.EndPoint);
                        _lastHasSize = _lastHasSize + im.Msg.Length + 42;
                    }
                    else
                    {
                        _lastTicks = DateTime.Now.Ticks;
                        Thread.Sleep(1);
                        _lastHasSize = 0;
                    }
                }
                catch (Exception e)
                {
                    Log.Log.WriteErrorLog(e.Message);
                    Log.Log.WriteErrorLog(e.StackTrace);
                }
            }
        }
    }
}
