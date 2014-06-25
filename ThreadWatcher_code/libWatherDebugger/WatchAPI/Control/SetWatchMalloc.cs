using libWatherDebugger.Memory;
using libWatherDebugger.Property;
using libWatherDebugger.Script;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMalloc
    {
        private DebugStackFrame _stack;
        public SetWatchMalloc(DebugStackFrame stack)
        {
            _stack = stack;
        }
        public void Enable()
        {
            SetMallocActiveEnable enable = new SetMallocActiveEnable(_stack);
            enable.RunRules();
            DebugScript.WaitSync();
            return;
        }
        public void Disable()
        {

        }
    }
}
