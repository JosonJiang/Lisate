using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Log
{
    public class LogParameter
    {
        private String _Msg = "";
        public String Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }

        private byte _Level = 0;
        public byte Level
        {
            get { return _Level; }
            set { _Level = value; }
        }


    }
}
