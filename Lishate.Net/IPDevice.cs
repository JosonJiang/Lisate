using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Net
{
    public class IPDevice
    {
        private UInt32 _ipaddress = 0;
        public UInt32 IPAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
    }
}
