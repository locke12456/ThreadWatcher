using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem
{
    public abstract class SelectedMode
    {
        public ListView Target { get; protected set; }
        public List<ListViewItem> Items { get; protected set; }
        public void Update() {
            
        }
    }
}
