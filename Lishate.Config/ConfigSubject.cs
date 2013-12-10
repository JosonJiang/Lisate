using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lishate.Config
{
    public sealed class ConfigSubject
    {
        private ArrayList configs = new ArrayList();

        private ConfigSubject()
        {
        }

        public static readonly ConfigSubject Instance = new ConfigSubject();

        public void AddConfig(BaseConfig bc)
        {
            configs.Add(bc);
        }

        public void RemoveConfig(BaseConfig bc)
        {
            configs.Remove(bc);
        }

        public void Notify()
        {
            foreach (BaseConfig bc in configs)
            {
                bc.Update();
            }
        }
    }
}
