using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;

namespace Lishate.Data.IDAL
{
    public  interface DalInterface
    {
        //SQLiteConnection conn = new SQLiteConnection(

        //public SQLiteTransaction tran = new SQLiteTransaction
        //SQLiteDataReader reader = new SQLiteDataReader
        #region connection
        DbConnection getDbConnection();
        DbConnection getDbConnection(string connstr);
        #endregion

        #region command
        DbCommand getDbCommand();
        DbCommand getDbCommand(string commandstr);
        DbCommand getDbCommand(DbConnection conn);
        DbCommand getDbCommand(string commandstr, DbConnection conn);
        DbCommand getDbCommand(string commandstr, DbConnection conn, DbTransaction tran);
        #endregion

#region command builder
        DbCommandBuilder getDbCommandBuilder();
        DbCommandBuilder getDbCommandBuiler(DbDataAdapter adapter);
#endregion

#region DataAdapter
        DbDataAdapter getDataAdapter();
        DbDataAdapter getDataAdapter(DbCommand command);
        DbDataAdapter getDataAdapter(string command, DbConnection connection);
        DbDataAdapter getDataAdapter(string command, string connstr);
#endregion

#region DataReader

#endregion
        
    }
}
