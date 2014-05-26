using Dialog;
using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.BreakPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog
{
    public partial class PropertyForm<T, T2> : Form where T2 : ItemProperties  where T : class
    {
        protected CrossThreadProtected _gui;
        protected ItemsManagement<T> _management;
        protected MenuStatus _propertiesMenu;
        protected MenuStatus _propertiesViewMenu;
        protected MenuStatus _detialMenu;
        public PropertyForm()
        {
            _gui = new CrossThreadProtected(this);
            InitializeComponent();

        }

        protected virtual void _initContextMenu()
        {

        }
        protected virtual void SetCurrentProperty(T item)
        {
            //Properties.BeginUpdate();
            int index = _itemsEquals(item);
            if (index >= 0)
            {
                //_gui.UpdateGUI_BySetValue("SelectedIndex", null, Properties);
                _gui.UpdateGUI_BySetValue("SelectedIndex", -1, Properties);
                _gui.UpdateGUI_BySetValue("SelectedIndex", index, Properties);
            }
            //Properties.EndUpdate();
        }
        protected virtual void AddProprty(T item)
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Add, item, Properties.Items);
        }
        protected virtual void ClearProprty()
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Clear, Properties.Items);
        }
        protected virtual void RemoveProprty(T item)
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Remove, item, Properties.Items);
        }
        protected virtual void RemoveProprtyByName(string name)
        {
            foreach (T item in Properties.Items)
            {
                if (item.ToString() == name)
                {
                    RemoveProprty(item);
                    return;
                }
            }
        }
        protected virtual int _itemsEquals(T item)
        {
            foreach (T obj in Properties.Items)
                if (obj.ToString() == item.ToString()) return Properties.Items.IndexOf(obj);
            return -1;
        }

        protected virtual void AddProprtyViewItems(List<T2> items)
        {
            foreach (T2 item in items)
            {
                item.List = PropertyView;
                item.Parent = this;
                _gui.UpdateGUI_ByCallMethod(Modify.Add, item, PropertyView.Items);

            }
        }

        protected virtual void ClearProprtyView()
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Clear, PropertyView.Items);
        }
        public virtual void AddDetialItems(List<T2> items)
        {
            foreach (T2 item in items)
            {
                _gui.UpdateGUI_ByCallMethod(Modify.Add, item, Details.Items);
            }
        }
        public virtual void ClearDetails()
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Clear, Details.Items);
        }
        protected virtual void AddProprtyView(T2 item)
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Add, item, PropertyView.Items);
        }
        public virtual void AddDetialItem(T2 item)
        {
            _gui.UpdateGUI_ByCallMethod(Modify.Add, item, Details.Items);
        }
        protected virtual T2 AddProprtyView(string name, string value)
        {
            T2 item = _createItem(name, value);
            _gui.UpdateGUI_ByCallMethod(Modify.Add, item, PropertyView.Items);
            return item;
        }
        protected virtual T2 AddDetialItem(string name, string value)
        {
            T2 item = _createItem(name, value);
            _gui.UpdateGUI_ByCallMethod(Modify.Add, item, Details.Items);
            Details.RequestEmbeddedControl += item.ListViewRequestEmbeddedControl;
            return item;
        }
        protected virtual T2 _createItem(string name, string value)
        {
            T2 item = null;
            return item;
        }
        protected void _clear_PropertyView_events()
        {
            foreach (T2 item in PropertyView.Items)
            {
                PropertyView.RequestEmbeddedControl -= item.ListViewRequestEmbeddedControl;
                PropertyView.AfterLabelEdit -= item.AfterLabelEdit;

            }
        }
        protected virtual void Properties_SelectedValueChanged(object sender, EventArgs e)
        {
            _management.CurrentItem = Properties.SelectedItem as T;
        }

        private void PropertyView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _clear_PropertyView_events();
            if (PropertyView.SelectedItems.Count == 0) return;
            PropertyView.RequestEmbeddedControl += (PropertyView.SelectedItems[0] as T2).ListViewRequestEmbeddedControl;
            PropertyView.AfterLabelEdit += (PropertyView.SelectedItems[0] as T2).AfterLabelEdit;
        }

    }

}
