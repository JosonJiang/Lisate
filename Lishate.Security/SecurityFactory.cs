using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Security
{
    public class SecurityFactory
    {
        private static Xor.XorSecurity xs = new Xor.XorSecurity();

        public static BaseSecurity GetSecurity(int id)
        {
            BaseSecurity bs ;
            switch (id)
            {
                case 0:
                    bs = xs;
                    break;
                default:
                    bs = xs;
                    break;
            }
            return bs;
        }

        public static Xor.XorSecurity XorSec
        {
            get { return xs; }
        }
    }
}
