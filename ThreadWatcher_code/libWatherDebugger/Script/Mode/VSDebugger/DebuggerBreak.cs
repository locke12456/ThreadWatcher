using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.VSDebugger
{
    public class DebuggerBreak : DebugScript
    {
        public DebuggerBreak()
            : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgRunMode;
        }
        public DebuggerBreak(EnvDTE.Debugger dbg)
            : base(dbg)
        {
            Mode = EnvDTE.dbgDebugMode.dbgRunMode;
        }
        protected override void _init()
        {
            _switchMode = new Dictionary<dbgDebugMode, Func<bool>>() {
                {dbgDebugMode.dbgBreakMode , _wait},
                {dbgDebugMode.dbgDesignMode , _wait},
                {dbgDebugMode.dbgRunMode , _command },
            };
        }
        protected override bool _tyrToControl()
        {
            _dbg.Break(false);
            return true;
        }
    }
}
