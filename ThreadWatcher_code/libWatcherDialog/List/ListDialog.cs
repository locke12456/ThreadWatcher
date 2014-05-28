using Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.List
{
    public partial class ListDialog<T> : Form
    {
        public delegate void ModifyEventHandler(object sender, object SelectedItem);
        public event ModifyEventHandler ModifiedEvent;
        protected CrossThreadProtected gui;
        public ListDialog()
        {
            gui = new CrossThreadProtected(this);
            InitializeComponent();
        }
        protected void AddItem(T item)
        {
            gui.UpdateGUI_ByCallMethod(Dialog.Modify.Add, item, Items.Items);
        }
        protected void AddItems(List<T> items)
        {
            gui.UpdateGUI_ByCallMethod("AddRange", items.ToArray(), Items.Items);
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            if (ModifiedEvent != null)
            {
                if (Items.SelectedItem == null)
                {
                    MessageBox.Show(this, "Please Select item ");
                    return;
                }
                ModifiedEvent(this, Items.SelectedItem);
            }
        }
    }
}
