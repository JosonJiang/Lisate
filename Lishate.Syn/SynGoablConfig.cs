using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Net;
using Lishate.Config;

namespace Lishate.Syn
{
    public class SynGoablConfig : BaseConfig
    {
        private SynGoablConfig()
        {
            Console.WriteLine("Serve Gobal Config Init");
            Update();
            ConfigSubject.Instance.AddConfig(this);
        }

        //private int _count = 1;
        public int Count
        {
            get 
            {
                if (ipEndPoint == null)
                {
                    return 0;
                }
                else
                {
                    return ipEndPoint.Count;
                }
            }
        }


        private String _info = "";
        public String Info
        {
            //get { return _info; }
            set 
            { 
                _info = value;
                ChangeInfo2EndPoint();
            }
        }

        private ArrayList ipEndPoint = new ArrayList();
        public ArrayList ServerItemInfo
        {
            get { return ipEndPoint; }
        }

        private void ChangeInfo2EndPoint()
        {
            String[] infos = _info.Split("|".ToCharArray());
            String[] ipinfo;
            if (infos.Length > 0)
            {
                ipEndPoint.Clear();
                foreach (String s in infos)
                {
                    ipinfo = s.Split(":".ToCharArray());
                    if (ipinfo.Length == 2)
                    {
                        try
                        {
                            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipinfo[0]), int.Parse(ipinfo[1]));
                            if (ie.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ipEndPoint.Add(ie);
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
            GobalConfigInfo.Instance.ServerList = ServerItemInfo;

        }

        private const String _ConfigName = "Syn";
        private const String _Info = "info";
        private const String _Count = "count";

        public static readonly SynGoablConfig Instance = new SynGoablConfig();

        public override void Update()
        {
            //base.Update();
            try
            {
                NameValueCollection nvc = (NameValueCollection)ConfigurationSettings.GetConfig(_ConfigName);
                /*
                if (int.TryParse(nvc.Get(_Count), out _count))
                {
                    
                    if (_UpdateSendSize != null)
                    {
                        _UpdateSendSize(_sendSize);
                    }
                     
                }
                 * */
                Info = nvc.Get(_Info);
                /*
                if (int.TryParse(nvc.Get(_Info), out _info))
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
                 * */
            }
            catch (Exception e)
            {
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
        }
    }
}
