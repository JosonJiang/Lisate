using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Utility;

namespace Lishate.Security
{
    public class BaseSecurity
    {
        /*
        public virtual byte[] Encode(object parames)
        {
            return null;
        }

        public virtual byte[] Decode(object parames)
        {
            return null;
        }
         * */

        public virtual bool Encode(object parames, byte[] buf, int index)
        {
            return false;
        }

        public virtual bool Decode(object parames, byte[] buf, int index)
        {
            return false;
        }

        public virtual bool Encode(object parames)
        {
            return false;
        }

        public virtual bool Decode(object parames, RingBuffer buffer, int index)
        {
            return false;
        }
    }
}
