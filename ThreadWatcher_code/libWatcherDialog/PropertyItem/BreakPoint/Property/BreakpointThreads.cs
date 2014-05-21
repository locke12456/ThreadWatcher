using ComponentOwl.BetterListView;
using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointThreads : BreakPointProperty
    {
        private static readonly string Name = "Threads";
        private static readonly string Value = "( ... )";
        private BetterListViewComboBoxEmbeddedControl _combobox;
        public BreakpointThreads()
            : base(BreakpointThreads.Name, BreakpointThreads.Value)
        {

        }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                _combobox = new BetterListViewComboBoxEmbeddedControl();
                _combobox.Items.AddRange(ThreadsManagement.getInstance().GetList().ToArray());
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
            try
            {
                ThreadItem item = _combobox.SelectedItem as ThreadItem;
                BreakpointHitLoactions bhl = _get_hit_location();
                if (bhl == null) return;
                bhl.TargetThreadId = item.Thread.ID;
                bhl.SetValue(BreakpointThreads.Value);
                base.AfterLabelEdit(sender, eventArgs);
            }
            catch (Exception fail) {
                Debug.WriteLine("edit fail : " + fail.Message);
            }
        }

        private BreakpointHitLoactions _get_hit_location()
        {
            BreakpointHitLoactions bhl = null;
            foreach (var property in List.Items)
            {
                if (property is BreakpointHitLoactions)
                    bhl = property as BreakpointHitLoactions;
            }
            return bhl;
        }
    }
}
