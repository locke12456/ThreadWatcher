using ComponentOwl.BetterListView;
using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointVirtualVariables : BreakPointProperty
    {
        private static readonly string Name = "VirtualVariables";
        private static readonly string Value = "( ... )";
        private BetterListViewComboBoxEmbeddedControl _combobox;
        public BreakpointVirtualVariables()
            : base(BreakpointVirtualVariables.Name, BreakpointVirtualVariables.Value)
        {

        }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                _combobox = new BetterListViewComboBoxEmbeddedControl();
                //_combobox.Items.AddRange(ThreadsManagement.getInstance().GetList().ToArray());
                return _combobox;
            }
        }
        //public override IBetterListViewEmbeddedControl ListViewRequestEmbeddedControl(object sender, BetterListViewRequestEmbeddedControlEventArgs eventArgs)
        //{
        //    return Control;
        //}
        public override void AfterLabelEdit(object sender, BetterListViewLabelEditEventArgs eventArgs)
        {
            if (_combobox.SelectedItem == null) return;
            //ThreadItem item = _combobox.SelectedItem as ThreadItem;
            //BreakpointHitLoactions bhl = List.Items[1] as BreakpointHitLoactions;
            //bhl.TargetThreadId = item.Thread.ID;
            //bhl.SetValue(BreakpointVirtualVariables.Value);
            //base.AfterLabelEdit(sender, eventArgs);
        }
    
    }
}
