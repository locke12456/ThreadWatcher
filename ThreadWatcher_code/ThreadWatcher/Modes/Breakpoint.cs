using libWatherDebugger.Breakpoint;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio.Debugger.Interop;

namespace ThreadWatcher.Modes
{
    public class Breakpoint : EventMode<IDebugBreakpointEvent2>
    {
        public Breakpoint() 
        {
        
        }
        protected override void _rule()
        {
            _init_stackframe();
            _triggered_breakpoint();
        }

        private void _triggered_breakpoint()
        {
            DebugBreakpointFactory bpFactory = new DebugBreakpointFactory(Event);
            bpFactory.CreateProduct();
            DebugBreakpoint breakpoint = bpFactory.Product as DebugBreakpoint;
            _gui.Breakpoints.BreakPointTriggered(breakpoint);
        }

        private void _init_stackframe()
        {
            _gui.Debugger.InitStackFrame(_thread);
            DebugStackFrame stack = _gui.Debugger.CurrentStackFrame as DebugStackFrame;
            _gui.Debugger.Locals(stack);
        }
        protected override bool _precondition()
        {
            return _thread != null;
        }
    }
}
