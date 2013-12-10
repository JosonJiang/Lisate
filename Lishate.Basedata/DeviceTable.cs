using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lishate.Basedata
{
    public class DeviceTable
    {
        private Hashtable _deviceHash = Hashtable.Synchronized(new Hashtable());

        public DeviceInfo GetDeviceInfo(ulong deviceId)
        {
            return (DeviceInfo)_deviceHash[deviceId];
        }

        public bool SetDeviceInfo(DeviceInfo di)
        {
            lock (_deviceHash.SyncRoot)
            {
                if (_deviceHash.ContainsKey(di.DeviceID))
                {
                    _deviceHash[di.DeviceID] = di;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddDeviceInfo(DeviceInfo di)
        {
            lock (_deviceHash.SyncRoot)
            {
                if (_deviceHash.ContainsKey(di.DeviceID))
                {
                    return false;
                }
                else
                {
                    _deviceHash.Add(di.DeviceID, di);
                    return true;
                }
            }
        }

        public int GetCount()
        {
            return _deviceHash.Count;
        }

        public void Remove(DeviceInfo di)
        {
            _deviceHash.Remove(di.DeviceID);
        }

    }
}
