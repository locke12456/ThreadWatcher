using EnvDTE;
using EnvDTE90a;
using libWatherDebugger.Script.Mode.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.BreakPoint
{
    public class AddBreakPoint : BreakPoint, IDebugBreakPoint
    {
        public string File
        {
            get;
            set;
        }
        public string Function
        {
            get;
            set;
        }
        public int Line
        {
            get;
            set;
        }
        public int Column
        {
            get;
            set;
        }
        public string Condition
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public EnvDTE.dbgBreakpointConditionType ConditionType
        {
            get;
            set;
        }
        public AddBreakPoint()
            : base()
        {
            Mode = dbgDebugMode.dbgBreakMode;
        }
        public AddBreakPoint(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
            File = ""; Function = "";
            Line = 0; Column = 0;
            Condition = "";
            ConditionType = EnvDTE.dbgBreakpointConditionType.dbgBreakpointConditionTypeWhenTrue;
        }
        protected override bool _tyrToControl()
        {
            Breakpoints bps = _dbg.Breakpoints.Add(Function, File, Line, Column, Condition, ConditionType, "C++");

            foreach (var bp_c in bps)
            {
                Breakpoint3 bp = bp_c as Breakpoint3;
                if (bp.File == File && bp.FileLine == Line)
                {
                    Breakpoint = bp;
                    bp.BreakWhenHit = false;
                    bp.Message = Message;
                }
            }
            return true;
        }
    }
}
