using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem.Thread
{
    [ToolboxItem(true)]
    public partial class ThreadPropertiesMenu :  PropertyDialogMenu
    {
        public ThreadPropertiesMenu() 
        {
            InitializeComponent();
            _init();
        }

        private void _init()
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>(){
                logToolStripMenuItem,
                breakToolStripMenuItem,
                continueToolStripMenuItem,
                stepToolStripMenuItem};
            _propertiesMenu = new ThreadsPropertiesMenuStatus(items);
            _propertiesMenu.Register(this);
        }
        public void PropertiesChanged(ListBox properties) 
        {
            Properties = properties;
            StatusChange(Properties.SelectedItem != null ? MenuState.ItemSelected : MenuState.Normal);
        }
        public void StatusChange(MenuState state) 
        {
            _propertiesMenu.State = state;
        }
        private void ItemMenu_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void ListMenu_Opening(object sender, CancelEventArgs e)
        {
            _propertiesMenu.Change();
        }

        private void logToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ThreadItem item = Properties.SelectedItem as ThreadItem;
            item.ShowLogger();
        }
    }
}
