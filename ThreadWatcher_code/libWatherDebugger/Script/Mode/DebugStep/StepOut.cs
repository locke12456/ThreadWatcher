using libWatherDebugger.Script.ScriptEvent;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.DebugStep
{
    public class StepOut : DebugScript
    {
        public override event DebugScript.EventHandler ASyncCompleteEvent;
        public StepOut()
            : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        public override Type ASyncEventType
        {
            get
            {
                return typeof(IDebugReturnValueEvent2);
            }
        }
        public StepOut(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            _dbg.StepOut(false);
            
            return true;
        }
    }
}
