using EnvDTE;
using libWatherDebugger.Script;
using libWatherDebugger.Script.Mode.DebugStep;
using libWatherDebugger.Script.Mode.Document;
using libWatherDebugger.Script.ScriptEvent;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher.Debugger.GeneralRules.Mode.Debugger
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
            //ProjectItem item = _dbg.VSDebugger.DTE.Solution.FindProjectItem(_dbg.VSDebugger.DTE.ActiveDocument.FullName);
            //if (item != null)
            //{
            //    Debug.WriteLine(item.Name);
            //}
            _check_document();
            return true;
        }

        private bool _recursive_search_documents(ProjectItem item)
        {
            foreach (ProjectItem proj_item in item.ProjectItems)
            {
                if (_is_document(proj_item))
                {

                }
            }
            return false;
        }

        private bool _is_document(ProjectItem proj_item)
        {

            if (proj_item.Document == null) return _recursive_search_documents(proj_item);

            return true;
        }

        private void _check_document()
        {
            if (_isLeavedOnAPIs(_dbg.VSDebugger.CurrentStackFrame.FunctionName))
            {
                _is_finished();
            }
            else
            {
                _not_leave();
            }
        }

        private void _is_finished()
        {
            DebugScript.ResetASyncScript();
            _script_list.Add(null);
        }

        private void _not_leave()
        {
            _stepOut.RegisterASyncEvent();
            _script_list.Add(_stepOut.Run);
            _script_list.Add(_step);
        }
        private bool _isLeavedOnAPIs(string func)
        {
            foreach (string api in libWatcher.Constants.APICppFiles.APIs)
                if (api == func) return false;
            //if (func.IndexOf("std::") == -1)
            //    return false;
            return true;
           
        }

    }
}
