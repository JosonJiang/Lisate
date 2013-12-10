using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

namespace Lishate.Data.IDAL
{
    public class Utility
    {
        public static DbParameter GetDbParameter(DbCommand comm,string parameterName,DbType type, int size,object value)
        {
            DbParameter dp = comm.CreateParameter();
            dp.ParameterName = parameterName;
            dp.DbType = type;
            dp.Value = value;
            dp.Size = size;
            return dp;
        }

        public static DbParameter GetDbParameter(DbCommand comm, string parameterName, DbType type, int size, object value, ParameterDirection direction)
        {
            DbParameter dp = comm.CreateParameter();
            dp.ParameterName = parameterName;
            dp.DbType = type;
            dp.Value = value;
            dp.Direction = direction;
            dp.Size = size;
            return dp;
        }

        public static DbParameter GetDbParameter(DbCommand comm, string parametername, DbType type, int size)
        {
            DbParameter dp = comm.CreateParameter();
            dp.ParameterName = parametername;
            dp.DbType = type;
            dp.Size = size;
            return dp;
        }

        public static DbParameter GetDbParameter(DbCommand comm, string parametername, DbType type, int size, ParameterDirection direction)
        {
            DbParameter dp = comm.CreateParameter();
            dp.ParameterName = parametername;
            dp.DbType = type;
            dp.Size = size;
            dp.Direction = direction;
            return dp;
        }
    }
}
