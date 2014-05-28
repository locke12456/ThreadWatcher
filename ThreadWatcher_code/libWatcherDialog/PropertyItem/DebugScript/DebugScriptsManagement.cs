using libWatherDebugger.Breakpoint;
using System;
using System.Collections.Generic;
using System.IO;
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
        public DebugScriptItem GetItem(DebugBreakpoint item)
        {
            return _findItemRule(item);
        }
        private DebugScriptsManagement()
        {
            _items = new List<DebugScriptItem>();
        }
        private bool _string_equal(DebugScriptItem child, object item)
        {
            string[] str = (item as string).Split(new char[] { ' ' });
            return child.Equals(str[0]);
        }
        private bool _object_equal(DebugScriptItem child, object item)
        {
            DebugBreakpoint dbp = item as DebugBreakpoint;
            FileInfo filename = new FileInfo(dbp.FileName);
            bool filename_eql = filename.Name == child.BreakpointInfo.filename.ToLower();
            bool line_eql = dbp.Document.Code.StartPosition + 1 == child.BreakpointInfo.line;
            return filename_eql && line_eql;
        }
        protected DebugScriptItem _findItemRule(object item)
        {
            DebugScriptItem find = base._findItemRule(item as DebugScriptItem);
            Dictionary<Type, Func<DebugScriptItem, object, bool>> mode = new Dictionary<Type, Func<DebugScriptItem, object, bool>>() 
            {
                { typeof(string) , _string_equal },
                { typeof(object) , _object_equal },
            };

            if (find != null) return find;
            foreach (DebugScriptItem child in _items)
            {
                Func<DebugScriptItem, object, bool> func;
                Type type = _get_type(item);
                if (mode.TryGetValue(type, out func))
                {
                    if (func(child, item)) return child;
                }
            }
            return null;
        }

        private static Type _get_type(object item)
        {
            Type type;
            type = item.GetType() != typeof(string) ? typeof(object) : item.GetType();
            return type;
        }
    }
}
