using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class TargetThread 
    {
        public ThreadItem Thread;
        public List<BreakpointHitLocation> Locations { get; set; }
        public TargetThread() { }
    }
}
