using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

using Lishate.Data.IDAL;
using Lishate.Data.Entity;

namespace Lishate.Data.DAL
{
    public class DeviceSwitchDAL
    {
        private DalInterface di;

        public const string TABLE_NAME = "device_switch";
        public const string COLUMN_DEVICE_SWITCH_ID = "iddeviceswtich";
        public const string COLUMN_DEVICE_ID = "device_id";
        public const string COLUMN_ON_OFF_CONFIG = "onoffconfig";

        public const string PARAM_DEVICE_SWITCH_ID = "@iddeviceswtich";
        public const string PARAM_DEVICE_ID = "@device_id";
        public const string PARAM_ON_OFF_CONFIG = "@onoffconfig";

        public DeviceSwitchDAL()
        {
            di = DALFactory.GetDalInterface();
        }

        public void UpdateDeviceSwitch(Device_Switch ds)
        {
            string cmd = "UPDATE " + TABLE_NAME + " SET " +
                COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID + "," +
                COLUMN_ON_OFF_CONFIG + "=" + PARAM_ON_OFF_CONFIG + " WHERE " +
                COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;// DT SET deviceid=@DeviceId, lastupdate=@LastUpdate Where deviceid=@DeviceId";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, ds.DeviceID, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_ON_OFF_CONFIG, DbType.String, 60, ds.OnOffConfig, ParameterDirection.Input));

                    comm.Connection = conn;
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void AddDeviceSwitch(Device_Switch ds)
        {
            string cmd = "INSERT INTO " + TABLE_NAME + " (" +
                COLUMN_DEVICE_ID + ", " +
                COLUMN_ON_OFF_CONFIG + ") VALUES (" +
                PARAM_DEVICE_ID + ", " +
                PARAM_ON_OFF_CONFIG + ")";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, ds.DeviceID, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_ON_OFF_CONFIG, DbType.String, 60, ds.OnOffConfig, ParameterDirection.Input));

                    comm.Connection = conn;
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void DeleteDeviceSwitch(Device_Switch ds)
        {
            string cmd = "DELETE " + TABLE_NAME + " WHERE " + COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;// DT WHERE DeviceId=@DeviceId";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, ds.DeviceID, ParameterDirection.Input));
                    //comm.Parameters.Add(Utility.GetDbParameter(comm, "@LastUpdate", DbType.String, 16, d.LastUpdate, ParameterDirection.Input));

                    comm.Connection = conn;
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public DataTable GetAllTable()
        {
            string cmd = "SELECT * FROM " + TABLE_NAME;

            DataTable table = new DataTable();
            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Connection = conn;
                    DbDataAdapter adapter = di.getDataAdapter();
                    adapter.SelectCommand = comm;
                    conn.Open();
                    adapter.Fill(table);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return table;
        }

        public Device_Switch GetDevice(string deviceid)
        {
            string cmd = "SELECT * FROM " + TABLE_NAME + " WHERE " + COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;

            DbDataReader reader = null;
            Device_Switch d = new Device_Switch();
            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, d.DeviceID, ParameterDirection.Input));

                    comm.Connection = conn;
                    conn.Open();

                    reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        d.LoadReader(reader);
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return d;
        }
    }
}
