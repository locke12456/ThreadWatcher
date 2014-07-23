using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.DebugStep
{
    public class StepOver : DebugStep
    {
        public StepOver() : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            _dbg.StepOver(false);
            return true;
        }
    }
}
