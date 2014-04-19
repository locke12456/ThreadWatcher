using ComponentOwl.BetterListView;
using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointThreads : BreakPointProperty
    {
        private static readonly string Name = "Threads";
        private static readonly string Value = "( ... )";
        public BreakpointThreads():base(BreakpointThreads.Name,BreakpointThreads.Value) 
        {
        
        }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                BetterListViewComboBoxEmbeddedControl combobox = new BetterListViewComboBoxEmbeddedControl();
                combobox.Items.AddRange(ThreadsManagement.getInstance().GetList().ToArray());
                return combobox;
            }
        }
    }
}
