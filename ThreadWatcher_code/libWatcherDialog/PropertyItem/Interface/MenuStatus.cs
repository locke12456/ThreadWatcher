using Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem
{
    public enum MenuState
    {
        ItemSelected, Normal,
    }
    public abstract class MenuStatus : IMenuStatus
    {
        protected ContextMenuStrip _menu;
        protected List<ToolStripMenuItem> _items;
        protected Dictionary<MenuState, Func<List<bool>> > _status;
        protected CrossThreadProtected gui;
        public MenuState State { get; set; }
        public void Register(ContainerControl target)
        {
            gui = new CrossThreadProtected(target);
        }
        public virtual void Change()
        {
            Func<List<bool>> mode;
            if (_status.TryGetValue(State, out mode)) 
            {
                List<bool> state = mode();
                foreach (var item in _items.Select((val2, idx2) => new { Index = idx2, Value = val2 }))
                {
                    gui.UpdateGUI_BySetValue(Modify.Enabled, state[item.Index], item.Value); 
                }
            }
        }
    }
}
