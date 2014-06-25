using libUtilities;
using libWatherDebugger;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Script.Mode.BreakPoint;
using libWatherDebugger.Script.Mode.DebugStep;
using libWatherDebugger.Script.Mode.VSDebugger;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher.Debugger.GeneralRules.Mode.BreakPoint
{
    public class AddDataBreakPoint : BreakpointRule
    {
        public string Type { get; set; }
        public string Watchtarget { get; set; }
        public bool WatchtargetIsPoniter { get; set; }
        public AddDataBreakPoint()
        {
            Watchtarget = "";
            Condition = "";
        }
        protected override void _init()
        {
            IDebuggerMemory info = null;
            double data_size = Convert.ToDouble(Data.Size);
            double type_size = data_size;
            if (Type != "")
            {
                info = _dbg.Query("sizeof(" + Type + ")");
                type_size = Convert.ToDouble(info.Value);
            }
            double dataSize = Math.Ceiling(data_size / type_size);
            string address = Data.Variable.Replace("0x", "");
            long addr = _set_watchtarget(address);// Convert.ToInt64(address);
            _initRuleList(addr, 1, 0);
        }

        private long _set_watchtarget(string address)
        {
            if (Watchtarget != "")
            {
                IDebuggerMemory info = null;
                //&(*((ListPointer *)0x005348b0)).Current
                string type = WatchtargetIsPoniter ? "" : "&";
                string query_string = type + "(*((" + Type + " *)0x" + address + "))." + Watchtarget + "";
                info = _dbg.QueryAddress(query_string);
                address = info.Address.Replace("0x", "");
            }
            long addr = Int64.Parse(address, System.Globalization.NumberStyles.HexNumber);
            return addr;
        }


        private void _initRuleList(long addr, int size, int type_size)
        {
            Breakpoints = new Dictionary<IDebuggerMemory, DebugBreakpoint>();
            _script_list = new List<Func<bool>>();
            for (int i = 0; i < size; i += 1)
            {
                _breakpoint = new AddWatchPoint();
                AddWatchPoint bp = _breakpoint as AddWatchPoint;
                bp.Condition = Condition;
                long address = addr + i * type_size;
                bp.Data = "0x" + address.ToString("X8");
                bp.CompleteEvent += bp_CompleteEvent;
                _add_heap_memory_data(bp);
                _script_list.Add(bp.Run);
            }
            _script_list.Add(null);
        }

        private void _add_heap_memory_data(AddWatchPoint bp)
        {
            HeapMemory info = null;
            DebugBreakpoint Breakpoint = new DebugBreakpoint();
            if (Type != "")
            {
                info = new HeapMemory();
                info.Variable = info.Address = bp.Data;
                // info.Variable.Replace("(" + Type + " *)", "");
            }
            if (info != null)
                Breakpoints.Add(info, Breakpoint);
        }

        private void bp_CompleteEvent(object obj, libWatherDebugger.Script.ScriptEvent.ScriptEventArgs e)
        {
            AddWatchPoint data = (obj as AddWatchPoint);
            DebugBreakpoint Breakpoint = null;
            foreach (var bp in Breakpoints)
            {
                string address = bp.Key.Address.Replace("0x", "").ToUpper();
                string data_address = data.Data.Replace("0x", "");
                if (address == data_address)
                {
                    bp.Value.Information = data.Breakpoint;
                    break;
                }
            }
        }
        protected override bool _finish()
        {
            Breakpoint = new DebugBreakpoint();
            Breakpoint.Information = _breakpoint.Breakpoint;
            Watchtarget = "";
            bool result = base._finish();
            return result;
        }
    }
}
