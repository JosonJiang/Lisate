using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Basedata;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using Lishate.Log;
using Lishate.Message;

namespace Lishate.Net
{
    public class NetManager : ServerManager
    {
        public void UpdateSendSize(int sendsize)
        {
            SendBufSize = sendsize;
        }

        public void UpdateRecvSize(int recvsize)
        {
            RecvBufSize = recvsize;
        }

        public void UpdateRate(int rate)
        {
            ByteRate = rate;
        }

        public NetManager()
        {
            NetGobalConfig.Instance.UpdateSendSize = UpdateSendSize;
            NetGobalConfig.Instance.UpdateRecvSize = UpdateRecvSize;
            NetGobalConfig.Instance.UpdateBitRate = UpdateRate;
        }

        private int _state = SERVER_STOP;

        public override int GetState()
        {
            return _state;
        }

        public override void Pause()
        {
            _state = SERVER_PAUSE;
        }

        public override void Restart()
        {
            _state = SERVER_RUN;
        }

        public override void Start()
        {
            _state = SERVER_RUN;
            Init();
            if (_sendThread.ThreadState == ThreadState.Unstarted)
            {
                _sendThread.Start();
            }
        }

        public override void Stop()
        {
            _state = SERVER_STOP;
            SendStop();
        }

        

        private Thread _sendThread;

        private UdpClient _serverClient;
        private NetSendManager _nsm;
        private NetRcvManager _nrm;
        private int _serverPort = 5000;

        public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }

        public int SendBufSize
        {
            get
            {
                if (_nsm != null)
                {
                    return _nsm.SendBufSize;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (_nsm != null)
                {
                    _nsm.SendBufSize = value;
                }
            }
        }

        public int RecvBufSize
        {
            get
            {
                if (_nrm != null)
                {
                    return _nrm.RecvBufferSize;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (_nrm != null)
                {
                    _nrm.RecvBufferSize = value;
                }
            }
        }

        public int ByteRate
        {
            get
            {
                if (_nsm != null)
                {
                    return _nsm.ByteRate;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (_nsm != null)
                {
                    _nsm.ByteRate = value;
                }
            }
        }

        private bool InitServerClient()
        {
            try
            {
                if (_serverClient == null)
                {
                    _serverClient = new UdpClient(new IPEndPoint(IPAddress.Any, _serverPort));
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("Init Server Port Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            return false;
        }

        private bool InitRecv()
        {
            try
            {
                if (_nrm == null)
                {
                    _nrm = new NetRcvManager(_serverClient);
                }
                _serverClient.BeginReceive(new AsyncCallback(AsyncEndRecv), _serverClient);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("Init Server Recv Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            return false;
        }

        private void AsyncEndRecv(IAsyncResult iar)
        {
            try
            {
                
                IPEndPoint ipp = new IPEndPoint(IPAddress.Any, 0);
                byte[] msg = _serverClient.EndReceive(iar, ref ipp);
                try
                {
                    _serverClient.BeginReceive(new AsyncCallback(AsyncEndRecv), _serverClient);
                }
                catch (Exception e)
                {
                    Log.Log.WriteSystemLog("Server Re BeginReceive ERROR, So Can't Receive");
                    Log.Log.WriteErrorLog(e.Message);
                    Log.Log.WriteErrorLog(e.StackTrace);
                }
                BaseMessage bm = MessageFactroy.GetMesssage(msg, 0);
                if (bm != null)
                {
                    IpMessage im = new IpMessage();
                    im.EndPoint = ipp;
                    im.Msg = bm;
                    MessageInstance.getInstance().RecvList.Add(im);
                }
            }
            catch (Exception e)
            {
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
        }

        private bool InitSend()
        {
            try
            {
                if (_nsm == null)
                {
                    _nsm = new NetSendManager(_serverClient);
                }
                if (_sendThread == null)
                {
                    _sendThread = new Thread(new ThreadStart(_nsm.Run));
                }
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("Send Init Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            return false;
        }

        private void SendStop()
        {
            try
            {
                _sendThread.Abort();

            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("Send Stop Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            finally
            {
                _sendThread = null;
            }
        }



        public bool Init()
        {
            try
            {
                //_serverClient = new UdpClient(new IPEndPoint(IPAddress.Any, _serverPort));
                if (!InitServerClient())
                {
                    return false;
                }
                if (!InitRecv())
                {
                    return false;
                }
                if (!InitSend())
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
                return false;
            }
        }

    }
}
