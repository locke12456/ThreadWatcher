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

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public partial class DebugScriptsMenu : PropertyDialogMenu
    {
        private DebugScriptsManagement _scripts = DebugScriptsManagement.getInstance();
        public DebugScriptsMenu() : base()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DialogResult result = debugScriptOpen.ShowDialog(this);
            if (DialogResult.OK == result) 
            {
                object info = DebugScriptEngine.DebugScriptEngine.getInstance().RunScript(debugScriptOpen.FileName);
                DebugScriptItemFactory factory = new DebugScriptItemFactory(info as DebugScriptEngine.Breakpoint.BreakpointRule);
                factory.CreateProduct();
                _scripts.AddItem(factory.Product);
            }
        }

        private void debugScriptEnabe_Click(object sender, System.EventArgs e)
        {
            DebugScriptItem item = _scripts.CurrentItem;
            if (item == null) return;
            item.Enable = true;
        }

        private void debugScriptDisable_Click(object sender, System.EventArgs e)
        {
            DebugScriptItem item = _scripts.CurrentItem;
            if (item == null) return;
            item.Enable = false;
        }

    }
}
