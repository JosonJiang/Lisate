using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

namespace Lishate.Data.Entity
{
    public class Device
    {
        private string _deviceid;
        public string DeviceID
        {
            get { return _deviceid; }
            set { _deviceid = value; }
        }

        private string _lastupdate;
        public String LastUpdate
        {
            get { return _lastupdate; }
            set { _lastupdate = value; }
        }

        private int _deviceType = 0;
        public int DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }

        private string _parentId = "";
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public void LoadReader(DbDataReader reader)
        {
            _deviceid = (string)reader.GetValue(0);
            _lastupdate = (string)reader.GetValue(1);
            _deviceType = (int)reader.GetValue(2);
            _parentId = (string)reader.GetValue(3);
        }

        public void LoadReader(DataRow row)
        {
            _deviceid = (string)row[0];
            _lastupdate = (string)row[1];
            _deviceType = (int)row[2];
            _parentId = (string)row[3];
        }
    }
}
