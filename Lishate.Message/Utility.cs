using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Security.Xor;
using Lishate.Utility;

namespace Lishate.Message
{
    public class Utility
    {
        /*
        public static void SetXorSec(short value, byte[] buf, int index, XorSecurity xs)
        {
           
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
        */
        public static void SetXorSec(byte[] buf, int index, XorSecurity xs, XorPacketParameter xpp)
        {
            xs.Encode(xpp, buf, index);
        }

        public static void GetXorSec(byte[] src, int srcIndex, XorSecurity xs, XorPacketParameter xpp)
        {
            xs.Decode(xpp, src, srcIndex);
        }
        /*
        public static void GetXorSec(byte[] src, int srcIndex, ref short value, XorSecurity xs)
        {
            XorPacketParameter xpp = new XorPacketParameter();
            GetXorSec(src, srcIndex, ref value, xs, xpp);
            
        }
        */
        public static bool GetXorSec(RingBuffer buffer, int index, XorSecurity xs, XorPacketParameter xpp)
        {
            return xs.Decode(xpp, buffer, index);
        }
    }
}
