using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Lishate.Basedata
{
    public class SynList
    {
        private ArrayList _list = new ArrayList();
        private object _synObj = new object();

        public void Add(object o)
        {
            lock (_synObj)
            {
                _list.Add(o);
                Monitor.Pulse(_synObj);
            }
        }

        public object Get()
        {
            lock (_synObj)
            {
                while (true)
                {
                    if (_list.Count > 0)
                    {
                        object o = (object)_list[0];
                        _list.RemoveAt(0);
                        return o;
                    }
                    else
                    {
                        Monitor.Wait(_synObj);
                    }
                }
            }
        }
    }
}
