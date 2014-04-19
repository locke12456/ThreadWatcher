using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class BreakpointHitLocation
    {
        public string FileName { get; set; }
        public uint LineNumber { get; set; }
        public uint HitCount { get; set; }
        public BreakpointHitLocation() { }
    }
}
