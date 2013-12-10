using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Basedata
{
    public class BaseInfoInstance
    {
        private BaseInfoInstance()
        {
        }

        private static BaseInfoInstance _instance = new BaseInfoInstance();

        public static BaseInfoInstance getInstance()
        {
            return _instance;
        }

        private DeviceTable _deviceInfo = new DeviceTable();
        public DeviceTable DeviceInfo
        {
            get { return _deviceInfo; }
            set { _deviceInfo = value; }
        }

        private MobDeviceTable _mobInfo = new MobDeviceTable();
        public MobDeviceTable MobInfo
        {
            get { return _mobInfo; }
            set { _mobInfo = value; }
        }
    }
}
