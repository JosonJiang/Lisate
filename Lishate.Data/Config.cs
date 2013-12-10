using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using Lishate.Config;


namespace Lishate.Data
{
    public class Config : BaseConfig
    {
        public const int DB_TYPE_SQLITE = 0;
        public const int DB_TYPE_MYSQL = 1;
        public const int DB_TYPE_SQLSERVER = 2;

        private const String _ConfigName = "Data";
        private const String _DbType = "dbtype";
        private const String _ConnectString = "connectstring";

        private int _db_type = DB_TYPE_SQLITE;
        public int DBType
        {
            get { return _db_type; }
            //set { _db_type = value; }
        }

        private String _connectString;
        public String ConnectString
        {
            get { return _connectString; }
            //set { _connectString = value; }
        }

        public void InitConfig()
        {
            try
            {
                NameValueCollection nc = (NameValueCollection)ConfigurationSettings.GetConfig(_ConfigName);
                int.TryParse(nc.Get(_DbType), out _db_type);
                _connectString = nc.Get(_ConnectString);
                nc = (NameValueCollection)ConfigurationSettings.GetConfig("Net");
                int i = 0;
                i++;
            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message);
                Log.WriteLog(e.StackTrace);
            }
        }

        private Config()
        {
            InitConfig();
            ConfigSubject.Instance.AddConfig(this);
        }

        public static readonly Config Instance = new Config();

        

        public override void Update()
        {
            //base.Update();
            InitConfig();
        }

    }
}
