using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using Lishate.Message.Parse;

namespace Lishate.Server
{
    public class ServerManage
    {
        private Net.NetManager nm = new Net.NetManager();
        private ParseManage pm = new ParseManage();

        public void Start()
        {
            try
            {
                InitStart();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Stop()
        {

        }

        private void InitStart()
        {
            Lishate.Data.Config.Instance.Init();
            Lishate.Log.Config.Instance.Init();
            Lishate.Net.NetGobalConfig.Instance.Init();
            Lishate.Syn.SynGoablConfig.Instance.Init();
            pm.Init();
            nm.Init();
            nm.Start();
            pm.Start();

        }

        private ServerManage()
        {
        }

        public static readonly ServerManage Instance = new ServerManage();
    }
}
