using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Security.Xor;
using Lishate.Utility;

namespace Lishate.Message
{
    public class Utility
    {
        public static void SetXorSec(short value, byte[] buf, int index, XorSecurity xs)
        {
            /*
            XorParameter xp = new XorParameter();
            //byte[] valuebuf = new byte[2];
            Random random = new Random();
            Lishate.Utility.Utility.ReadShort2Buf(xp.RealValue, 0, value);
            //xp.RealValue = valuebuf;
            //xp.Random = new byte[2];
            xp.Random[0] = (byte)random.Next(128);
            xp.Random[1] = (byte)random.Next(128);
            byte[] result = xs.Encode(xp);
            if (result != null)
            {
                Array.Copy(result, 0, buf, index, 4);
            }
             * */
            XorPacketParameter xpp = new XorPacketParameter();
            SetXorSec(value, buf, index, xs, xpp);
        }

        public static void SetXorSec(short value, byte[] buf, int index, XorSecurity xs, XorPacketParameter xpp)
        {
            xpp.Src_1 = (byte)((value >> 8) & 0xFF);
            xpp.Src_2 = (byte)(value & 0xFF);
            xs.Encode(xpp, buf, index);
        }

        public static void GetXorSec(byte[] src, int srcIndex, ref short value, XorSecurity xs, XorPacketParameter xpp)
        {

            xs.Decode(xpp, src, srcIndex);
            value = (short)(xpp.Src_1 << 8);
            value = (short)(value + xpp.Src_2); 
        }

        public static void SetXorSec(byte[] buf, int index, XorSecurity xs, XorPacketParameter xpp)
        {
            xs.Encode(xpp, buf, index);
        }

        public static void GetXorSec(byte[] src, int srcIndex, XorSecurity xs, XorPacketParameter xpp)
        {
            xs.Decode(xpp, src, srcIndex);
        }

        public static void GetXorSec(byte[] src, int srcIndex, ref short value, XorSecurity xs)
        {
            XorPacketParameter xpp = new XorPacketParameter();
            GetXorSec(src, srcIndex, ref value, xs, xpp);
            /*
            XorParameter xp = new XorParameter();

            Array.Copy(src, srcIndex, xp.SecurityValue, 0, 4);
            if (xs.Decode(xp) != null)
            {
                Lishate.Utility.Utility.WriteBuf2Short(xp.RealValue, 0, ref value);
            }
            **/
        }

        public static bool GetXorSec(RingBuffer buffer, int index, XorSecurity xs, XorPacketParameter xpp)
        {
            return xs.Decode(xpp, buffer, index);
        }
    }
}
