using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;

namespace Lishate.Config
{
    public class GobalConfigInfo : BaseConfig
    {
        private ArrayList _serveList = new ArrayList();
        public ArrayList ServerList
        {
            get { return _serveList; }
            set { _serveList = value; }
        }

        private GobalConfigInfo()
        {
        }

        public static readonly GobalConfigInfo Instance = new GobalConfigInfo();
    }
}
