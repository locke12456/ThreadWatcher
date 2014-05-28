using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem
{
    public interface ISelectedMode
    {
        ListView Target { get; }
        List<ListViewItem> Items { get; }
        void Update();
    }
}
