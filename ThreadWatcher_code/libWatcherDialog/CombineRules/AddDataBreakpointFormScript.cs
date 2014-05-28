using libUtilities;
using libWatcherDialog.GeneralRules.Mode.MemoryData;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.DebugScript;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Script;
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
    public class AddDataBreakpointFormScript : BreakpointTriggerFromAPI
    {
        protected DebugBreakpoint _bp;
        protected IDebuggerMemory _data;
        protected AddDataBreakPoint _addDataBreakpoint;
        protected AddHeapMemoryData _addToList;
        public override DebugBreakpoint Breakpoint
        {
            get
            {
                return _bp;
            }
            set
            {
                _bp = value;
            }
        }
        public override IDebuggerMemory Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                if (_addToList != null)
                    _addToList.Data = _data;
            }
        }
        public AddDataBreakpointFormScript()
        {
            _init_rulelist();
        }
        protected virtual void _init_rulelist()
        {
            _addToList = new AddHeapMemoryData();
            _addDataBreakpoint = new AddDataBreakPoint();
            _addToList.RuleCompleted += _addToList_RuleCompleted;
            _addDataBreakpoint.RuleCompleted += _addDataBreakpoint_RuleCompleted;
            _rules = new List<WatcherRule>() {
                _addToList , _addDataBreakpoint, new Continue() , LastRule
            };
            Data = null;
        }
        protected override void _start()
        {
            //_wait();
            //DebugScript.RegisterASyncEvent(DebugScript.ASyncEvent);
        }
        protected override void _wait()
        {
            //if (DebugScript.HasASyncScript())
            //    DebugScript.WaitSync();
        }
        protected virtual void _addToList_RuleCompleted(object sender, RuleEventArgs e)
        {
            BreakpointsManagement bps = BreakpointsManagement.getInstance();
            _addDataBreakpoint.Data = _addToList.Data;
            //DebugScript.FinishSync();
        }
        protected virtual void _addDataBreakpoint_RuleCompleted(object sender, RuleEventArgs e)
        {
            HeapMemory memory = _addDataBreakpoint.Data as HeapMemory;
            memory.InWatchList = true;
            BreakpointItem bpItem = new BreakpointItem();
            bpItem.Data = memory;
            bpItem.Breakpoint = _addDataBreakpoint.Breakpoint;
            bpItem.Condition.Condition = BreakpointsManagement.getInstance().ConcernedTarget.Conditions[0].DebugCondition;
            bpItem.Breakpoint.Condition = bpItem.Condition.Condition;
            BreakpointsManagement.getInstance().AddItem(bpItem);
            //DebugScript.FinishSync();
        }

    }
}
