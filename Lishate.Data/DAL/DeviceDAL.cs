using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

using Lishate.Data.IDAL;
using Lishate.Data.Entity;

namespace Lishate.Data.DAL
{
    public class DeviceDAL
    {
        private DALHelper dh;
        private DalInterface di;

        public const string TABLE_NAME = "DeviceTable";
        public const string COLUMN_DEVICE_ID = "deviceid";
        public const string COLUMN_LASTUPDATE = "lastupdate";
        public const string COLUMN_DEVICE_TYPE = "devicetype";
        public const string COLUMN_PARENT_ID = "parentid";

        public const string PARAM_DEVICE_ID = "@deviceid";
        public const string PARAM_LASTUPDATE = "@lastupdate";
        public const string PARAM_DEVICE_TYPE = "@devicetype";
        public const string PARAM_PARENT_ID = "@parentid";

        public DeviceDAL()
        {
            dh = new DALHelper();
            di = DALFactory.GetDalInterface();
        }

        public void AddDevice(Device d)
        {
            string cmd = "INSERT INTO " + TABLE_NAME + " (" +
                COLUMN_DEVICE_ID + ", " +
                COLUMN_LASTUPDATE + ", " +
                COLUMN_DEVICE_TYPE + ", " +
                COLUMN_PARENT_ID + ") VALUES (" +
                PARAM_DEVICE_ID + ", " +
                PARAM_LASTUPDATE + ", " +
                PARAM_DEVICE_TYPE + ", " +
                PARAM_PARENT_ID + ")";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm,PARAM_DEVICE_ID,DbType.String,16,d.DeviceID,ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_LASTUPDATE, DbType.String, 16, d.LastUpdate, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_TYPE, DbType.Int32, 4, d.DeviceType, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_PARENT_ID, DbType.String,16, d.ParentId, ParameterDirection.Input));

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

        public void UpdateDevice(Device d)
        {
            string cmd = "UPDATE " + TABLE_NAME + " SET " +
                COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID + "," +
                COLUMN_LASTUPDATE + "=" + PARAM_LASTUPDATE + "," +
                COLUMN_DEVICE_TYPE + "=" + PARAM_DEVICE_TYPE + "," +
                COLUMN_PARENT_ID + "=" + PARAM_PARENT_ID + " WHERE " + 
                COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;// DT SET deviceid=@DeviceId, lastupdate=@LastUpdate Where deviceid=@DeviceId";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, d.DeviceID, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_LASTUPDATE, DbType.String, 16, d.LastUpdate, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_TYPE, DbType.Int32, 4, d.DeviceType, ParameterDirection.Input));
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_PARENT_ID, DbType.String, 16, d.ParentId, ParameterDirection.Input));

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
        

        public void DeleteDevice(Device d)
        {
            string cmd = "DELETE " + TABLE_NAME + " WHERE " + COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;// DT WHERE DeviceId=@DeviceId";

            using (DbConnection conn = di.getDbConnection(Config.Instance.ConnectString))
            {
                try
                {
                    DbCommand comm = di.getDbCommand(cmd);
                    comm.Parameters.Add(Utility.GetDbParameter(comm, PARAM_DEVICE_ID, DbType.String, 16, d.DeviceID, ParameterDirection.Input));
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

        public Device GetDevice(string deviceid)
        {
            string cmd = "SELECT * FROM " + TABLE_NAME + " WHERE " + COLUMN_DEVICE_ID + "=" + PARAM_DEVICE_ID;

            DbDataReader reader = null;
            Device d = new Device();
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
