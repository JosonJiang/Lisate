using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Message;

namespace Lishate.Message.Public
{
    public class LoginRcv : BaseRcvMessage
    {
        public LoginRcv()
        {
            MCommand = GobalDef.COMMAND_MTYPE_PUBLIC;
            SCommand = GobalDef.COMMAND_STYPE_PUBLIC_LOGIN;
            Req = GobalDef.BASE_MSG_FT_RCV;
        }
    }
}
