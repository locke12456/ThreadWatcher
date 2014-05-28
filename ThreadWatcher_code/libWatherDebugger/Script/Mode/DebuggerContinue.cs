using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode
{
    public class DebuggerContinue : Step
    {
        public DebuggerContinue(EnvDTE.Debugger dbg)
            : base(dbg)
        {
        }
        protected override bool _command()
        {
            _dbg.Go(false);
            return true;
        }
    }
}
