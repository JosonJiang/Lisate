using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Message
{
    public class GobalDef
    {
        public const byte DEVICE_MTYPE_SOCKET = 0x01;
        public const byte DEVICE_STYPE_SOCKET_WIFI = 0x01;


        public const byte COMMAND_MTYPE_PUBLIC = 0x00;
        public const byte COMMAND_STYPE_PUBLIC_LOGIN = 0x01;
        public const byte COMMAND_STYPE_PUBLIC_CHECK_TIME = 0x07;

        public const byte COMMAND_MTYPE_SOCKET = 0x01;
        public const byte COMMAND_STYPE_SOCKET_OPEN = 0x01;
        public const byte COMMAND_STYPE_SOCKET_CLOSE = 0x02;
        public const byte COMMAND_STYPE_SOCKET_GET_STATUE = 0x03;
        public const byte COMMAND_STYPE_SOCKET_SET_CONFIG = 0x04;
        public const byte COMMAND_STYPE_SOCKET_GET_CONFIG_SERVER = 0x05;
        public const byte COMMAND_STYPE_SOCKET_GET_CONFIG_CLIENT = 0x06;
        

        public const byte BASE_MSG_LENGTH = 0x31;
        public const byte BASE_MSG_REQ_LENGTH = 0x31;
        public const byte BASE_MSG_RCV_LENGTH = 0x35;

        public const byte BASE_MSG_START_INDEX = 0x00;
        public const byte BASE_MSG_LENGTH_INDEX = 0x01;
        public const byte BASE_MSG_PROTYPE_INDEX = 0x02;
        public const byte BASE_MSG_SEQ_INDEX = 0x03;
        public const byte BASE_MSG_FTTYPE_INDEX = 0x07;
        public const byte BASE_MSG_FROM_DEVID_INDEX = 0x0B;
        public const byte BASE_MSG_TO_DEVID_INDEX = 0x1B;
        public const byte BASE_MSG_CMD_INDEX = 0x2B;
        public const byte BASE_MSG_STATUE_INDEX = 0x2F;
        public const byte BASE_MSG_REQ_CONTENT_INDEX = 0x2F;

        public const byte BASE_MSG_START_VALUE = 0x00;
        public const byte BASE_MSG_END_VALUE = 0xFF;
        public const byte BASE_MSG_PRO_11 = 0x11;

        public const byte BASE_MSG_RCV_STATUE_OK = 0x00;
        public const byte BASE_MSG_RCV_STATUE_FAIL = 0x01;

        public const byte BASE_MSG_FT_HUB = 0x02;
        public const byte BASE_MSG_FT_END = 0x03;
        public const byte BASE_MSG_FT_SERVER = 0x01;
        public const byte BASE_MSG_FT_MOBILE = 0x00;
        public const byte BASE_MSG_FT_REQ = 0x01;
        public const byte BASE_MSG_FT_RCV = 0x02;

    }
}
