using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointHitLoactions : BreakPointProperty
    {
        private static readonly string Name = "HitLoactions";
        private static readonly string Value = "( ... )";
        public BreakpointHitLoactions()
            : base(BreakpointHitLoactions.Name, BreakpointHitLoactions.Value)
        { 
        }
    }
}
