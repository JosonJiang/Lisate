using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lishate.Basedata
{
    public class IpDeviceTable
    {
        private Hashtable _hashTable = new Hashtable();

        public void SetIpDevice(IpDevice id)
        {
            _hashTable[id.IpAddress] = id;
            //id.deviceInfo.IpAddress = id.IpAddress;
        }

        public IpDevice GetIpDevice(UInt32 ip)
        {
            return (IpDevice)_hashTable[ip];
        }

        public void Remove(UInt32 ipaddress)
        {
            _hashTable.Remove(ipaddress);
        }


    }
}
