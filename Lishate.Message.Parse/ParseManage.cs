using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lishate.Message.Parse
{
    public class ParseManage
    {
        private Thread _msgParseThread;
        private Thread _serverParseThead;

        private MessageParseBL mpb = new MessageParseBL();
        private ServerMessageBL smb = new ServerMessageBL();

        public void Init()
        {
            try
            {
                if (_msgParseThread == null)
                {
                    _msgParseThread = new Thread(new ThreadStart(mpb.Run));
                }
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("msg parse Init Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            try
            {
                if (_serverParseThead == null)
                {
                    _serverParseThead = new Thread(new ThreadStart(smb.Run));
                }
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("server msg parse Init Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }

        }

        public void Start()
        {
            try
            {
                _msgParseThread.Start();
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("msg parse start Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            try
            {
                _serverParseThead.Start();
            }
            catch (Exception e)
            {
                Log.Log.WriteSystemLog("msg parse start Error");
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
        }

        public void Stop()
        {
            try
            {
                _msgParseThread.Abort();
            }
            catch (Exception e)
            {
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            finally
            {
                _msgParseThread = null;
            }
            try
            {
                _serverParseThead.Abort();
            }
            catch (Exception e)
            {
                Log.Log.WriteErrorLog(e.Message);
                Log.Log.WriteErrorLog(e.StackTrace);
            }
            finally
            {
                _serverParseThead = null;
            }
        }
    }
}
