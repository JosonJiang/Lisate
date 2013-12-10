using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lishate.Config
{
    public class FileWatch
    {
        private FileSystemWatcher _fsw = new FileSystemWatcher();
        private String _defaultPath = "App.config";
        private int flag = 0;

        public String Path
        {
            get { return _fsw.Path; }
            set { _fsw.Path = value; }
        }

        public readonly static FileWatch Instance = new FileWatch();

        private FileWatch()
        {
            
            //*
            _defaultPath = "App.config";
            _fsw.Path = AppDomain.CurrentDomain.BaseDirectory;
            _fsw.NotifyFilter = NotifyFilters.LastWrite;
            _fsw.Filter = _defaultPath;
            _fsw.Changed += fsw_Changed;
            _fsw.EnableRaisingEvents = true;
            //*/
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            flag++;
            if (flag >= 2)
            {
                flag = 0;
                ConfigSubject.Instance.Notify();
            }
            
        }
    }
}
