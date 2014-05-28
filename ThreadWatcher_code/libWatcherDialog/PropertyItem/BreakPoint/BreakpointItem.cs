using libWatcherDialog.PropertyItem.BreakPoint.Property;
using libWatcherDialog.PropertyItem.BreakPoint.Property.BreakpointThread;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class BreakpointItem : PropertyItem<BreakPoints,BreakPointProperty>
    {
        public MemoryInfo Data { get; set; }
        public DebugBreakpoint Breakpoint { get; set; }
        public BreakpointHitLoactions HitLocations { get; private set; }
        public BreakpointCondition Condition { get; set; }
        public BreakpointItem() {
            HitLocations = new BreakpointHitLoactions();
            Condition = new BreakpointCondition();
            Children = new List<BreakPointProperty>();
            Children.Add(Condition);
            Children.Add(new BreakpointThreads());
            Children.Add(HitLocations);
           
        }
        public override string ToString()
        {
            return Data.Variable;
        }
        public bool Equals(string name)
        {
            name = name.Replace("0x", "").ToUpper();
            string data = Data.Variable.ToUpper();
            name = name.Split(new string[] { "\n" , ","}, StringSplitOptions.RemoveEmptyEntries)[0];
            return data.IndexOf(name) != -1;
        }
    }
}
