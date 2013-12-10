using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace Lishate.Server
{
    public class ServerManage
    {
        private Net.NetManager nm = new Net.NetManager();

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
            nm.Init();
            nm.Start();

        }
    }
}
