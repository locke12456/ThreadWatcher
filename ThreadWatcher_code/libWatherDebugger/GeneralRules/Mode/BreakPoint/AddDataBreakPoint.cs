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
        public AddDataBreakPoint()
        {
            Condition = "";
        }
        protected override void _init()
        {
            double dataSize = 1;// Math.Ceiling( Convert.ToDouble(watchInfo.Size) / 4 );
            string address = Data.Variable.Replace("0x", "");
            long addr = Int64.Parse(address, System.Globalization.NumberStyles.HexNumber);// Convert.ToInt64(address);
            _initRuleList(addr, (int)dataSize);
        }
        private void _initRuleList(long addr, int size)
        {
            _script_list = new List<Func<bool>>();
            for (int i = 0; i < size; i += 1)
            {
                _breakpoint = new AddWatchPoint();
                AddWatchPoint bp = _breakpoint as AddWatchPoint;
                bp.Condition = Condition;
                long address = addr + i * 4;
                bp.Data = "0x" + address.ToString("X8");
                _script_list.Add(bp.Run);
            }
            _script_list.Add(null);
        }
        protected override bool _finish()
        {
            Breakpoint = new DebugBreakpoint();
            Breakpoint.Information = _breakpoint.Breakpoint;
            bool result = base._finish();
            return result;
        }
    }
}
