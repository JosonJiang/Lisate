using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message
{
    public class SuperMessage 
    {
        private BaseMessage _bm;
        public BaseMessage BM
        {
            get { return _bm; }
            set { _bm = value; }
        }

        public virtual void PacketBuf()
        {

        }

        public virtual void SetupBuf()
        {
        }
    }
}
