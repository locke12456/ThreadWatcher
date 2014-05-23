using libWatcherDialog.GeneralRules.Mode;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.GeneralRules.Mode.Logs
{
    public class WriteLog : ItemManagementRule
    {

        public DebugThread Thread { get; private set; }
        public ThreadLog Log { get; private set; }
        public WriteLog(DebugThread thread,ThreadLog log)
            : base()
        {
            Log = log;
            Thread = thread;
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() {_write_log,null };    
        }
        private bool _write_log()
        {
            ThreadLog log = Log;
            _logs.AddItem(log);
            BreakpointItem target = _breakpoints.GetItem(log.Name);
            target.HitLocations.BreakpointHit(Thread);
            return true;
        }
    }
}
