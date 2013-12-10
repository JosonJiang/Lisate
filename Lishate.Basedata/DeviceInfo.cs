using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Lishate.Basedata
{
    public class DeviceInfo
    {
        private IPEndPoint _point;
        public IPEndPoint IPPoint
        {
            get { return _point; }
            set { _point = value; }
        }

        private ulong _deviceId = 0;
        public ulong DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        private int _type = 0;
        public int DeviceType
        {
            get { return _type; }
            set { _type = value; }
        }

        private ulong _parentId = 0;
        public ulong ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private DateTime _lastDatetime = DateTime.MinValue;
        public DateTime LastDateTime
        {
            get { return _lastDatetime; }
            set { _lastDatetime = value; }
        }


    }
}
