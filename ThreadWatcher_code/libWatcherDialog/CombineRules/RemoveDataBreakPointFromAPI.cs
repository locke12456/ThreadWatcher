using libUtilities;
using libWatcherDialog.GeneralRules.Mode.MemoryData;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Logger;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Script.Mode.Document;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
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
    public class RemoveDataBreakPointFromAPI : BreakpointTriggerFromAPI
    {
        public static readonly string Name = "MemoryFreed";
        public static readonly string Created = "a data address has freed .";
        private DeleteDataBreakpoint _deldp;
        private FreeHeapMemoryData _hasFreed;
        public RemoveDataBreakPointFromAPI()
        {
            _reset();
            _init_rules();
        }

        private void _reset()
        {
            api_type = APITypes.Free;
            Data = null;
        }

        private void _init_rules()
        {
            _deldp = new DeleteDataBreakpoint();
            _hasFreed = new FreeHeapMemoryData();
            _deldp.RuleCompleted += _deldp_RuleCompleted;
            _rules = new List<WatcherRule>() {
                    _deldp , _hasFreed , new Continue() , LastRule
                };
        }

        private void _deldp_RuleCompleted(object sender, RuleEventArgs e)
        {
            DeleteDataBreakpoint rule = sender as DeleteDataBreakpoint;
            _hasFreed.Data = rule.Data;
        }
        protected override void _finish()
        {
            base._finish();
            _reset();
        }
        protected override void _init()
        {
            if (Data == null)
            {
                _initData();
                _set_delete_data();
            }
        }

        private void _set_delete_data()
        {
            _deldp.Data = Data;
            _deldp.Breakpoint = Breakpoint;
        }

    }
}
