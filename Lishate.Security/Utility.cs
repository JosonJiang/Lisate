using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Lishate.Security
{
    public class Utility
    {
        private static Random random = new Random();

        public static Random GlobalRandon
        {
            get { return random; }
        }

        public static byte[] CopyByte(byte[] src)
        {
            if (src == null || src.Length == 0)
            {
                return null;
            }

            byte[] result = new byte[src.Length];
            System.Array.Copy(src, result, src.Length);
            return result;
        }
    }
}
