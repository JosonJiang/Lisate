using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Config;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;

namespace Lishate.Log
{
    public class Config : BaseConfig
    {
        public static readonly byte Show_Log = 1 << 0;
        public static readonly byte Txt_Log = 1 << 1;
        public static readonly byte DataBase_Log = 1 << 2;

        public static readonly byte Info_Level = 1;
        public static readonly byte Debug_Level = 2;
        public static readonly byte User_Level = 3;
        public static readonly byte System_Level = 4;
        public static readonly byte Error_Level = 5;

        public static readonly byte Level_MAX = Error_Level;

        private const String _ConfigName = "Log";
        private const String _Level = "level";
        private const String _LogType = "type";
        private const String _Path = "path";

        private static int _log_type = Show_Log;
        public static int LogType
        {
            get { return _log_type; }
            //set { _log_type = value; }
        }

        private static int _log_level = 0;
        public static int LogLevel
        {
            get { return _log_level; }
            //set { _log_level = value; }
        }

        private static String _log_text_path = AppDomain.CurrentDomain + "\\log.txt";
        public static String LogPath
        {
            get { return _log_text_path; }
            //set { _log_text_path = value; }
        }

        private Config()
        {
            ConfigSubject.Instance.AddConfig(this);
        }
        public static readonly Config Instance = new Config();

        public override void Update()
        {
            //base.Update();
            try
            {
                NameValueCollection nvc = (NameValueCollection)ConfigurationSettings.GetConfig(_ConfigName);
                int.TryParse(nvc.Get(_LogType), out _log_type);
                int.TryParse(nvc.Get(_Level), out _log_level);
                _log_text_path = nvc.Get(_Path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
