using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

namespace Lishate.Data.Entity
{
    public class Device_Switch
    {

        private string _device_id = "";
        public string DeviceID
        {
            get { return _device_id; }
            set { _device_id = value; }
        }

        private String _on_off_config = "";
        public String OnOffConfig
        {
            get { return _on_off_config; }
            set { _on_off_config = value; }
        }

        public void LoadReader(DbDataReader reader)
        {
            _device_id = reader.GetString(0);
            _on_off_config = reader.GetString(1);
        }

        public void LoadReader(DataRow reader)
        {
            _device_id = (string)reader[0];
            _on_off_config = (string)reader[1];
        }
    }
}
