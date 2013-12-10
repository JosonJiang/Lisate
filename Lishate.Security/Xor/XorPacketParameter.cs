using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Security.Xor
{
    public class XorPacketParameter
    {
        private byte _random_first = 0;
        public byte RandomFirst
        {
            get { return _random_first; }
            set { _random_first = value; }
        }

        private byte _random_second = 0;
        public byte RandomSecond
        {
            get { return _random_second; }
            set { _random_second = value; }
        }

        private byte _src_1 = 0;
        public byte Src_1
        {
            get { return _src_1; }
            set { _src_1 = value; }
        }

        private byte _src_2 = 0;
        public byte Src_2
        {
            get { return _src_2; }
            set { _src_2 = value; }
        }

        private byte _encode_1 = 0;
        public byte Encode_1
        {
            get { return _encode_1; }
            set { _encode_1 = value; }
        }

        private byte _encode_2 = 0;
        public byte Encode_2
        {
            get { return _encode_2; }
            set { _encode_2 = value; }
        }

        private byte _encode_3 = 0;
        public byte Encode_3
        {
            get { return _encode_3; }
            set { _encode_3 = value; }
        }

        private byte _encode_4 = 0;
        public byte Encode_4
        {
            get { return _encode_4; }
            set { _encode_4 = value; }
        }
    }
}
