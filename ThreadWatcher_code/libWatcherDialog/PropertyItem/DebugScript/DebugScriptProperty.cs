using ComponentOwl.BetterListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public class DebugScriptProperty : PropertyViewItem.PropertyViewItem
    {
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                return null;
            }
        }
        public DebugScriptProperty(string name, string value)
            : base(name, value)
        {

        }
        public override IBetterListViewEmbeddedControl ListViewRequestEmbeddedControl(object sender, BetterListViewRequestEmbeddedControlEventArgs eventArgs)
        {
            return (SubItems.IndexOf(eventArgs.SubItem) > 0) ? Control : null;
        }

    }
}
