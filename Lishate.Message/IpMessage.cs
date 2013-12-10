using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Lishate.Message
{
    public class IpMessage
    {
        private BaseMessage _msg;
        public BaseMessage Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        private IPEndPoint _endPoint;
        public IPEndPoint EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }
    }
}
