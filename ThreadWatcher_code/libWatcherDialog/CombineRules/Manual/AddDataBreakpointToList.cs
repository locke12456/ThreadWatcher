using libUtilities;
using libWatcherDialog.GeneralRules.Mode.DebugScript;
using libWatcherDialog.GeneralRules.Mode.MemoryData;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.EventArgs;
using Watcher.Debugger.GeneralRules.Mode.Debugger;

namespace libWatcherDialog.CombineRules
{
    public class AddDataBreakpointToList : BreakpointTriggerFromAPI
    {
        private Return _return = null;
        private AddHeapMemoryData _addToList;
        public AddDataBreakpointToList()
        {
            api_type = APITypes.Malloc;
            Data = null;
            _init_rulelist();
        }

        protected virtual void _init_rulelist()
        {
            _addToList = new AddHeapMemoryData();
            _return = new Return();
            _return.RuleCompleted += _return_RuleCompleted;
            _rules = new List<WatcherRule>() {
                _addToList, _return , LastRule
            };
        }

        private void _return_RuleCompleted(object sender, RuleEventArgs e)
        {
            BreakpointsManagement bps = BreakpointsManagement.getInstance();
            DataBreakpointListItem item = bps.Datas.GetData(Data.Variable);
            item.File = new System.IO.FileInfo(_dbg.FileName);
            item.Position = _dbg.LineNumber;
        }
        protected override void _init()
        {
            if (Data == null)
            {
                _initData();
                _addToList.Data = Data;
            }
        }
        protected override void _finish()
        {
            base._finish();
            Data = null;
            _return = new Return();
        }
    }
}
