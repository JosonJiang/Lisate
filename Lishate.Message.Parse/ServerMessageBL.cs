using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Log;
using Lishate.Message;
using Lishate.Message.Public;
using Lishate.Config;

namespace Lishate.Message.Parse
{
    public class ServerMessageBL
    {
        public BaseMessage ParsePublicMsgReq(BaseMessage bm)
        {
            switch (bm.SCommand)
            {
                case GobalDef.COMMAND_STYPE_PUBLIC_GETSERVER:
                    GetServerRcv gsr = new GetServerRcv();
                    gsr.FromDevID = bm.FromDevID;
                    gsr.ToDevID = bm.ToDevID;
                    gsr.FromType = GobalDef.BASE_MSG_FT_SERVER;
                    gsr.ToType = bm.FromType;
                    gsr.ServerList = GobalConfigInfo.Instance.ServerList;
                    return gsr;
            }
            return null;
        }

        public void ParsePublicRsp(BaseMessage bm)
        {
            short temp = bm.Seq;

        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    IpMessage im = (IpMessage)MessageInstance.getInstance().ServerList.Get();
                    BaseMessage bm = im.Msg;
                    if (bm.ToType == GobalDef.BASE_MSG_FT_SERVER)
                    {

                        if (bm.Req == GobalDef.BASE_MSG_FT_REQ)
                        {
                            BaseMessage resultmsg = null;
                            switch (bm.MCommand)
                            {
                                case GobalDef.COMMAND_MTYPE_PUBLIC:
                                    resultmsg = ParsePublicMsgReq(bm);
                                    break;
                            }
                            im.Msg = resultmsg;
                            im.Msg.Seq = bm.Seq;
                            im.Msg.PacketBuf();

                            //im.Msg.SetupBuf(im.Msg.Content, 0);
                            MessageInstance.getInstance().SendList.Add(im);
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Log.WriteErrorLog(e.Message);
                    Log.Log.WriteErrorLog(e.StackTrace);
                }
            }
        }
    }
}
