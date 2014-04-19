using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.VSDebugger
{
    public class DebuggerContinue : DebugScript
    {
        private static readonly int max_try = 3;
        private int _try_agian = 0;
        public DebuggerContinue() : base() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }

        public DebuggerContinue(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            try
            {
                _dbg.Go(false);
                return true;
            }
            catch (Exception fail) 
            {
                if (++_try_agian < max_try)
                {
                    Debug.WriteLine(fail.Message + ", try agian .");
                    return false;
                }
                throw (fail);
            }
        }
    }
}
