using System;
using System.Collections.Generic;
using System.Text;
using Lishate.Server;

namespace Lishate.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int i = -2;
            //byte b = (byte)(i & 0xff);
            //b = (byte)((i >> 8) & 0xff);
            ServerManage.Instance.Start();
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
