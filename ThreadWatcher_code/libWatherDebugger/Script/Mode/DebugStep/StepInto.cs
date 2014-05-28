using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.DebugStep
{
    public class StepInto : DebugScript
    {
        public StepInto() : base() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        public StepInto(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            _dbg.StepInto(false);
            return true;
        }
    }
}
