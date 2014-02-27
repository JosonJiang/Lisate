using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Security;
using Lishate.Security.Xor;
using Lishate.Log;
using Lishate.Utility;

namespace Lishate.Message
{
    public class BaseMessage
    {
        private static byte GetCheckSum(byte[] buf, int index)
        {
            byte result = 0;
            for (int i = 0; i < 10; i++)
            {
                result += buf[index + 1 + i];
            }
            return result;
        }

        //private static XorSecurity xs = new XorSecurity();

        public static Boolean CheckIsMessage(byte[] buf, int index)
        {
            if (buf == null || buf.Length - index < GobalDef.BASE_MSG_LENGTH)
            {
                return false;
            }

            if (buf[index + GobalDef.BASE_MSG_START_INDEX] == GobalDef.BASE_MSG_START_VALUE)
            {
                if (buf[index + GobalDef.BASE_MSG_LENGTH_INDEX] < 128 && buf[index + GobalDef.BASE_MSG_LENGTH_INDEX] > 0)
                {
                    if (buf[index + buf[index + GobalDef.BASE_MSG_LENGTH_INDEX] - 1] == GobalDef.BASE_MSG_END_VALUE)
                    {
                        if (GetCheckSum(buf, index) == buf[index + buf[index + GobalDef.BASE_MSG_LENGTH_INDEX] - 2])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private byte _pro = GobalDef.BASE_MSG_PRO_11;
        public byte Pro
        {
            get { return _pro; }
            set { _pro = value; }
        }

        private int _length = GobalDef.BASE_MSG_LENGTH;
        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                /*
                if (_content == null || _content.Length != _length)
                {
                    _content = new byte[_length];
                }
                 * */
            }
        }

        private short _seq;
        public short Seq
        {
            get { return _seq; }
            set { _seq = value; }
        }

        private byte _ftType = 0;
        public byte FromType
        {
            get { return (byte)(_ftType >> 5); }
            set
            {
                _ftType = (byte)(_ftType & 0x1f);
                _ftType = (byte)(_ftType | value<<5);
            }
        }

        public byte ToType
        {
            get { return (byte)((_ftType >> 2) & 0x07); }
            set
            {
                _ftType = (byte)(_ftType & 0xE3);
                _ftType = (byte)(_ftType | (value << 2));
            }
        }

        public byte Req
        {
            get { return (byte)(_ftType & 0x3); }
            set
            {
                _ftType = (byte)(_ftType & 0xFC);
                _ftType = (byte)(_ftType | value);
            }
        }

        private byte[] _content = null;
        public byte[] Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private ulong _FromDevID = 0;
        public ulong FromDevID
        {
            get { return _FromDevID; }
            set { _FromDevID = value; }
        }

        private ulong _ToDevID = 0;
        public ulong ToDevID
        {
            get { return _ToDevID; }
            set { _ToDevID = value; }
        }

        private byte _MType = 0;
        public byte MCommand
        {
            get { return _MType; }
            set { _MType = value; }
        }

        private byte _SType = 0;
        public byte SCommand
        {
            get { return _SType; }
            set { _SType = value; }
        }

        private XorPacketParameter _xpp;
        public XorPacketParameter PacketParameter
        {
            get { return _xpp; }
            set { _xpp = value; }
        }

        public virtual void PacketBuf()
        {
            //Log.Log.WriteDebugLog("BaseMessage PacketBuf\r\n");
            if (_content == null)
            {
                _content = new byte[Length];
            }
            _content[GobalDef.BASE_MSG_START_INDEX] = GobalDef.BASE_MSG_START_VALUE;
            _content[GobalDef.BASE_MSG_LENGTH_INDEX] = (byte)Length;
            _content[GobalDef.BASE_MSG_PROTYPE_INDEX] = GobalDef.BASE_MSG_PRO_11;
            if (_xpp == null)
            {
                _xpp = new XorPacketParameter();
            }
            _xpp.Src_1 = (byte)(_seq & 0xFF);
            _xpp.Src_2 = (byte)(_seq >> 8 & 0xFF);
            Lishate.Message.Utility.SetXorSec(_content, GobalDef.BASE_MSG_SEQ_INDEX, SecurityFactory.XorSec,_xpp);
            _xpp.Src_1 = _ftType;
            _xpp.Src_2 = (byte)Lishate.Security.Utility.GlobalRandon.Next(255);
            Lishate.Message.Utility.SetXorSec(_content, GobalDef.BASE_MSG_FTTYPE_INDEX, SecurityFactory.XorSec,_xpp);
            for (int i = 0; i < 8; i = i + 2)
            {
                _xpp.Src_1 = (byte)(_FromDevID >> (i * 8) & 0xFF);
                _xpp.Src_2 = (byte)(_FromDevID >> ((i + 1) * 8) & 0xFF);
                Lishate.Message.Utility.SetXorSec(_content, GobalDef.BASE_MSG_FROM_DEVID_INDEX + (i * 2), SecurityFactory.XorSec,_xpp);
            }
            for (int i = 0; i < 8; i = i + 2)
            {
                _xpp.Src_1 = (byte)(_ToDevID >> (i * 8) & 0xFF);
                _xpp.Src_2 = (byte)(_ToDevID >> ((i + 1) * 8) & 0xFF);
                Lishate.Message.Utility.SetXorSec(_content, GobalDef.BASE_MSG_TO_DEVID_INDEX + (i * 2), SecurityFactory.XorSec, _xpp);
            }
            _xpp.Src_1 = MCommand;
            _xpp.Src_2 = SCommand;
            Lishate.Message.Utility.SetXorSec(_content, GobalDef.BASE_MSG_CMD_INDEX, SecurityFactory.XorSec, _xpp);
            byte btemp = GetCheckSum(_content, 0);
            _content[_length - 2] = btemp;
            _content[_length - 1] = GobalDef.BASE_MSG_END_VALUE;
        }

        public virtual void SetupBuf(byte[] buf, int index)
        {
            _length = buf[index + GobalDef.BASE_MSG_LENGTH_INDEX];
            _pro = buf[index + GobalDef.BASE_MSG_PROTYPE_INDEX];
            if (_xpp == null)
            {
                _xpp = new XorPacketParameter();
            }


            Utility.GetXorSec(buf, index + GobalDef.BASE_MSG_SEQ_INDEX, SecurityFactory.XorSec,_xpp);// buf[index + BASE_MSG_SEQ_INDEX];
            _seq = (short)(_xpp.Src_2 << 8 | _xpp.Src_1);
            Utility.GetXorSec(buf, index + GobalDef.BASE_MSG_FTTYPE_INDEX, SecurityFactory.XorSec, _xpp);
            _ftType = _xpp.Src_1;
            _FromDevID = 0;
            for (int i = 0; i < 8; i = i + 2)
            {
                Lishate.Message.Utility.GetXorSec(buf, GobalDef.BASE_MSG_FROM_DEVID_INDEX + (i * 2) + index, SecurityFactory.XorSec, _xpp);
                _FromDevID = (ulong)(_FromDevID | ((ulong)(_xpp.Src_1)) << (i * 8));
                _FromDevID = (ulong)(_FromDevID | ((ulong)(_xpp.Src_2 ))<< ((i + 1) * 8));
            }
            _ToDevID = 0;
            for (int i = 0; i < 8; i = i + 2)
            {
                Lishate.Message.Utility.GetXorSec(buf, GobalDef.BASE_MSG_TO_DEVID_INDEX + (i * 2) + index, SecurityFactory.XorSec, _xpp);
                _ToDevID = (ulong)(_ToDevID | ((ulong)(_xpp.Src_1)) << (i * 8));
                _ToDevID = (ulong)(_ToDevID | ((ulong)(_xpp.Src_2)) << ((i + 1) * 8));
            }
            Lishate.Message.Utility.GetXorSec(buf, GobalDef.BASE_MSG_CMD_INDEX + index, SecurityFactory.XorSec, _xpp);
            _MType = _xpp.Src_1;
            _SType = _xpp.Src_2;
            
        }

        public virtual bool SetupBuf(RingBuffer buffer)
        {
            byte temp = 0;
            if (buffer.GetIndexByte((int)GobalDef.BASE_MSG_LENGTH_INDEX, ref temp))
            {
                _length = temp;
            }
            else
            {
                return false;
            }
            if (buffer.GetIndexByte((int)GobalDef.BASE_MSG_PROTYPE_INDEX, ref temp))
            {
                _pro = temp;
            }
            else { return false; }
            if (_xpp == null)
            {
                _xpp = new XorPacketParameter();
            }
            if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_SEQ_INDEX, Security.SecurityFactory.XorSec, _xpp))
            {
                _seq = (short)(_xpp.Src_2 << 8 | _xpp.Src_1);
            }
            else
            {
                return false;
            }
            if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_FTTYPE_INDEX, SecurityFactory.XorSec, _xpp) == true)
            {
                _ftType = _xpp.Src_1;
            }
            else
            {
                return false;
            }
            _FromDevID = 0;
            for (int i = 0; i < 8; i = i + 2)
            {
                if (Lishate.Message.Utility.GetXorSec(buffer, GobalDef.BASE_MSG_FROM_DEVID_INDEX + (i * 2), SecurityFactory.XorSec, _xpp))
                {
                    _FromDevID = (ulong)(_FromDevID | ((ulong)(_xpp.Src_1)) << (i * 8));
                    _FromDevID = (ulong)(_FromDevID | ((ulong)(_xpp.Src_2)) << ((i + 1) * 8));
                }
                else
                {
                    return false;
                }
            }
            _ToDevID = 0;
            for (int i = 0; i < 8; i = i + 2)
            {
                if (Lishate.Message.Utility.GetXorSec(buffer, GobalDef.BASE_MSG_TO_DEVID_INDEX + (i * 2), SecurityFactory.XorSec, _xpp))
                {
                    _ToDevID = (ulong)(_ToDevID | ((ulong)(_xpp.Src_1)) << (i * 8));
                    _ToDevID = (ulong)(_ToDevID | ((ulong)(_xpp.Src_2)) << ((i + 1) * 8));
                }
                else
                {
                    return false;
                }
            }
            if (Lishate.Message.Utility.GetXorSec(buffer, GobalDef.BASE_MSG_CMD_INDEX, SecurityFactory.XorSec, _xpp))
            {

                _MType = _xpp.Src_1;
                _SType = _xpp.Src_2;
            }
            else
            {
                return false;
            }
            return true;


        }

    }

}
