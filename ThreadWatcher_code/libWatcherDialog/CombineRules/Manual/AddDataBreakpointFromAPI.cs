using libUtilities;
using libWatcherDialog.GeneralRules.Mode.DebugScript;
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
using Watcher.Debugger.GeneralRules.Mode.BreakPoint;
using Watcher.Debugger.GeneralRules.Mode.Debugger;

namespace libWatcherDialog.CombineRules
{
    public class AddDataBreakpointFromAPI : BreakpointTriggerFromAPI
    {
        protected AddDataBreakPoint _addDataBreakpoint;
        private Return _return = null;

        public AddDataBreakpointFromAPI()
        {
            _initTypes();
            _init_rules();
        }

        private void _initTypes()
        {
            api_type = APITypes.Malloc;
            Data = null;
        }

        protected virtual void _init_rules()
        {
            _return = new Return();
            _addDataBreakpoint = new AddDataBreakPoint();
            _addDataBreakpoint.RuleCompleted += _addDataBreakpoint_RuleCompleted;
            _rules = new List<WatcherRule>() {
                _addDataBreakpoint , _return , LastRule
            };
        }

        protected virtual void _addDataBreakpoint_RuleCompleted(object sender, RuleEventArgs e)
        {
            AddDataBreakpointFromAPI rule = sender as AddDataBreakpointFromAPI;
            _addDataToManagement(rule);
        }

        private static void _addDataToManagement(AddDataBreakpointFromAPI rule)
        {
            BreakpointItem bpItem = new BreakpointItem();
            bpItem.Data = rule.Data as MemoryInfo;
            bpItem.Breakpoint = rule.Breakpoint;
            BreakpointsManagement.getInstance().AddItem(bpItem);
        }
        protected override void _init()
        {
            if (Data == null)
            {
                _initData();
                _set_data_breakpoint_info();
            }
        }
        private void _set_data_breakpoint_info()
        {
            _addDataBreakpoint.Data = Data;
            _addDataBreakpoint.Breakpoint = Breakpoint;
        }
        protected override void _finish()
        {
            Breakpoint = _addDataBreakpoint.Breakpoint;
            base._finish();
            _reset();
        }

        private void _reset()
        {
            Data = null;
            _return = new Return();
        }
    }
}
