using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.GeneralRules.Mode.Breakpoint
{
    public class BreakpointHit : ItemManagementRule
    {
        public DebugThread Thread { get; private set; }
        public BreakpointItem Breakpoint { get; private set; }
        public BreakpointHit(DebugThread thread, BreakpointItem bp)
            : base()
        {
            Thread = thread;
            Breakpoint = bp;
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() { _breakpoint, null };
        }
        private bool _breakpoint()
        {
            //Continue cnt = new Continue();
            //cnt.RunRules();
            BreakpointItem item = Breakpoint;
            DebugThread thread = Thread;
            //string name = item.Breakpoint.Name;
            string id = thread.ID;
            //_write_log(thread, name, id);
            return true;
        }

        
    }
}
