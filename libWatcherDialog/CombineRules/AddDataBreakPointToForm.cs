using libUtilities;
using libWatcherDialog.GeneralRules.Mode.BreakPoint;
using libWatcherDialog.GeneralRules.Mode.Debugger;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.CombineRules
{
    public class AddDataBreakPointToForm : CombineRules , IBreakPoint
    {
        private AddDataBreakPoint adddbp;
        public DebugBreakpoint Breakpoint { get; set; }
        public IDebuggerMemory Data { get; set; }
        public AddDataBreakPointToForm()
            : base()
        {
            Data = null;
            adddbp = new AddDataBreakPoint();
            _rules = new List<GeneralRules.WatcherRule>() {
                new Break(), adddbp , LastRule
            };
        }
        protected override void _init()
        {
            adddbp.Data = Data;
        }
        protected override void _finish()
        {
            base._finish();
            HeapMemory memory = adddbp.Data as HeapMemory;
            memory.InWatchList = true;
            BreakpointItem bpItem = new BreakpointItem();
            bpItem.Data = memory;
            bpItem.Breakpoint = adddbp.Breakpoint;
            BreakpointsManagement.getInstance().AddItem(bpItem);
        }
    }
}
