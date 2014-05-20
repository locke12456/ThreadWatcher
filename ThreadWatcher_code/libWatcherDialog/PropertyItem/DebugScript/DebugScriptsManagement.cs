using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public class DebugScriptsManagement : ItemsManagement<DebugScriptItem>
    {
        protected static DebugScriptsManagement _this;
        public static DebugScriptsManagement getInstance()
        {
            if (_this == null) _this = new DebugScriptsManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this = null;
        }
        public DebugScriptItem GetItem(object item)
        {
            return _findItemRule(item);
        }
        private DebugScriptsManagement()
        {
            _items = new List<DebugScriptItem>();
        }
        protected DebugScriptItem _findItemRule(object item)
        {
            DebugScriptItem find = null;
            find = base._findItemRule(item as DebugScriptItem);
            if (find != null) return find;
            return find;// _findItem(item);
        }
    }
}
