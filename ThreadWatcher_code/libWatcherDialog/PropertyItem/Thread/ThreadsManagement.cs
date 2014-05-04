using System;
using System.Collections.Generic;
using System.Threading;

namespace libWatcherDialog.PropertyItem.Thread
{
    public class ThreadsManagement : ItemsManagement<ThreadItem>
    {
        protected static ThreadsManagement _this;
        public static ThreadsManagement getInstance()
        {
            if (_this == null) _this = new ThreadsManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this = null;
        }
        public ThreadItem GetItem(object item)
        {
            return _findItemRule(item);
        }
        private ThreadsManagement()
        {
            _items = new List<ThreadItem>();
        }
        private bool _string_equal(ThreadItem current, object item)
        {
            return current.Thread.ID.Equals(item as string);
        }
        private bool _object_equal(ThreadItem current, object item)
        {
            return current.Thread.Equals(item);
        }
        protected ThreadItem _findItemRule(object item)
        {
            ThreadItem find = null;
            find = base._findItemRule(item as ThreadItem);
            if (find != null) return find;
            return _findItem(item);
        }

        private ThreadItem _findItem(object item)
        {
            Dictionary<Type, Func<ThreadItem, object, bool>> mode = _initCompareMode();
            return _compare(mode, item);
        }

        private ThreadItem _compare(Dictionary<Type, Func<ThreadItem, object, bool>> mode, object item)
        {
            foreach (ThreadItem child in _items)
            {
                ThreadItem equel = _compare_equals(item, mode, child);
                if (equel!= null) return equel;
            }
            return null;
        }

        private ThreadItem _compare_equals(object item, Dictionary<Type, Func<ThreadItem, object, bool>> mode, ThreadItem current)
        {
            Func<ThreadItem, object, bool> func;
            if (mode.TryGetValue(item.GetType(), out func))
            {
                if (func(current, item)) return current;
            }
            return _childIsIDebugThread(current, item);
        }

        private ThreadItem _childIsIDebugThread(ThreadItem current, object item)
        {
            if (_object_equal(current, item)) return current;
            return null;
        }

        private Dictionary<Type, Func<ThreadItem, object, bool>> _initCompareMode()
        {
            Dictionary<Type, Func<ThreadItem, object, bool>> mode = new Dictionary<Type, Func<ThreadItem, object, bool>>() 
            {
                { typeof(string) , _string_equal },
                { typeof(object) , _object_equal },
            };
            return mode;
        }
    }
}
