using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Lishate.Test
{
    public class UdpSend
    {
        private UdpClient client;
        private IPEndPoint serverPoint;
        private IPEndPoint point;

        private Random r = new Random();

        private UInt32 _port = 12188;//5000;
        public UInt32 Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private int _max = 100000;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        private String _serverIp = "192.168.1.101";
        public String ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }

        private int _BitRate = 128 * 1024;
        
        public int BitRate
        {
            get { return _BitRate; }
            set 
            {
                _BitRate = value;
                _perMSBitRate = _BitRate / 1000;
            }
        }
        private int _perMSBitRate = 131;
        

        public int SendSize
        {
            get { if (client != null) { return client.Client.SendBufferSize; } return 0; }
            set { if (client != null) { client.Client.SendBufferSize = value; } }
        }

        public UdpSend()
        {
            
            

           // point = new IPEndPoint(IPAddress.Any, (int)_port);
           // client = new UdpClient(point);
        }

        public void Init()
        {
            point = new IPEndPoint(IPAddress.Any, (int)_port);
            serverPoint = new IPEndPoint(IPAddress.Parse(ServerIp), 12188/*5000*/);
            //client = new UdpClient(point);
            GetNextClient();
        }

        private bool GetClient(IPEndPoint point)
        {
            try
            {
                client = new UdpClient(point);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GetNextClient()
        {
            _port++;
            point = new IPEndPoint(IPAddress.Any, (int)_port);
            while (GetClient(point) == false)
            {
                _port++;
                point = new IPEndPoint(IPAddress.Any,(int) _port);

            }
            Console.WriteLine("GetNextClient over point is: " + _port);
        }

        private long _lastTicks = DateTime.Now.Ticks;

        private int _lastHasSize = 0;

        private bool canSend(int size)
        {
            long temp = (DateTime.Now.Ticks - _lastTicks)/10000;
            //Console.WriteLine("temp is: " + temp + " last ticks is: " + _lastTicks + " size is: " + size);
            if (temp > 300)
            {
                temp = 300;
            }
            if(temp > 0)
            {
                if((_perMSBitRate * temp ) > (_lastHasSize + size))
                {
                    return true;
                }
            }
            return false;
        }

        public void Send()
        {
            byte[] b = new byte[1024];
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = (byte)r.Next(255);
            }
            int flag = 0;
            //Console.WriteLine("the ticks is: " + DateTime.Now.Ticks);
            int isSend = 0;
            while (flag < Max)
            {
                //Console.WriteLine("the flag is: " + flag);
                if (canSend(b.Length))
                {
                    //Console.WriteLine("can send");
                    client.Send(b, b.Length, serverPoint);
                    _lastHasSize += b.Length;
                    //Console.WriteLine("will add flag");
                    isSend = 1;
                    flag++;
                }
                else
                {
                    //Console.WriteLine("can't send");
                    if (isSend == 1)
                    {
                        _lastTicks = DateTime.Now.Ticks;
                    }
                    isSend = 0;
                    Thread.Sleep(1);
                    //Console.WriteLine("after sleep flag is: " + flag);
                    _lastHasSize = 0;

                }
                
            }
            Console.WriteLine("the ticks is; " + DateTime.Now.Ticks);
            Console.WriteLine("***********************************");
            Console.WriteLine("end");
        }
    }
}
