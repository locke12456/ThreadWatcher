using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode
{
    public class Step : IDebugScript
    {
        protected EnvDTE.Debugger _dbg;
        public Step(EnvDTE.Debugger dbg)
        {
            _dbg = dbg;
        }

        public bool Run()
        {
            switch (_dbg.CurrentMode)
            {
                case dbgDebugMode.dbgBreakMode:
                    return _command();
                    break;
            }
            return false;
        }

        protected virtual bool _command()
        {
            return true;
        }

    }
}
