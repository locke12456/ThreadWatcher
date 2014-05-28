using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.DebugScript;
using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class BreakpointsManagement : ItemsManagement<BreakpointItem>
    {
        protected static BreakpointsManagement _this;
        public DebugScriptItem ConcernedTarget
        {
            get;
            set;
        }
        private MemoryData _memoryDatas;

        public MemoryData Datas {
            get { return _memoryDatas; }
        }
        public int WatchpointCount
        {
            get;
            private set;
        }
        public static BreakpointsManagement getInstance()
        {
            if (_this == null) _this = new BreakpointsManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this = null;
        }
        public BreakpointItem GetItem(object item) 
        {
            return _findItemRule(item);
        }
        public void AddMemoryData(DataBreakpointListItem data) 
        {
            _memoryDatas.Add(data);
            //WatchpointCount++;
        }
        public void RemoveMemoryData(DataBreakpointListItem data)
        {
            data.MemoryAddressInfo.InWatchList = false;
            _memoryDatas.Remove(data);
            BreakpointItem item = _findItemRule(data.MemoryAddressInfo.Variable);
            RemoveItem(item);
        }
        private BreakpointsManagement()
        {
            _memoryDatas = new MemoryData();
            _items = new List<BreakpointItem>();
        }

        private bool _string_equal(BreakpointItem child, object item)
        {
            string[] str = (item as string).Split(new char[] { ' ' });
            return child.Equals(str[0]);
        }
        private bool _object_equal(BreakpointItem child, object item)
        {
            return child.Breakpoint == item;
        }
        //protected override BreakpointItem _findItemRule(BreakpointItem item)
        //{
        //    return _items.IndexOf(item) == -1 ? null : item;
        //}
        protected BreakpointItem _findItemRule(object item)
        {

            BreakpointItem find = base._findItemRule(item as BreakpointItem);
            Dictionary<Type, Func<BreakpointItem, object, bool>> mode = new Dictionary<Type, Func<BreakpointItem, object, bool>>() 
            {
                { typeof(string) , _string_equal },
                { typeof(object) , _object_equal },
            };
            if (find != null) return find;
            foreach (BreakpointItem child in _items)
            {
                Func<BreakpointItem, object, bool> func;
                if (mode.TryGetValue(item.GetType(), out func))
                {
                    if (func(child, item)) return child;
                }
            }
            return null;
        }
    }
}
