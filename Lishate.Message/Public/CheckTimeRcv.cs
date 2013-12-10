using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message.Public
{
    public class CheckTimeRcv : BaseRcvMessage
    {
        public const ulong SixMask = 0x3F;
        public const ulong FiveMask = 0x1F;
        public const ulong FourMask = 0x0F;
        public const ulong SevenMask = 0x7F;

        public const int SecondLeft = 0;
        public const int MinuteLeft = 6;
        public const int HourLeft = 12;
        public const int DayLeft = 17;
        public const int MonthLeft = 22;
        public const int YearLeft = 26;


        private ulong _checktime = 0;
        public ulong Second
        {
            get { return ((_checktime>>SecondLeft) & (SixMask)); }
            set
            {
                _checktime = _checktime & (~(SixMask<<SecondLeft));
                _checktime = _checktime | value;
            }
        }
        public ulong Minute
        {
            get { return ((_checktime >> MinuteLeft) & SixMask); }
            set
            {
                _checktime = _checktime & (~(SixMask << MinuteLeft));
                _checktime = _checktime | (value << MinuteLeft);
            }
        }
        public ulong Hour
        {
            get { return (_checktime >> HourLeft) & FiveMask; }
            set
            {
                _checktime = _checktime & (~(FiveMask << HourLeft));
                _checktime = _checktime | (value << HourLeft);
            }
        }
        public ulong Day
        {
            get { return (_checktime >> DayLeft) & FiveMask; }
            set
            {
                _checktime = _checktime & (~(FiveMask << DayLeft));
                _checktime = _checktime | (value << DayLeft);
            }
        }
        public ulong Month
        {
            get { return (_checktime >> MonthLeft) & FourMask; }
            set
            {
                _checktime = _checktime & (~(FourMask << MonthLeft));
                _checktime = _checktime | (value << MonthLeft);
            }
        }
        public ulong Year
        {
            get { return (_checktime >> YearLeft) & SevenMask; }
            set
            {
                _checktime = _checktime & (~(SevenMask << YearLeft));
                _checktime = _checktime | (value << YearLeft);
            }
        }

        public CheckTimeRcv()
        {
            Length = GobalDef.BASE_MSG_RCV_LENGTH + 8;
        }

        public override void PacketBuf()
        {
            base.PacketBuf();
            PacketParameter.Src_1 = RcvStatue;
            PacketParameter.Src_2 = (byte)(_checktime & 0xFF);
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter);

            PacketParameter.Src_1 = (byte)(_checktime >> 8 & 0xFF);
            PacketParameter.Src_2 = (byte)(_checktime >> 16 & 0xFF);
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX + 4, Security.SecurityFactory.XorSec, PacketParameter);

            PacketParameter.Src_1 = (byte)(_checktime >> 24 & 0xFF);
            PacketParameter.Src_2 = (byte)(_checktime >> 32 & 0xFF);
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX + 8, Security.SecurityFactory.XorSec, PacketParameter);
        }

        public override void SetupBuf(byte[] buf, int index)
        {
            base.SetupBuf(buf, index);
            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index, Security.SecurityFactory.XorSec, PacketParameter);
            _checktime = 0;
            RcvStatue = PacketParameter.Src_1;
            _checktime = PacketParameter.Src_2;

            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index + 4, Security.SecurityFactory.XorSec, PacketParameter);
            _checktime = _checktime + ((ulong)PacketParameter.Src_1 << 8);
            _checktime = _checktime + ((ulong)PacketParameter.Src_2 << 16);

            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index + 8, Security.SecurityFactory.XorSec, PacketParameter);
            _checktime = _checktime + ((ulong)PacketParameter.Src_1 << 24);
            _checktime = _checktime + ((ulong)PacketParameter.Src_2 << 32);


        }

        public override bool SetupBuf(Lishate.Utility.RingBuffer buffer)
        {
            if (base.SetupBuf(buffer) == true)
            {
                if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter) == true)
                {
                    RcvStatue = PacketParameter.Src_1;
                    _checktime = PacketParameter.Src_2;
                }
                else
                {
                    return false;
                }
                if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_STATUE_INDEX + 4, Security.SecurityFactory.XorSec, PacketParameter) == true)
                {
                    _checktime = _checktime + ((ulong)PacketParameter.Src_1 << 8);
                    _checktime = _checktime + ((ulong)PacketParameter.Src_2 << 16);
                }
                else
                {
                    return false;
                }
                if (Utility.GetXorSec(buffer, GobalDef.BASE_MSG_STATUE_INDEX + 4, Security.SecurityFactory.XorSec, PacketParameter) == true)
                {
                    _checktime = _checktime + ((ulong)PacketParameter.Src_1 << 8);
                    _checktime = _checktime + ((ulong)PacketParameter.Src_2 << 16);
                }
                else
                {
                    return false;
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
