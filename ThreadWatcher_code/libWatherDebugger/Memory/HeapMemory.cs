using libUtilities;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.Memory
{
    /// <summary>
    /// 用於儲存watchpoint資訊
    /// </summary>
    public class HeapMemory : MemoryInfo
    {
        public bool InWatchList { get; set; }
        public HeapMemory() : base()
        {
            InWatchList = false;
        }
        public HeapMemory(IDebuggerMemory data)
            : base() 
        {
            Variable = data.Value;
            Members = data.Members;
            Value = data.Value;
            Address = data.Address;
        }
        public override string ToString()
        {
            return Variable;
        }
    }
}
