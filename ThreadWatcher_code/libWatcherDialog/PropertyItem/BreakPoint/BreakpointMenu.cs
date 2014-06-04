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
        protected BreakpointsManagement _breakpoints = BreakpointsManagement.getInstance();
        private DebugScripts _scriptDialog;
        public BreakpointMenu()
            : base()
        {
            InitializeComponent();
            Disposed += BreakpointMenu_Disposed;
        }

        private void BreakpointMenu_Disposed(object sender, System.EventArgs e)
        {
            _scriptDialog.CanDistory = true;
            _scriptDialog.Close();
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

        private void scriptToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_scriptDialog == null)
            {
                _scriptDialog = new DebugScripts();
                _scriptDialog.Show();
            }
            else _scriptDialog.Visible = true;
        }

        private void modeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Dictionary<BreakPointsModes, bool[]> modes = new Dictionary<BreakPointsModes, bool[]>() 
            {   
                { BreakPointsModes.Manual , new bool[]{true , false}},
                { BreakPointsModes.Script , new bool[]{false , true}}
            };
            List<ToolStripMenuItem> menus = new List<ToolStripMenuItem>() { manualModeToolStripMenuItem, scriptModeToolStripMenuItem };
            bool[] mode_value;
            if (modes.TryGetValue(_breakpoints.Mode, out mode_value))
            {
                foreach (var value in mode_value.Select((val2, idx2) => new { Index = idx2, Value = val2 }))
                {
                    if (menus[value.Index] != null)
                    {
                        menus[value.Index].Checked = value.Value;
                        menus[value.Index].Enabled = !value.Value;
                    }
                }
            }
        }

        private void manualModeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _breakpoints.Mode = BreakPointsModes.Manual;
        }

        private void scriptModeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _breakpoints.Mode = BreakPointsModes.Script;
        }

    }
}
