using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Basedata
{
    public abstract class ServerManager
    {
        public const int SERVER_IDLE = 0;
        public const int SERVER_STOP = 1;
        public const int SERVER_RUN = 2;
        public const int SERVER_PAUSE = 3;

        public virtual void Start() { }
        public virtual void Stop() { }
        public virtual void Pause() { }
        public virtual void Restart() { }

        public virtual int GetState() { return SERVER_STOP; }
    }
}
