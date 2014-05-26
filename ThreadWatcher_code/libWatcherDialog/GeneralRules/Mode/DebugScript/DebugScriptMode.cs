using libWatcherDialog.PropertyItem.DebugScript;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.GeneralRules.Mode.DebugScript
{
    public class DebugScriptRule : ItemManagementRule
    {
        
        public DebugScriptRule() :base() {
            _init();
            
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() { _check_script_rule, null };
        }
        private bool _check_script_rule()
        {
            while (_dbg.VSDebugger.CurrentMode != EnvDTE.dbgDebugMode.dbgBreakMode) ;

            
            return true;
        }
    }
}
