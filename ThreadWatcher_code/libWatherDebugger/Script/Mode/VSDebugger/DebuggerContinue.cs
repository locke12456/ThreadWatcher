using System.Diagnostics;

namespace libWatherDebugger.Script.Mode.VSDebugger
{
    public class DebuggerContinue : DebuggerControl
    {
        private static readonly int max_try = 3;
        private int _try_agian = 0;
        public DebuggerContinue() : base() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            try
            {
                while (_dbg.CurrentMode != Mode) ;
                _dbg.Go(false);
                return true;
            }
            catch (System.Exception fail) 
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
