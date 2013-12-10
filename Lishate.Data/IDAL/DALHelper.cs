using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

namespace Lishate.Data.IDAL
{
    public class DALHelper
    {
        public DALHelper()
        {
            dInterface = DALFactory.GetDalInterface();
            conn = dInterface.getDbConnection(Config.Instance.ConnectString);
        }

        private DalInterface dInterface;
        private DbConnection conn;

        public static DbConnection CreateConnection()
        {
            DbConnection conn = DALFactory.GetDalInterface().getDbConnection(Config.Instance.ConnectString);
            return conn;
        }

        public static DbConnection CreateConnection(string connectstr)
        {
            DbConnection conn = DALFactory.GetDalInterface().getDbConnection(connectstr);
            return conn;
        }

        public DbCommand GetStoredProcCommand(string storceProc)
        {
            DbCommand comm = conn.CreateCommand();
            comm.CommandText = storceProc;
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }

        public DbCommand GetSqlStringCommand(string sqlQuery)
        {
            DbCommand comm = conn.CreateCommand();
            comm.CommandText = sqlQuery;
            comm.CommandType = CommandType.Text;
            return comm;
        }

        public void AddParameterCollection(DbCommand command, DbParameterCollection dbCollection)
        {
            foreach (DbParameter parameter in dbCollection)
            {
                command.Parameters.Add(parameter);
            }
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);

        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);

        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbDataAdapter dbDataAdapter = dInterface.getDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbDataAdapter dbDataAdapter = dInterface.getDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }

        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter dbDataAdapter = dInterface.getDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter dbDataAdapter = dInterface.getDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;

        }

        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }

    }
}
