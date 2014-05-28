using EnvDTE;
using EnvDTE90a;
using libUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.BreakPoint
{
    public class AddWatchPoint : BreakPoint
    {

        public string Data
        {
            get;
            set;
        }
        public string Condition
        {
            get;
            set;
        }
        public AddWatchPoint()
            : base()
        {
            Mode = dbgDebugMode.dbgBreakMode;
            Condition = "";
        }
        public AddWatchPoint(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            Breakpoint3 bp;
            EnvDTE90a.Debugger4 dbg = _dbg as Debugger4;
            Breakpoints bps = dbg.Breakpoints.Add("", "", 0, 0, Condition , EnvDTE.dbgBreakpointConditionType.dbgBreakpointConditionTypeWhenTrue, "C++", Data, 4);
            string data = Data.Replace("0x", "").ToUpper();
            foreach (var bp_c in bps)
            {
                bp = bp_c as Breakpoint3;
                if (bp.Name.IndexOf(data) != -1)
                {
                    Breakpoint = bp;
                }
            }
            return true;
        }
    }
}
