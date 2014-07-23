using EnvDTE;
using System;
using System.Collections.Generic;

namespace libWatherDebugger.Script.Mode.VSDebugger
{
    public class DebuggerBreak : DebuggerControl
    {
        public DebuggerBreak()
            : base()
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
