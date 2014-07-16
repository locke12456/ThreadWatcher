using Microsoft.VisualStudio.Debugger.Interop;
using ThreadWatcher.GUI;

namespace ThreadWatcher.Modes
{
    public class ProcessCreate : EventMode<IDebugProcessCreateEvent2>
    {
        public ProcessCreate() 
        {
            
        }
        protected override void _rule()
        {
            _gui = GUIManagement.getInstance();

            /*
             * breakpoints = new BreakPoints();
             * threads = new libWatcherDialog.Threads();
             * _dte.Debugger.Breakpoints.Add("", APICppFiles.APIFileName, APICppFiles.MemoryAllocLine, 1,"____watch_malloc_active");
             * _dte.Debugger.Breakpoints.Add("", APICppFiles.APIFileName, APICppFiles.MemoryFreeLine, 1, "____watch_free_active");
             * DebugScriptItem item;
             * item = new DebugScriptItem();
             * item.BreakpointInfo = new libWatcherDialog.DebugScriptEngine.Property.SourceFileInfo();
             * item.BreakpointInfo.line = 41;
             * item.BreakpointInfo.filename = "ConsoleApplication1.cpp";
             * _dte.Debugger.Breakpoints.Add("", item.BreakpointInfo.filename , item.BreakpointInfo.line, 1);
             * _dte.Debugger.Breakpoints.Add("", "dbgdel.cpp", 42, 1);
             * Debug.WriteLine(pEvent);
             *  */
        }
    }
}
