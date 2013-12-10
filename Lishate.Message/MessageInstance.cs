using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Basedata;

namespace Lishate.Message
{
    public class MessageInstance
    {
        private MessageInstance()
        {
        }

        private static MessageInstance _instance = new MessageInstance();

        public static MessageInstance getInstance()
        {
            return _instance;
        }

        private SynList _RecvList = new SynList();
        public SynList RecvList
        {
            get { return _RecvList; }
            set { _RecvList = value; }
        }

        private SynList _SendList = new SynList();
        public SynList SendList
        {
            get { return _SendList; }
            set { _SendList = value; }
        }

        private SynList _ServerList = new SynList();
        public SynList ServerList
        {
            get { return _ServerList; }
            set { _ServerList = value; }
        }
    }
}
