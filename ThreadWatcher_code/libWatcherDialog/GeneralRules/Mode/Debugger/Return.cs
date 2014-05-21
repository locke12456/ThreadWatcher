using EnvDTE;
using libWatherDebugger.Script;
using libWatherDebugger.Script.Mode.DebugStep;
using libWatherDebugger.Script.Mode.Document;
using libWatherDebugger.Script.ScriptEvent;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules.Mode.Debugger
{
    public class Return : WatcherRule
    {
        private StepOut _stepOut;
       
        public Return() { }
        protected override void _init()
        {
            _stepOut = new StepOut();
            _script_list = new List<Func<bool>>() { 
                _stepOut.Run , _step
            };
            _stepOut.RegisterASyncEvent();
            _stepOut.ASyncCompleteEvent += _stepOutASyncCompleteEvent;
        }
        private void _stepOutASyncCompleteEvent(object sender, ScriptEventArgs e)
        {
            
        }
        private bool _step() 
        {
            DebugScript.WaitSync();
            while (_dbg.VSDebugger.CurrentMode != EnvDTE.dbgDebugMode.dbgBreakMode) ;
            System.Diagnostics.Debug.WriteLine(_dbg.FunctionName);
            foreach (Project proj in _dbg.VSDebugger.DTE.Solution.Projects) {
                foreach (ProjectItem proj_item in proj.ProjectItems) 
                {
                    if (proj_item.Document == _dbg.VSDebugger.DTE.ActiveDocument) {
                        
                    }
                }  
            }
            if (_isLeavedOnAPIs(_dbg.VSDebugger.CurrentStackFrame.FunctionName))
            {
                DebugScript.ResetASyncScript();
                _script_list.Add(null);
            }
            else
            {
                _stepOut.RegisterASyncEvent();
                _script_list.Add(_stepOut.Run);
                _script_list.Add(_step);
            }
            return true;
        }
        private bool _isLeavedOnAPIs(string func)
        {
            foreach (string api in libWatcher.Constants.APICppFiles.APIs)
                if (api == func) return false;
            return true;
        }

    }
}
