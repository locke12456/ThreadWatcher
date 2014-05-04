using libWatcherDialog.PropertyItem.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.Log
{
    public class LoggedEventArgs
    {
        public string ID { get; private set; }
        public LogItem Log { get; private set; }
        public LoggedEventArgs(string id, LogItem log)
        {
            ID = id; Log = log;
        }
    }
    public class LogManagement : ItemsManagement<LogItem>
    {
        public delegate void LoggedEventHandler(object sender, LoggedEventArgs log);
        public event LoggedEventHandler LoggedEvent;
        private static LogManagement _this;
        private Dictionary<string, List<LogItem>> _dictionary;

        public static LogManagement getInstance()
        {
            if (_this == null) _this = new LogManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this = null;
        }

        private LogManagement()
        {
            //
            _dictionary = new Dictionary<string, List<LogItem>>();
        }
        public override void AddItem(LogItem target)
        {
            List<LogItem> value;
            if (_dictionary.TryGetValue(target.Key, out value))
                _items = value;
            else
            {
                _items = new List<LogItem>();
                _dictionary.Add(target.Key, _items);
            }
            base.AddItem(target);
            if (LoggedEvent != null) LoggedEvent(this, new LoggedEventArgs(target.Key, target));
        }
        public List<LogItem> GetLog(Type type)
        {
            return _items.Where(item => item.GetType() == type).ToList();
        }
        public List<LogItem> GetLog(string Key)
        {
            List<LogItem> value;
            if (_dictionary.TryGetValue(Key, out value)) return value;
            return null;
        }
    }
}
