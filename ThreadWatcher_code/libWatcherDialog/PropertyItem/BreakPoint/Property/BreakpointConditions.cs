using ComponentOwl.BetterListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointConditions : BreakPointProperty
    {
        private static readonly string Name = "Conditions";
        private static readonly string Value = "( ... )";
        public BreakpointConditions()
            : base(BreakpointConditions.Name, BreakpointConditions.Value)
        {

        }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                return null;
            }
        }
    }
}
