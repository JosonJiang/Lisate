using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;
using Lishate.Security.Xor;

namespace Lishate.Message.Public
{
    public class GetServerRcv : BaseRcvMessage
    {
        public int ServerCount
        {
            get { return list.Count; }
        }

        private ArrayList list = new ArrayList();
        public ArrayList ServerList
        {
            get { return list; }
            set { list = value; }
        }

        public GetServerRcv()
        {
            MCommand = GobalDef.COMMAND_MTYPE_PUBLIC;
            SCommand = GobalDef.COMMAND_STYPE_PUBLIC_GETSERVER;
            Req = GobalDef.BASE_MSG_FT_RCV;
        }

        private void packip(byte[] content, int index, XorPacketParameter packetparmeter, IPEndPoint iep)
        {
            String s = iep.Address.ToString();
            String[] ipbuf = s.Split(".".ToCharArray());
            packetparmeter.Src_1 = (byte)int.Parse(ipbuf[0]);
            packetparmeter.Src_2 = (byte)int.Parse(ipbuf[1]);
            Utility.SetXorSec(Content, index, Security.SecurityFactory.XorSec, packetparmeter);
            packetparmeter.Src_1 = (byte)int.Parse(ipbuf[2]);
            packetparmeter.Src_2 = (byte)int.Parse(ipbuf[3]);
            Utility.SetXorSec(Content, index + 4, Security.SecurityFactory.XorSec, packetparmeter);
            int temp = iep.Port;
            packetparmeter.Src_1 = (byte)(iep.Port & 0xFF);
            packetparmeter.Src_2 = (byte)((iep.Port >> 8) & 0xFF);
            Utility.SetXorSec(Content, index + 8, Security.SecurityFactory.XorSec, packetparmeter);
            //packetparmeter.Src_1 = (byte)((ipaddr >> (0 * 8)) & 0xFF);
            //packetparmeter.Src_2 = (byte)((ipaddr >> (1 * 8)) & 0xFF);
            

        }

        private void setip(byte[] content, int index, XorPacketParameter packetparmeter, IPEndPoint iep)
        {
            byte[] buf = new byte[6];
            for (int i = 0; i < 6; i = i + 2)
            {
                Utility.GetXorSec(content, index + (i * 2), Security.SecurityFactory.XorSec, packetparmeter);
                buf[i] = packetparmeter.Src_1;
                buf[i + 1] = packetparmeter.Src_2;
            }
            String ipaddress = ((int)buf[0]).ToString() + "." + ((int)buf[1]).ToString() + "." + ((int)buf[2]).ToString() + "." + ((int)buf[3]).ToString();
            iep.Address = IPAddress.Parse(ipaddress);
            iep.Port = buf[4] | (buf[5] << 8);
        }

        public override void PacketBuf()
        {
            Length = GobalDef.BASE_MSG_RCV_LENGTH + ServerCount * 12;
            base.PacketBuf();
            PacketParameter.Src_1 = (byte)RcvStatue;
            PacketParameter.Src_2 = (byte)ServerCount;
            Utility.SetXorSec(Content, GobalDef.BASE_MSG_STATUE_INDEX, Security.SecurityFactory.XorSec, PacketParameter);
            int index = GobalDef.BASE_MSG_STATUE_INDEX + 4;
            foreach (IPEndPoint iep in list)
            {
                packip(Content, index, PacketParameter, iep);
                index = index + 12;
            }
        }

        public override void SetupBuf(byte[] buf, int index)
        {
            base.SetupBuf(buf, index);
            Utility.GetXorSec(buf, GobalDef.BASE_MSG_STATUE_INDEX + index, Security.SecurityFactory.XorSec, PacketParameter);
            RcvStatue = PacketParameter.Src_1;
            int count = PacketParameter.Src_2;
            int findex = GobalDef.BASE_MSG_STATUE_INDEX + index + 4;
            list.Clear();
            for (int i = 0; i < count; i++)
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 12188);
                setip(buf, findex, PacketParameter, iep);
                findex = findex + 12;
                list.Add(iep);
            }

        }
    }
}
