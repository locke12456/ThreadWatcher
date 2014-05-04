using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libWatcherDialog.List;
using libWatcherDialog.CombineRules;
using libWatherDebugger.Memory;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public partial class BreakpointMenu : PropertyDialogMenu
    {
        public BreakpointMenu() : base()
        {
            InitializeComponent();
        }

        private void dataBreakpointToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DataBreakpointList datas = new DataBreakpointList();
            datas.ModifiedEvent += BreakpointList_ModifiedEvent;
            datas.AddDatas(BreakpointsManagement.getInstance().Datas.GetDisplayList());
            datas.ShowDialog(this);
        }

        private void BreakpointList_ModifiedEvent(object sender, object SelectedItem)
        {
            AddDataBreakPointToForm addDatabp = new AddDataBreakPointToForm();
            addDatabp.Data = (SelectedItem as DataBreakpointListItem).MemoryAddressInfo;
            addDatabp.Run();
            (sender as DataBreakpointList).Close();
        }
    }
}
