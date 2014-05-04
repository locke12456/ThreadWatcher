
using libWatherDebugger.Script.Mode.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.BreakPoint
{
    public class BreakPoint : DebugScript
    {
        public EnvDTE.Breakpoint Breakpoint { get;protected set; }
        public BreakPoint() : base() 
        {
        }
        public BreakPoint(EnvDTE.Debugger dbg)
            : base(dbg)
        {
        }
        public void Remove() {
        
        }
    }
}
