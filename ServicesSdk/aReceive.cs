using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesSdk
{
    public abstract class aReceive
    {
        public abstract void ConsumeMessage(string list );
        private List<string> ValidationList { get; set; }
        
    }
}
