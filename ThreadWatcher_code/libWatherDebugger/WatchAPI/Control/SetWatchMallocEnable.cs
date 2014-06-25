using libWatherDebugger.Property;
using libWatherDebugger.Script;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMallocEnable : DebugScript
    {
        public override event DebugScript.EventHandler ASyncCompleteEvent;
        private DebugStackFrame _stack;
        private DebugProperty _property;
        private string _address;
        public SetWatchMallocEnable(DebugStackFrame stack) 
        {
            _stack = stack;
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        public override Type ASyncEventType
        {
            get
            {
                return typeof(IDebugBreakpointEvent2);
            }
        }
        protected override bool _tyrToControl()
        {
            while (_dbg.CurrentMode != EnvDTE.dbgDebugMode.dbgBreakMode);

            Watcher.Debugger.Debugger dbg = Watcher.Debugger.Debugger.getInstance();

            dbg.TryQuery("____watch_malloc_active = true");

            if (DebugScript.HasASyncScript())
                DebugScript.FinishSync();

            return true;
        }
        private bool _init_property()
        {
            DebugPropertyFactory factory = new DebugPropertyFactory(_stack);

            if (VSConstants.S_OK != factory.CreateProduct(_address)) return false;

            _property = factory.ProductList[0] as DebugProperty;

            _property.DataSize = 4;

            return true;
        }
    }
}
