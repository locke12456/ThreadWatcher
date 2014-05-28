using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.BreakPoint
{
    public class RemoveWatchPonit : BreakPoint
    {
        public string Data
        {
            get;
            set;
        }
        public RemoveWatchPonit() : base() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            try
            {
                EnvDTE.Breakpoint breakpoint = null;
                string data = Data.Replace("0x", "");
                data = data.ToUpper();
                foreach (EnvDTE.Breakpoint bp in _dbg.Breakpoints)
                {
                    string name = bp.Name;
                    if (name.IndexOf(data) != -1)
                    {
                        breakpoint = bp;
                        break;
                    }
                }
                if (breakpoint != null)
                    breakpoint.Delete();
            }
            catch (Exception fail) {
                Debug.WriteLine(fail.Message);
            }
            return true;
        }
    }
}
