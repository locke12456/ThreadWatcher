using ComponentOwl.BetterListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    public abstract class ItemProperties : ComponentOwl.BetterListView.BetterListViewItem
    {
        protected ComponentOwl.BetterListView.BetterListView parent;
        public ItemProperties(string[] items)
            : base(items)
        {

        }
        public ItemProperties(string name, string value)
            : base(new string[] { name, value })
        {
        }

        public virtual IBetterListViewEmbeddedControl Control { get { return null; } }
        public List<ItemProperties> Children
        {
            get;
            protected set;
        }
        public virtual IBetterListViewEmbeddedControl ListViewRequestEmbeddedControl(object sender, BetterListViewRequestEmbeddedControlEventArgs eventArgs)
        {
            return (SubItems.IndexOf(eventArgs.SubItem) != -1) ? Control : null;
        }
    }
}
