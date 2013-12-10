using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message.Socket
{
    public class SocketGetStatueRcv : BaseRcvMessage
    {
        private byte _SocketStatue = 0;
        public byte SocketStatue
        {
            get { return _SocketStatue; }
            set { _SocketStatue = value; }
        }

        public override void PacketBuf()
        {
            base.PacketBuf();
            PacketParameter.Src_1 = RcvStatue;
            PacketParameter.Src_2 = _SocketStatue;
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter);
        }

        public override void SetupBuf(byte[] buf, int index)
        {
            base.SetupBuf(buf, index);
            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index, Security.SecurityFactory.XorSec, PacketParameter);
            RcvStatue = PacketParameter.Src_1;
            _SocketStatue = PacketParameter.Src_2;
        }

        public override bool SetupBuf(Lishate.Utility.RingBuffer buffer)
        {
            if (base.SetupBuf(buffer) == true)
            {
                _SocketStatue = PacketParameter.Src_2;
                return true;
            }
            return false;
        }
    }
}
