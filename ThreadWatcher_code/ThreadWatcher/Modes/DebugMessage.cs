using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.Message;
using libWatherDebugger.Thread;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

namespace ThreadWatcher.Modes
{
    public class DebugMessage : EventMode<IDebugMessageEvent2>
    {
        public DebugMessage() {
        }
        protected override void _rule()
        {
            DebugMessageFactory factory = new DebugMessageFactory(Event);
            DebugThreadFactory threadfactory = new DebugThreadFactory(_thread);
            //factory.CreateProduct(thread);
            //ThreadItem item = ThreadsManagement.getInstance().GetItem(thread) as ThreadItem;
            //ThreadsManagement.getInstance().SetCurrentItem(item);
            if (VSConstants.S_OK == factory.CreateProduct())
            {
                string msg = (factory.Product as libWatherDebugger.Message.DebugMessage).Message;
                threadfactory.CreateProduct(_thread);
                _gui.Debugger.InitStackFrame(threadfactory.Product);
                ThreadLog log = new ThreadLog();
                log.Name = msg;
                log.Key = threadfactory.Product.ID;
                LogManagement.getInstance().AddItem(log);
                BreakpointItem target = BreakpointsManagement.getInstance().GetItem(log.Name);
                target.HitLocations.BreakpointHit(threadfactory.Product);
                //item.WriteLog(msg);
            }
        }
    }
}
