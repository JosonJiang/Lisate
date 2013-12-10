using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;

namespace Lishate.Data.IDAL
{
    public class Trans : IDisposable
    {

        private DbConnection conn;

        private DbTransaction dbTrans;

        public DbConnection DbConnection
        {

            get { return this.conn; }

        }

        public DbTransaction DbTrans
        {

            get { return this.dbTrans; }

        }



        public Trans()
        {

            conn = DALHelper.CreateConnection();

            conn.Open();

            dbTrans = conn.BeginTransaction();

        }

        public Trans(string connectionString)
        {

            conn = DALHelper.CreateConnection(connectionString);

            conn.Open();

            dbTrans = conn.BeginTransaction();

        }

        public void Commit()
        {

            dbTrans.Commit();

            this.Colse();

        }



        public void RollBack()
        {

            dbTrans.Rollback();

            this.Colse();

        }



        public void Dispose()
        {

            this.Colse();

        }



        public void Colse()
        {

            if (conn.State == System.Data.ConnectionState.Open)
            {

                conn.Close();

            }

        }

    }
}
