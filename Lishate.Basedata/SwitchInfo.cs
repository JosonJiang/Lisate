using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Basedata
{
    public class SwitchInfo
    {
        private byte _onOffStatue = 0;
        public byte OnOffStatu
        {
            get { return _onOffStatue; }
            set { _onOffStatue = value; }
        }

    }
}
