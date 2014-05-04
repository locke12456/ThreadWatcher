using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem
{
    public class PropertyDialogMenu : UserControl
    {
        protected System.Windows.Forms.ListBox Properties;
        protected System.Windows.Forms.TextBox Filter;
        protected ComponentOwl.BetterListView.BetterListView PropertyView;
        protected ComponentOwl.BetterListView.BetterListView Details;
        protected MenuStatus _propertiesMenu;
        protected MenuStatus _propertiesViewMenu;
        protected MenuStatus _detailMenu;
        public void PropertiesChanged(ListBox properties)
        {
            Properties = properties;
            StatusChange(Properties.SelectedItem != null ? MenuState.ItemSelected : MenuState.Normal);
        }
        public void PropertyViewChanged(ComponentOwl.BetterListView.BetterListView properties)
        {
            PropertyView = properties;
            StatusChange(PropertyView.SelectedValue != null ? MenuState.ItemSelected : MenuState.Normal);
        }
        public void DetailsChanged(ComponentOwl.BetterListView.BetterListView properties)
        {
            Details = properties;
            StatusChange(Details.SelectedValue != null ? MenuState.ItemSelected : MenuState.Normal);
        }
        public void StatusChange(MenuState state)
        {
            _propertiesMenu.State = state;
            _propertiesViewMenu.State = state;
            _detailMenu.State = state;
        }
    }
}
