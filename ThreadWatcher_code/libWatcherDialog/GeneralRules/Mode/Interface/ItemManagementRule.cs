using libUtilities;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Code;
using libWatcherDialog.PropertyItem.DebugScript;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.GeneralRules.Mode
{
    public abstract class ItemManagementRule : WatcherRule 
    {
        protected BreakpointsManagement _breakpoints = BreakpointsManagement.getInstance();
        protected LogManagement _logs = LogManagement.getInstance();
        protected ThreadsManagement _threads = ThreadsManagement.getInstance();
        protected CodeMenagement _codes = CodeMenagement.getInstance();
        protected DebugScriptsManagement _scripts = DebugScriptsManagement.getInstance();
    }
}
