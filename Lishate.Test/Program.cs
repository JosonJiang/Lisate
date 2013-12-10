using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using Lishate.Message;
using Lishate.Basedata;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using Lishate.Config;

namespace Lishate.Test
{
    class Program
    {
        private static int _mod = 0;
        private static int _max = 100000;

        private static UdpServer us = null;
        private static ArrayList sends = new ArrayList();
        private static ArrayList sendThread = new ArrayList();
        private static String _serverIp = "192.168.1.101";
        private static int _sendSize = 0;
        private static int _bitRate = 128 * 1024;

        private static void Parse(String s)
        {
            String[] value = s.ToLower().Split(new string[]{"="},StringSplitOptions.RemoveEmptyEntries);
            int temp = 0;
            if (value.Length == 0)
            {
                return;
            }
            if (value[0].Equals("mode"))
            {
                Int32.TryParse(value[1], out _mod);
                if (_mod == 0)
                {
                    us = new UdpServer();
                }
            }
            else if (value[0].Equals("max"))
            {
                if (Int32.TryParse(value[1], out temp))
                {
                    _max = temp;
                }
                if (sends != null)
                {
                    foreach (UdpSend send in sends)
                    {
                        send.Max = temp;
                        
                        
                    }
                }
            }
            else if (value[0].Equals("servercount"))
            {
                if (us != null)
                {
                    Console.WriteLine("server count is: " + us.RefCount);
                }
            }
            else if (value[0].Equals("serverip"))
            {
                _serverIp = value[1];
                if (sends != null)
                {
                    foreach (UdpSend send in sends)
                    {
                        send.ServerIp = _serverIp;
                    }
                }
            }
            else if (value[0].Equals("sendsize"))
            {
                if (int.TryParse(value[1], out temp))
                {
                    _sendSize = temp;
                }
            }
            else if (value[0].Equals("sendcount"))
            {
                if (_mod == 1)
                {
                    int count = 0;
                    if (int.TryParse(value[1], out count))
                    {
                        sends.Clear();
                        foreach (Thread t in sendThread)
                        {
                            try
                            {
                                t.Abort();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                        sendThread.Clear();
                        for (int i = 0; i < count; i++)
                        {
                            UdpSend us = new UdpSend();
                            us.Port = (uint)(5000 + i);
                            us.Max = _max;
                            us.ServerIp = _serverIp;
                            us.BitRate = _bitRate;
                            sends.Add(us);
                            us.Init();
                            
                            Console.WriteLine("defualt size is: " + us.SendSize);
                            us.SendSize = _sendSize;
                            Console.WriteLine("update size is: " + us.SendSize);
                            Thread t = new Thread(new ThreadStart(us.Send));
                            sendThread.Add(t);
                        }
                    }
                }
            }
            else if (value[0].Equals("bitrate"))
            {
                if (int.TryParse(value[1], out temp))
                {
                    _bitRate = temp;
                }
            }
            else if (value[0].Equals("serversize"))
            {
                if (value.Length == 1)
                {
                    if (us != null)
                    {
                        Console.WriteLine("server buffer size is: " + us.ServerBufferSize);
                    }
                }
                else if (value.Length == 2)
                {
                    if (us != null)
                    {
                        if (int.TryParse(value[1], out temp))
                        {
                            us.ServerBufferSize = temp;
                            Console.WriteLine("set server size is: " + us.ServerBufferSize);
                        }

                    }
                }
            }
            else if (value[0].Equals("start"))
            {
                if (_mod == 1)
                {
                    foreach (Thread t in sendThread)
                    {
                        try
                        {
                            t.Start();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
                else
                {
                    if (us != null)
                    {
                        us.StartRcv();
                    }
                }
            }

        }

        static void Main(string[] args)
        {

            /*
            Message.Public.LoginRcv lr = new Message.Public.LoginRcv();
            BaseMessage bm = new Message.Public.LoginRcv();
            lr.DevID = 1234567890123;
            lr.FromType = 1;
            lr.ToType = 2;
            lr.Req = 1;
            lr.MCommand = 1;
            lr.SCommand = 2;
            
            lr.PacketBuf();



            Console.WriteLine("bm will show");
            bm.SetupBuf(lr.Content, 0);
            Console.WriteLine(bm.Length);
             * */

            //UdpClient client = new UdpClient();
            /*
            UdpSend us = new UdpSend();
            us.Port = 5001;
            us.Init();
            us.Send();
            

            DeviceInfo di = new DeviceInfo();
            DeviceTable dt = new DeviceTable();
            di.DeviceID = 1;
            dt.SetDeviceInfo(di);
            di = new DeviceInfo();
            di.DeviceID = 2;
            dt.SetDeviceInfo(di);

            Console.WriteLine("dt count is: " + dt.GetCount());
             * */
            /*
            AutoResetTest art = new AutoResetTest();
            Thread setThread = new Thread(new ThreadStart(art.Set));
            Thread waiteThread = new Thread(new ThreadStart(art.Wait));

            Console.WriteLine("set thead state:" + setThread.ThreadState);

            setThread.Start();
            waiteThread.Start();
            setThread.Abort();
            Console.WriteLine("set thead state:" + setThread.ThreadState);
            setThread = new Thread(new ThreadStart(art.Set));
            Console.WriteLine("set thead state:" + setThread.ThreadState);
            setThread.Start();
            * */
            /*
            IDictionary log = (IDictionary)ConfigurationSettings.GetConfig("Log");
            String set1 = (string)log["setting1"] + " " + (string)log["setting2"];
            IDictionary syn = (IDictionary)ConfigurationSettings.GetConfig("Syn");
            NameValueCollection nc = (NameValueCollection)ConfigurationSettings.GetConfig("Data");
            set1 = (string)nc.Get("hel");
             * */
            String str = FileWatch.Instance.Path;
            Lishate.Server.ServerManage sm = new Server.ServerManage();
            sm.Start();
            while (true)
            {
                String s = Console.ReadLine();
                Parse(s);
            }
            
        }
    }
}
