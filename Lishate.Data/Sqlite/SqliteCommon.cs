using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;

using Lishate.Data.IDAL;

namespace Lishate.Data.Sqlite
{
    public class SqliteCommon : DalInterface
    {
        public DbDataAdapter getDataAdapter()
        {
            //return base.getDataAdapter();
            return new SQLiteDataAdapter();
        }

        public DbDataAdapter getDataAdapter(string command, DbConnection connection)
        {
            //return base.getDataAdapter(command, connection);
            return new SQLiteDataAdapter(command,(SQLiteConnection)connection);
        }

        public DbDataAdapter getDataAdapter(string command, string connstr)
        {
            //return base.getDataAdapter(command, connstr);
            return new SQLiteDataAdapter(command, connstr);
        }

        public DbDataAdapter getDataAdapter(DbCommand command)
        {
            //return base.getDataAdapter(command);
            return new SQLiteDataAdapter((SQLiteCommand)command);
        }

        public DbCommand getDbCommand()
        {
            //return base.getDbCommand();
            return new SQLiteCommand();
        }

        public DbCommand getDbCommand(DbConnection conn)
        {
            //return base.getDbCommand(conn);
            return new SQLiteCommand((SQLiteConnection)conn);
        }

        public DbCommand getDbCommand(string commandstr)
        {
            //return base.getDbCommand(commandstr);
            return new SQLiteCommand(commandstr);
        }

        public DbCommand getDbCommand(string commandstr, DbConnection conn)
        {
            //return base.getDbCommand(commandstr, conn);
            return new SQLiteCommand(commandstr, (SQLiteConnection)conn);
        }

        public DbCommand getDbCommand(string commandstr, DbConnection conn, DbTransaction tran)
        {
            //return base.getDbCommand(commandstr, conn, tran);
            return new SQLiteCommand(commandstr, (SQLiteConnection)conn, (SQLiteTransaction)tran);
        }

        public DbCommandBuilder getDbCommandBuilder()
        {
            //return base.getDbCommandBuilder();
            return new SQLiteCommandBuilder();
        }

        public DbCommandBuilder getDbCommandBuiler(DbDataAdapter adapter)
        {
            //return base.getDbCommandBuiler(adapter);
            return new SQLiteCommandBuilder((SQLiteDataAdapter)adapter);
        }

        public DbConnection getDbConnection()
        {
            //return base.getDbConnection();
            return new SQLiteConnection();
        }

        public DbConnection getDbConnection(string connstr)
        {
            //return base.getDbConnection(connstr);
            return new SQLiteConnection(connstr);
        }
    }
}
