using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lishate.Basedata
{
    public class MobDeviceTable
    {
        private Hashtable _deviceHash = Hashtable.Synchronized(new Hashtable());

        public MobInfo GetMobInfo(ulong deviceId)
        {
            return (MobInfo)_deviceHash[deviceId];
        }

        public bool SetMobInfo(MobInfo di)
        {
            lock (_deviceHash.SyncRoot)
            {
                if (_deviceHash.ContainsKey(di.MobKey))
                {
                    _deviceHash[di.MobKey] = di;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddMobInfo(MobInfo di)
        {
            lock (_deviceHash.SyncRoot)
            {
                if (_deviceHash.ContainsKey(di.MobKey))
                {
                    return false;
                }
                else
                {
                    _deviceHash.Add(di.MobKey, di);
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
