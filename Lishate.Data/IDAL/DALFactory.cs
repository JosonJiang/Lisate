using System;
using System.Collections.Generic;
using System.Text;

using Lishate.Data.MySql;
using Lishate.Data.Sqlite;
using Lishate.Data.Sqlserver;

namespace Lishate.Data.IDAL
{
    public class DALFactory
    {
        private static DalInterface mysqlInterface = new MysqlComm();
        private static DalInterface sqliteInterface = new SqliteCommon();
        private static DalInterface sqlserverInterface = new SqlserverCommon();

        public static DalInterface GetDalInterface()
        {
            DalInterface result = null;
            switch (Config.Instance.DBType)
            {
                case Config.DB_TYPE_MYSQL:
                    result = mysqlInterface;
                    break;
                case Config.DB_TYPE_SQLITE:
                    result = sqliteInterface;
                    break;
                case Config.DB_TYPE_SQLSERVER:
                    result = sqlserverInterface;
                    break;
            }
            return result;
        }
    }
}
