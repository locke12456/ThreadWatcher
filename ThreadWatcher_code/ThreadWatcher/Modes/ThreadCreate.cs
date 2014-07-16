using libWatherDebugger.Thread;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadWatcher.Modes
{
    public class ThreadCreate : EventMode<IDebugThreadCreateEvent2>
    {
        public ThreadCreate() 
        {
        }
        protected override void _rule()
        {
            DebugThreadFactory factory = new DebugThreadFactory(_thread);
            factory.CreateProduct(_thread);
            _gui.Threads.AddThread(factory.Product as DebugThread);
        }
        protected override bool _precondition()
        {
            return _thread != null;
        }
    }
}
