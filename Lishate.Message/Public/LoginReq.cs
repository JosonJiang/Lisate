using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message.Public
{
    public class LoginReq : BaseMessage
    {
        public LoginReq()
        {
            Length = GobalDef.BASE_MSG_REQ_LENGTH + 4;
        }

        public override void PacketBuf()
        {
            base.PacketBuf();
            PacketParameter.Src_1 = _deviceMType;
            PacketParameter.Src_2 = _deviceSType;
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_REQ_CONTENT_INDEX, Security.SecurityFactory.XorSec, PacketParameter);
        }

        public override void SetupBuf(byte[] buf, int index)
        {
            base.SetupBuf(buf, index);
            Utility.GetXorSec(buf, GobalDef.BASE_MSG_REQ_CONTENT_INDEX + index, Security.SecurityFactory.XorSec, PacketParameter);
            _deviceMType = PacketParameter.Src_1;
            _deviceSType = PacketParameter.Src_2;
        }

        public override bool SetupBuf(Lishate.Utility.RingBuffer buffer)
        {
            if (base.SetupBuf(buffer) == true)
            {
                if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_REQ_CONTENT_INDEX , Security.SecurityFactory.XorSec, PacketParameter))
                {
                    _deviceMType = PacketParameter.Src_1;
                    _deviceSType = PacketParameter.Src_2;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private byte _deviceMType = 0;
        public byte DeviceMType
        {
            get { return _deviceMType; }
            set { _deviceMType = value; }
        }

        private byte _deviceSType = 0;
        public byte DeviceSType
        {
            get { return _deviceSType; }
            set { _deviceSType = value; }
        }
    }
}
