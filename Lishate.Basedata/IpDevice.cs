using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Basedata
{
    public class IpDevice
    {
        private UInt32 _ipaddress = 0;
        public UInt32 IpAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }

        private DeviceInfo _di;
        public DeviceInfo deviceInfo
        {
            get { return _di; }
            set { _di = value; }
        }
    }
}
