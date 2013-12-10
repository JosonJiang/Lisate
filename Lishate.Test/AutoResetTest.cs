using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lishate.Test
{
    public class AutoResetTest
    {
        public AutoResetEvent are = new AutoResetEvent(false);
        public int SetCount = 10;
        public bool _isContinue = false;

        public void Set()
        {
            while (true)
            {
                for (int i = 0; i < SetCount; i++)
                {
                    Console.WriteLine("now set");
                    //_isContinue = true;
                    are.Set();
                }
                Thread.Sleep(5000);
                //_isContinue = false;
            }
        }

        public void Wait()
        {
            while (true)
            {
                //Console.WriteLine("are will wait");
                if (_isContinue == false)
                {
                    are.WaitOne();
                    Console.WriteLine("are after wait");
                }
            }
        }

        
    }
}
