using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.Memory
{
    public class HeapMemory : MemoryInfo
    {
        public bool InWatchList { get; set; }
        public HeapMemory() : base()
        {
            InWatchList = false;
        }
        public override string ToString()
        {
            return Variable;
        }
    }
}
