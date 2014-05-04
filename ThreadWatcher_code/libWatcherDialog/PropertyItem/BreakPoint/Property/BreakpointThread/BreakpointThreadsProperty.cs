using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointThreadsProperty : BreakPointProperty
    {
        public ThreadItem Thread;
        public string FileName { get; private set; }
        public uint LineNumber { get; private set; }
        public BreakpointThreadsProperty(string name, string value)
            : base(name, value)
        { 
        }
    }
}
