using libWatherDebugger.Breakpoint;
using libWatherDebugger.Script.Mode.BreakPoint;
using libWatherDebugger.Script.Mode.VSDebugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.GeneralRules.Mode.BreakPoint
{
    public class ResetBreakpointCondition : WatcherRule
    {
        public DebugBreakpoint Breakpoint { get; private set; }
        public string Condition { get; private set; }
        private AddWatchPoint _addbp;
        public ResetBreakpointCondition(DebugBreakpoint bp , string condition ) 
        {
            Breakpoint = bp;
            Condition = condition;
            _init();
        }
        protected override void _init()
        {
            _addbp = new AddWatchPoint();
            _script_list = new List<Func<bool>>() { new DebuggerBreak().Run, _reset_condition, _addbp.Run, new DebuggerContinue().Run, null };
        }
        private bool _reset_condition ()
        {
            Breakpoint.Information.Delete();
            _addbp.Condition = Condition;
            _addbp.Data = Breakpoint.Name;
            return true;
        }
    }
}
