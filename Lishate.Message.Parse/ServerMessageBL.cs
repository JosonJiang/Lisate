using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Log;
using Lishate.Message;

namespace Lishate.Message.Parse
{
    public class ServerMessageBL
    {
        public void Run()
        {
            while (true)
            {
                try
                {
                    IpMessage im = (IpMessage)MessageInstance.getInstance().ServerList.Get();

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
