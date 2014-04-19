using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property.BreakpointThread
{
    public class BreakpointCondition : BreakpointThreadsProperty
    {
        // thread , document , line , code
        public string Condition { get; set; }
        public BreakpointCondition(string name, string value)
            : base(name, value)
        {
        }
    }
}
