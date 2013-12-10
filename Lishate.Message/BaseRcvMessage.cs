using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message
{
    public class BaseRcvMessage : BaseMessage
    {
        private byte _statue = 0;
        public byte RcvStatue
        {
            get { return _statue; }
            set { _statue = value; }
        }

        public BaseRcvMessage()
        {
            Length = GobalDef.BASE_MSG_RCV_LENGTH;
        }

        public override void PacketBuf()
        {
            base.PacketBuf();
            PacketParameter.Src_1 = _statue;
            PacketParameter.Src_2 = (byte)Security.Utility.GlobalRandon.Next(255);
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter);
            //Log.Log.WriteDebugLog("BaseRcvMessage PacketBuf\r\n");
        }

        public override void SetupBuf(byte[] buf, int index)
        {
            base.SetupBuf(buf, index);
            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index, Security.SecurityFactory.XorSec, PacketParameter);
            _statue = PacketParameter.Src_1;
        }

        public override bool SetupBuf(Lishate.Utility.RingBuffer buffer)
        {
            //return base.SetupBuf(buffer);
            if (base.SetupBuf(buffer))
            {
                if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter) == true)
                {
                    _statue = PacketParameter.Src_1;
                }

            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
