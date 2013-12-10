using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Lishate.Data.IDAL;

namespace Lishate.Data.Sqlserver
{
    public class SqlserverCommon : DalInterface
    {
        public DbDataAdapter getDataAdapter()
        {
            //return base.getDataAdapter();

            return new SqlDataAdapter();
        }

        public DbDataAdapter getDataAdapter(DbCommand command)
        {
            //return base.getDataAdapter(command);
            return new SqlDataAdapter((SqlCommand)command);
        }

        public DbDataAdapter getDataAdapter(string command, DbConnection connection)
        {
            //return base.getDataAdapter(command, connection);
            return new SqlDataAdapter(command, (SqlConnection)connection);
        }

        public DbDataAdapter getDataAdapter(string command, string connstr)
        {
            //return base.getDataAdapter(command, connstr);
            return new SqlDataAdapter(command, connstr);
        }

        public DbCommand getDbCommand()
        {
            return new SqlCommand();
        }

        public DbCommand getDbCommand(string commandstr)
        {
            return new SqlCommand(commandstr);
        }

        public DbCommand getDbCommand(DbConnection conn)
        {
            return null;
        }
        public DbCommand getDbCommand(string commandstr, DbConnection conn)
        {
            return new SqlCommand(commandstr, (SqlConnection)conn);
        }

        public DbCommand getDbCommand(string commandstr, DbConnection conn, DbTransaction tran)
        {
            return new SqlCommand(commandstr, (SqlConnection)conn, (SqlTransaction)tran);
        }

        public DbCommandBuilder getDbCommandBuilder()
        {
            return new SqlCommandBuilder();
        }

        public DbCommandBuilder getDbCommandBuiler(DbDataAdapter adapter)
        {
            return new SqlCommandBuilder((SqlDataAdapter)adapter);
        }

        public DbConnection getDbConnection()
        {
            return new SqlConnection();
        }
        public DbConnection getDbConnection(string connstr)
        {
            return new SqlConnection(connstr);
        }
    }
}
