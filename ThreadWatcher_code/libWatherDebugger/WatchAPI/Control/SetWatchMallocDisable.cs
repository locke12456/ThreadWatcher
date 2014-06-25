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
    public class SetWatchMallocDisable : DebugScript
    {
        public override event DebugScript.EventHandler ASyncCompleteEvent;
        private DebugStackFrame _stack;
        private DebugProperty _property;
        public SetWatchMallocDisable(DebugStackFrame stack) 
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

            if (!_init_property()) return true;

            Byte[] data = {0,0,0,1};
            _property.SetData(data);
            
            if (DebugScript.HasASyncScript())
                DebugScript.FinishSync();

            return true;
        }
        private bool _init_property()
        {
            DebugPropertyFactory factory = new DebugPropertyFactory(_stack);

            if (VSConstants.S_OK != factory.CreateProduct("____watch_malloc_active")) return false;

            _property = factory.ProductList[0] as DebugProperty;

            _property.DataSize = 4;

            return true;
        }
    }
}
