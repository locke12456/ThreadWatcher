using EnvDTE;
using libWatherDebugger.Script;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;

namespace libWatherDebugger.WatchAPI
{
    public abstract class WatchAPISetting  : DebugScript
    {
        public override event DebugScript.EventHandler ASyncCompleteEvent;
        public override Type ASyncEventType
        {
            get
            {
                return typeof(IDebugBreakpointEvent2);
            }
        }
        protected override bool _tyrToControl()
        {
            //while (_dbg.CurrentMode != EnvDTE.dbgDebugMode.dbgBreakMode);

            Watcher.Debugger.Debugger dbg = Watcher.Debugger.Debugger.getInstance();

            _setting(dbg);

            if (DebugScript.HasASyncScript())
                DebugScript.FinishSync();

            return true;
        }
        protected virtual void _setting(Watcher.Debugger.Debugger dbg) 
        {
        
        }
        protected override void _init()
        {
            _switchMode = new Dictionary<dbgDebugMode, Func<bool>>() {
                {dbgDebugMode.dbgBreakMode , _command},
                {dbgDebugMode.dbgDesignMode , _wait},
                {dbgDebugMode.dbgRunMode , _command},
            };
        }
    
    }
}
