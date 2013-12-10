using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Lishate.Basedata
{
    public class MobInfo
    {
        private ulong _mobKey = 0;
        public ulong MobKey
        {
            get { return _mobKey; }
            set { _mobKey = value; }
        }

        private IPEndPoint _ipEnd;
        public IPEndPoint EndPoint
        {
            get { return _ipEnd; }
            set { _ipEnd = value; }
        }

        private DateTime _lastDatetime = DateTime.MinValue;
        public DateTime LastDateTime
        {
            get { return _lastDatetime; }
            set { _lastDatetime = value; }
        }
    }
}
