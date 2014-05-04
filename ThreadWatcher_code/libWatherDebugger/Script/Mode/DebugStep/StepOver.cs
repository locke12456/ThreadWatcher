using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.DebugStep
{
    public class StepOver : DebugScript
    {
        public StepOver() : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        public StepOver(EnvDTE.Debugger dbg) : base(dbg) {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            _dbg.StepOver(false);
            return true;
        }
    }
}
