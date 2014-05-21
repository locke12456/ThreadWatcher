using ComponentOwl.BetterListView;
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
        private const string _name = "Condition";
        // thread , document , line , code
        private BetterListViewComboBoxEmbeddedControl _combobox;
        public string Condition
        {
            get {
                return SubItems[1].Text;
            }
            set
            {
                SetValue(value);
            }
        }
        public BreakpointCondition()
            : base(_name, "")
        {
        }
        public BreakpointCondition(string name, string value)
            : base(name, value)
        {
        }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                _combobox = new BetterListViewComboBoxEmbeddedControl();
                _combobox.Items.Add(Condition);
                return _combobox;
            }
        }
        public override void AfterLabelEdit(object sender, BetterListViewLabelEditEventArgs eventArgs)
        {
            if (_combobox.LabelText == Value) return;
            Condition = _combobox.LabelText;
            BreakpointsManagement.getInstance().CurrentItem.Breakpoint.Condition = Condition;
           
        }
    }
}
