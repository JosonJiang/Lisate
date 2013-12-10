using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Security.Xor
{
    public class XorParameter
    {

        private byte[] _random = new byte[2];
        public byte[] Random
        {
            get { return _random; }
            set { _random = value; }
        }

        private byte[] _realValue = new byte[2];
        public byte[] RealValue
        {
            get { return _realValue; }
            set { _realValue = value; }
        }

        private byte[] _SecurityValue = new byte[4];
        public byte[] SecurityValue
        {
            get { return _SecurityValue; }
            set { _SecurityValue = value; }
        }
    }
}
