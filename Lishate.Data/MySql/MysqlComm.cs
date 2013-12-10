using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

using Lishate.Data.IDAL;

namespace Lishate.Data.MySql
{
    public class MysqlComm : DalInterface
    {
        public DbDataAdapter getDataAdapter()
        {
            //return base.getDataAdapter();
            return new MySqlDataAdapter();
        }

        public DbDataAdapter getDataAdapter(DbCommand command)
        {
            //return base.getDataAdapter(command);
            return new MySqlDataAdapter((MySqlCommand)command);
        }

        public DbDataAdapter getDataAdapter(string command, DbConnection connection)
        {
            //return base.getDataAdapter(command, connection);
            return new MySqlDataAdapter(command, (MySqlConnection)connection);
        }

        public DbDataAdapter getDataAdapter(string command, string connstr)
        {
            //return base.getDataAdapter(command, connstr);
            return new MySqlDataAdapter(command, connstr);
        }

        public DbCommand getDbCommand()
        {
            //return base.getDbCommand();
            return new MySqlCommand();
        }

        public DbCommand getDbCommand(DbConnection conn)
        {
            //return base.getDbCommand(conn);
            return null;
        }

        public DbCommand getDbCommand(string commandstr)
        {
            //return base.getDbCommand(commandstr);
            return new MySqlCommand(commandstr);
        }

        public DbCommand getDbCommand(string commandstr, DbConnection conn)
        {
            //return base.getDbCommand(commandstr, conn);
            return new MySqlCommand(commandstr, (MySqlConnection)conn);
        }

        public DbCommand getDbCommand(string commandstr, DbConnection conn, DbTransaction tran)
        {
            //return base.getDbCommand(commandstr, conn, tran);
            return new MySqlCommand(commandstr, (MySqlConnection)conn, (MySqlTransaction)tran);
        }

        public DbCommandBuilder getDbCommandBuilder()
        {
            //return base.getDbCommandBuilder();
            return new MySqlCommandBuilder();
        }

        public DbCommandBuilder getDbCommandBuiler(DbDataAdapter adapter)
        {
            //return base.getDbCommandBuiler(adapter);
            return new MySqlCommandBuilder((MySqlDataAdapter)adapter);
        }

        public DbConnection getDbConnection()
        {
            //return base.getDbConnection();
            return new MySqlConnection();
        }

        public DbConnection getDbConnection(string connstr)
        {
            //return base.getDbConnection(connstr);
            return new MySqlConnection(connstr);
        }
    }
}
