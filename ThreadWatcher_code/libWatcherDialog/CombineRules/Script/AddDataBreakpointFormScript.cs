using libUtilities;
using libWatcherDialog.GeneralRules.Mode.MemoryData;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.DebugScript;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Script;
using libWatherDebugger.Stack;
using libWatherDebugger.WatchAPI.Control;
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
            SetMallocActiveEnable disable = new SetMallocActiveEnable(false);

            _rules = new List<WatcherRule>() {
                _addToList , _addDataBreakpoint, disable ,new Continue() , LastRule
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
            _addDataBreakpoint.Watchtarget = bps.ConcernedTarget.Target.name;
            _addDataBreakpoint.WatchtargetIsPoniter = bps.ConcernedTarget.Target.IsPointer;
            _addDataBreakpoint.Type = bps.ConcernedTarget.BreakpointInfo.type;
            _addDataBreakpoint.Data = _addToList.Data;
            //DebugScript.FinishSync();
        }
        protected virtual void _addDataBreakpoint_RuleCompleted(object sender, RuleEventArgs e)
        {
            AddDataBreakPoint data = sender as AddDataBreakPoint;
            BreakpointsManagement bps = BreakpointsManagement.getInstance();
            foreach (var mem in data.Breakpoints)
            {
                HeapMemory memory = mem.Key as HeapMemory;
                _add_to_list(bps, memory);
               _add_breakpoint_item(bps, mem, memory);
            }
            //DebugScript.FinishSync();
        }

        private void _add_breakpoint_item(BreakpointsManagement bps, KeyValuePair<IDebuggerMemory, DebugBreakpoint> mem, HeapMemory memory)
        {
            if (!memory.InWatchList)
            {
                mem.Value.Information.Delete();
                return;
            }
            BreakpointItem bpItem = new BreakpointItem();
            bpItem.Data = memory;
            bpItem.Breakpoint = mem.Value;
            bpItem.Condition.Condition = bps.ConcernedTarget.Conditions.DebugCondition;
            bpItem.Breakpoint.Condition = bpItem.Condition.Condition;
            bps.AddItem(bpItem);
        }

        private static void _add_to_list(BreakpointsManagement bps, HeapMemory memory)
        {
            DataBreakpointListItem list_item = new DataBreakpointListItem();
            list_item.MemoryAddressInfo = memory;
            memory.InWatchList = bps.Datas.GetWatchingList().Count < 4;
            bps.AddMemoryData(list_item);
        }

    }
}
