using libWatherDebugger.Script;
using Microsoft.VisualStudio.Debugger.Interop;

namespace ThreadWatcher.Modes
{
    public class ReturnValue : EventMode<IDebugReturnValueEvent2>
    {
        public ReturnValue() 
        {
        }
        protected override void _rule()
        {
            if (DebugScript.HasASyncScript())
                DebugScript.FinishSync();
        }
    }
}
