using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message.Public
{
    public class GetServerReq : BaseReqMessage
    {
        public GetServerReq()
        {
            MCommand = GobalDef.COMMAND_MTYPE_PUBLIC;
            SCommand = GobalDef.COMMAND_STYPE_PUBLIC_GETSERVER;
            Req = GobalDef.BASE_MSG_FT_REQ;
        }
    }
}
