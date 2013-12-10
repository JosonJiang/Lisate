using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message
{
    public class BaseReqMessage : BaseMessage
    {
        public BaseReqMessage()
        {
            Length = GobalDef.BASE_MSG_REQ_LENGTH;
        }
    }
}
