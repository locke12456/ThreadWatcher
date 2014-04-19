using libUtilities;
using libWatherDebugger.Breakpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules.Mode.BreakPoint
{
    public interface IBreakPoint
    {
        DebugBreakpoint Breakpoint { get; set; }
        IDebuggerMemory Data { get; set; }
    }
    public abstract class BreakpointRule : WatcherRule, IBreakPoint
    {
        public DebugBreakpoint Breakpoint { get; set; }
        public IDebuggerMemory Data { get; set; }
        protected libWatherDebugger.Script.Mode.BreakPoint.BreakPoint _breakpoint;

    }
}
