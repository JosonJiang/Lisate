using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Security;

namespace Lishate.Security.Xor
{
    public class XorSecurity : BaseSecurity
    {
        /*
        public override byte[] Encode(XorParameter parameter)
        {
            //return base.Encode(parames);
            byte first = parameter.Random[0];
            first = (byte)(first % 4);
            byte[] result = parameter.SecurityValue;
            switch (first)
            {
                case 0:
                    result[0] = parameter.Random[0];
                    result[1] = (byte)(result[0] ^ parameter.RealValue[0]);
                    result[2] = parameter.Random[1];
                    result[3] = (byte)(result[2] ^ parameter.RealValue[1]);
                    break;
                case 1:
                    result[0] = parameter.Random[0];
                    result[1] = parameter.Random[1];
                    result[2] = (byte)(result[0] ^ parameter.RealValue[0]);
                    result[3] = (byte)(result[1] ^ parameter.RealValue[1]);
                    break;
                case 2:
                    result[0] = parameter.Random[0];
                    result[1] = parameter.Random[1];
                    result[2] = (byte)(result[1] ^ parameter.RealValue[0]);
                    result[3] = (byte)(result[0] ^ parameter.RealValue[1]);
                    break;
                case 3:
                    result[0] = parameter.Random[0];
                    result[2] = parameter.Random[1];
                    result[1] = (byte)(result[2] ^ parameter.RealValue[1]);
                    result[3] = (byte)(result[0] ^ parameter.RealValue[0]);
                    break;
            }
            return result;
        }

        public override byte[] Decode(XorParameter parameter)
        {
            //return base.Decode(parames);
            byte first = parameter.SecurityValue[0];
            first = (byte)(first % 4);
            byte[] result = parameter.RealValue;//new byte[2];
            switch (first)
            {
                case 0:
                    result[0] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[1]);
                    result[1] = (byte)(parameter.SecurityValue[2] ^ parameter.SecurityValue[3]);
                    break;
                case 1:
                    result[0] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[2]);
                    result[1] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[3]);
                    break;
                case 2:
                    result[0] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[2]);
                    result[1] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[3]);
                    break;
                case 3:
                    result[0] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[3]);
                    result[1] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[2]);
                    break;
            }
            return result;
        }
        */
        private bool decode(XorPacketParameter xpp)
        {
            byte first = xpp.Encode_1;
            first = (byte)(first % 4);
            //byte[] result = parameter.RealValue;//new byte[2];
            switch (first)
            {
                case 0:
                    xpp.Src_1 = (byte)(xpp.Encode_1 ^ xpp.Encode_2);
                    xpp.Src_2 = (byte)(xpp.Encode_3 ^ xpp.Encode_4);
                    break;
                case 1:
                    xpp.Src_1 = (byte)(xpp.Encode_1 ^ xpp.Encode_3);
                    xpp.Src_2 = (byte)(xpp.Encode_2 ^ xpp.Encode_4);
                    /*
                    result[0] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[2]);
                    result[1] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[3]);
                     * */
                    break;
                case 2:
                    xpp.Src_1 = (byte)(xpp.Encode_2 ^ xpp.Encode_3);
                    xpp.Src_2 = (byte)(xpp.Encode_1 ^ xpp.Encode_4);
                    /*
                    result[0] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[2]);
                    result[1] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[3]);
                     * */
                    break;
                case 3:
                    xpp.Src_1 = (byte)(xpp.Encode_1 ^ xpp.Encode_4);
                    xpp.Src_2 = (byte)(xpp.Encode_2 ^ xpp.Encode_3);
                    /*
                    result[0] = (byte)(parameter.SecurityValue[0] ^ parameter.SecurityValue[3]);
                    result[1] = (byte)(parameter.SecurityValue[1] ^ parameter.SecurityValue[2]);
                     * */
                    break;
            }
            return true;
        }

        private bool encode(XorPacketParameter xpp)
        {
            byte first = xpp.RandomFirst;
            first = (byte)(first % 4);
            switch (first)
            {
                case 0:
                    xpp.Encode_1 = xpp.RandomFirst;
                    xpp.Encode_2 = (byte)(xpp.Encode_1 ^ xpp.Src_1);
                    xpp.Encode_3 = xpp.RandomSecond;
                    xpp.Encode_4 = (byte)(xpp.Encode_3 ^ xpp.Src_2);
                    /*
                    result[0] = parameter.Random[0];
                    result[1] = (byte)(result[0] ^ parameter.RealValue[0]);
                    result[2] = parameter.Random[1];
                    result[3] = (byte)(result[2] ^ parameter.RealValue[1]);
                     * */
                    break;
                case 1:
                    xpp.Encode_1 = xpp.RandomFirst;
                    xpp.Encode_2 = xpp.RandomSecond;
                    xpp.Encode_3 = (byte)(xpp.Encode_1 ^ xpp.Src_1);
                    xpp.Encode_4 = (byte)(xpp.Encode_2 ^ xpp.Src_2);
                    /*
                    result[0] = parameter.Random[0];
                    result[1] = parameter.Random[1];
                    result[2] = (byte)(result[0] ^ parameter.RealValue[0]);
                    result[3] = (byte)(result[1] ^ parameter.RealValue[1]);
                     * */
                    break;
                case 2:
                    xpp.Encode_1 = xpp.RandomFirst;
                    xpp.Encode_2 = xpp.RandomSecond;
                    xpp.Encode_3 = (byte)(xpp.Encode_2 ^ xpp.Src_1);
                    xpp.Encode_4 = (byte)(xpp.Encode_1 ^ xpp.Src_2);
                    /*
                    result[0] = parameter.Random[0];
                    result[1] = parameter.Random[1];
                    result[2] = (byte)(result[1] ^ parameter.RealValue[0]);
                    result[3] = (byte)(result[0] ^ parameter.RealValue[1]);
                     * */
                    break;
                case 3:
                    xpp.Encode_1 = xpp.RandomFirst;
                    xpp.Encode_3 = xpp.RandomSecond;
                    xpp.Encode_2 = (byte)(xpp.Encode_3 ^ xpp.Src_2);
                    xpp.Encode_4 = (byte)(xpp.Encode_1 ^ xpp.Src_1);
                    /*
                    result[0] = parameter.Random[0];
                    result[2] = parameter.Random[1];
                    result[1] = (byte)(result[2] ^ parameter.RealValue[1]);
                    result[3] = (byte)(result[0] ^ parameter.RealValue[0]);
                     * */
                    break;
            }
            return true;
        }

        public override bool Decode(object parames, byte[] buf, int index)
        {
            XorPacketParameter xpp = (XorPacketParameter)parames;
            xpp.Encode_1 = buf[index];
            xpp.Encode_2 = buf[index + 1];
            xpp.Encode_3 = buf[index + 2];
            xpp.Encode_4 = buf[index + 3];

            decode(xpp);
            return true;
        }

        public override bool Decode(object parames, Lishate.Utility.RingBuffer buffer, int index)
        {
            //return base.Decode(parames, buffer, index);
            XorPacketParameter xpp = (XorPacketParameter)parames;
            byte temp = 0;
            if (buffer.GetIndexByte(index, ref temp) == true)
            {
                xpp.Encode_1 = temp;
            }
            else
            {
                return false;
            }
            if (buffer.GetIndexByte(index + 1, ref temp) == true)
            {
                xpp.Encode_2 = temp;
            }
            else 
            { return false; }

            if (buffer.GetIndexByte(index + 2, ref temp) == true)
            {
                xpp.Encode_3 = temp;
            }
            else
            {
                return false;
            }
            if (buffer.GetIndexByte(index + 3, ref temp) == true)
            {
                xpp.Encode_4 = temp;
            }
            else
            {
                return false;
            }
            decode(xpp);
            return true;
        }

        public override bool Encode(object parames, byte[] buf, int index)
        {
            //return base.Encode(parames, buf, index);
            XorPacketParameter xpp = (XorPacketParameter)parames;
            xpp.RandomFirst = (byte)(Utility.GlobalRandon.Next(128));
            xpp.RandomSecond = (byte)(Utility.GlobalRandon.Next(128));
            encode(xpp);
            buf[index] = xpp.Encode_1;
            buf[index + 1] = xpp.Encode_2;
            buf[index + 2] = xpp.Encode_3;
            buf[index + 3] = xpp.Encode_4;

            
            return true;
        }

        public override bool Encode(object parames)
        {
            //return base.Encode(parames, buffer, index);
            XorPacketParameter xpp = (XorPacketParameter)parames;
            xpp.RandomFirst = (byte)(Utility.GlobalRandon.Next(128));
            xpp.RandomSecond = (byte)(Utility.GlobalRandon.Next(128));
            encode(xpp);
            return true;
        }
    }
}
