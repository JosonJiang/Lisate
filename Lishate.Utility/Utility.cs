using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Utility
{
    public class Utility
    {
        public static bool ReadShort2Buf(byte[] buf, int index, short value)
        {
            if (buf != null && buf.Length - index >= 2)
            {
                buf[index + 0] = (byte)(value >> 8 & 0xFF);
                buf[index + 1] = (byte)(value & 0xFF);
                return true;
            }
            return false;
        }

        public static bool ReadInt2Buf(byte[] buf, int index, int value)
        {
            if (buf != null && buf.Length - index >= 4)
            {
                buf[index + 0] = (byte)(value >> 24 & 0xFF);
                buf[index + 1] = (byte)(value >> 16 & 0xff);
                buf[index + 2] = (byte)(value >> 8 & 0xFF);
                buf[index + 3] = (byte)(value & 0xFF);
                return true;
            }
            return false;
        }

        public static bool ReadLong2Buf(byte[] buf, int index, long value)
        {
            if (buf != null && buf.Length - index >= 8)
            {
                if (ReadInt2Buf(buf, index, (int)(value >> 32)) == true)
                {
                    if (ReadInt2Buf(buf, index + 4, (int)(value & 0xFFFFFFFF)) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool WriteBuf2Short(byte[] buf, int index, ref short value)
        {
            if (buf != null && buf.Length - index >= 2)
            {
                value = (short)(buf[0] << 8);
                value = (short)(value | (short)buf[1]);
                return true;
            }
            return false;
        }

        public static bool WriteBuf2Int(byte[] buf, int index, ref int value)
        {
            if (buf != null && buf.Length - index >= 4)
            {
                value = (int)(buf[0] << 24);
                value = (int)(value | (int)buf[1] << 16);
                value = (int)(value | (int)buf[2] << 8);
                value = (int)(value | (int)buf[3]);
                return true;
            }
            return false;
        }

        public static bool WriteBuf2Long(byte[] buf, int index, ref long value)
        {
            if (buf != null && buf.Length - index >= 8)
            {
                value = (long)(buf[0] << 56);
                value = (long)(value | (long)buf[1] << 48);
                value = (long)(value | (long)buf[2] << 40);
                value = (long)(value | (long)buf[3] << 32);
                value = (long)(value | (long)buf[4] << 24);
                value = (long)(value | (long)buf[5] << 16);
                value = (long)(value | (long)buf[6] << 8);
                value = (long)(value | (long)buf[7] << 0);
                return true;
            }
            return false;
        }

        public static bool ConvertByte2Long(byte[] buf, ref ulong va)
        {
            if (buf.Length != 8)
            {
                return false;
            }
            va = 0;
            for (int i = 0; i < 8; i++)
            {
                va = va + (((ulong)(buf[i] & 0xFF)) << (i * 8));

            }
            return true;
        }

        public static bool ConvertLong2Byte(byte[] buf, ulong va)
        {
            if (buf.Length != 8)
            {
                return false;
            }
            for (int i = 0; i < 8; i++)
            {
                buf[i] = (byte)(((ulong)va >> (i * 8)) & 0xFF);
            }
            return true;
        }

        public static bool ConvertString2Byte(String source, byte[] buf, int length)
        {
            if (source.Length != length * 2)
            {
                return false;
            }
            if (buf.Length != length)
            {
                return false;
            }
            for (int i = 0; i < length; i++)
            {
                buf[i] = (byte)Convert.ToInt32(source.Substring(i * 2, 2), 16);
            }
            return true;
        }

        public static bool CheckIsHex(String s)
        {
            char[] temp = s.ToCharArray();
            foreach (char c in temp)
            {
                if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                {
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static String ConvertByte2String(byte[] buf, int length)
        {
            if (buf.Length != length)
            {
                return null;
            }
            String result = "";
            String temp = "";
            for (int i = 0; i < length; i++)
            {
                temp = Convert.ToString((int)buf[i], 16);
                if (temp.Length == 1)
                {
                    temp = "0" + temp;
                }
                result += temp;
            }
            return result;
        }

        public static bool ConvertString2Long(string source, ref ulong value)
        {
            byte[] tempbuf = new byte[8];
            if (ConvertString2Byte(source, tempbuf, 8))
            {
                return ConvertByte2Long(tempbuf, ref value);
            }
            return false;

        }

        public static String ConvertLong2String(ulong value)
        {
            byte[] tempbuf = new byte[8];
            if (ConvertLong2Byte(tempbuf, value))
            {
                return ConvertByte2String(tempbuf, 8);
            }
            return null;
        }
    }
}
