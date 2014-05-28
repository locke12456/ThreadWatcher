using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode
{
    public class StepOver : Step
    {
        public StepOver(EnvDTE.Debugger dbg) : base(dbg) {
        }
        protected override bool _command()
        {
            _dbg.StepOver(false);
            return true;
        }
    }
}
