using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using Lishate.Config;
using Lishate.Log;

namespace Lishate.Net
{
    public class NetGobalConfig : BaseConfig
    {
        public delegate void UpdateSize(int a);

        private UpdateSize _UpdateSendSize;
        private UpdateSize _UpdateRecvSize;
        private UpdateSize _UpdateBitRate;

        public UpdateSize UpdateSendSize
        {
            get { return _UpdateSendSize; }
            set { _UpdateSendSize = value; }
        }

        public UpdateSize UpdateRecvSize
        {
            get { return _UpdateRecvSize; }
            set { _UpdateRecvSize = value; }
        }

        public UpdateSize UpdateBitRate
        {
            get { return _UpdateBitRate; }
            set { _UpdateBitRate = value; }
        }
        private NetGobalConfig()
        {
            Console.WriteLine("Net Gobal Config Init");
            ConfigSubject.Instance.AddConfig(this);
        }

        private const String _ConfigName = "Net";
        private const String _SendSize = "sendsize";
        private const String _RecvSize = "recvsize";
        private const String _SendRate = "sendrate";

        public static readonly NetGobalConfig Instance = new NetGobalConfig();

        private int _NetPort = 0;
        public int NetPort
        {
            get { return _NetPort; }
            set { _NetPort = value; }
        }

        private UInt32 _IpAddress = 0;
        public UInt32 ServerIpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private int _sendSize = 8192;
        public int SendSize
        {
            get { return _sendSize; }
            //set { _sendSize = value; }
        }

        private int _recvSize = 8192;
        public int RecvSize
        {
            get { return _recvSize; }
            //set { _recvSize = value; }
        }


        private int _bitRate = 131072;
        public int BitRate
        {
            get { return _bitRate; }
            //set { _bitRate = value; }
        }

        public override void Update()
        {
            //base.Update();
            try
            {
                NameValueCollection nvc = (NameValueCollection)ConfigurationSettings.GetConfig(_ConfigName);
                if (int.TryParse(nvc.Get(_SendSize), out _sendSize))
                {
                    if (_UpdateSendSize != null)
                    {
                        _UpdateSendSize(_sendSize);
                    }
                }
                if (int.TryParse(nvc.Get(_RecvSize), out _recvSize))
                {
                    if (_UpdateRecvSize != null)
                    {
                        _UpdateRecvSize(_recvSize);
                    }
                }
                if (int.TryParse(nvc.Get(_SendRate), out _bitRate))
                {
                    if (_UpdateBitRate != null)
                    {
                        _UpdateBitRate(_bitRate);
                    }
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
