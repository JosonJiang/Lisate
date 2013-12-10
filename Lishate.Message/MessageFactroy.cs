using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Security;
using Lishate.Security.Xor;
using Lishate.Message.Public;
using Lishate.Message.Socket;

namespace Lishate.Message
{
    public class MessageFactroy
    {
        private static BaseMessage GetPublicMsg(byte[] buf, int index, XorPacketParameter xpp)
        {
            BaseMessage bm = null;
            int cmd = xpp.Src_2;
            Utility.GetXorSec(buf, index + GobalDef.BASE_MSG_FTTYPE_INDEX, SecurityFactory.XorSec, xpp);
            int _ftType = xpp.Src_1;
            _ftType = _ftType & 0x3;
            switch (cmd)
            {
                case GobalDef.COMMAND_STYPE_PUBLIC_LOGIN:
                    if (_ftType == GobalDef.BASE_MSG_FT_REQ)
                    {
                        bm = new LoginReq();
                        bm.SetupBuf(buf, index);
                    }
                    else if (_ftType == GobalDef.BASE_MSG_FT_RCV)
                    {
                        bm = new LoginRcv();
                        bm.SetupBuf(buf, index);
                    }
                    break;
                case GobalDef.COMMAND_STYPE_PUBLIC_CHECK_TIME:
                    if (_ftType == GobalDef.BASE_MSG_FT_REQ)
                    {
                        bm = new CheckTimeReq();
                        bm.SetupBuf(buf, index);
                    }
                    else if (_ftType == GobalDef.BASE_MSG_FT_RCV)
                    {
                        bm = new CheckTimeRcv();
                        bm.SetupBuf(buf, index);
                    }
                    break;
            }
            return bm;
        }

        private static BaseMessage GetSocketMsg(byte[] buf, int index, XorPacketParameter xpp)
        {
            BaseMessage bm = null;
            int cmd = xpp.Src_2;
            Utility.GetXorSec(buf, index + GobalDef.BASE_MSG_FTTYPE_INDEX, SecurityFactory.XorSec, xpp);
            int _ftType = xpp.Src_1;
            _ftType = _ftType & 0x3;
            switch (cmd)
            {
                case GobalDef.COMMAND_STYPE_SOCKET_GET_STATUE:
                    if (_ftType == GobalDef.BASE_MSG_FT_REQ)
                    {
                        bm = new SocketGetStatueReq();
                        bm.SetupBuf(buf, index);
                    }
                    else if (_ftType == GobalDef.BASE_MSG_FT_RCV)
                    {
                        bm = new SocketGetStatueRcv();
                        bm.SetupBuf(buf, index);
                    }
                    break;
                case GobalDef.COMMAND_STYPE_SOCKET_OPEN:
                    if (_ftType == GobalDef.BASE_MSG_FT_REQ)
                    {
                        bm = new SocketOnReq();
                        bm.SetupBuf(buf, index);
                    }
                    else if (_ftType == GobalDef.BASE_MSG_FT_RCV)
                    {
                        bm = new SocketOnRcv();
                        bm.SetupBuf(buf, index);
                    }
                    break;
            }
            return bm;
        }

        public static BaseMessage GetMesssage(byte[] buf, int index)
        {
            BaseMessage bm = null;
            if (BaseMessage.CheckIsMessage(buf, index))
            {
                XorPacketParameter _xpp = new XorPacketParameter();
                Lishate.Message.Utility.GetXorSec(buf, GobalDef.BASE_MSG_CMD_INDEX + index, SecurityFactory.XorSec, _xpp);
                switch (_xpp.Src_1)
                {
                    case GobalDef.COMMAND_MTYPE_PUBLIC:
                        bm = GetPublicMsg(buf, index, _xpp);
                        break;
                    case GobalDef.COMMAND_MTYPE_SOCKET:
                        bm = GetSocketMsg(buf, index, _xpp);
                        break;
                }
            }
            return bm;
        }
    }
}
