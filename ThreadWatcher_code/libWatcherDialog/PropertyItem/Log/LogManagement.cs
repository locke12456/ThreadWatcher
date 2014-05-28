using libUtilities;
using libWatcherDialog.PropertyItem.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private JSONObject _dictionary;

        public static LogManagement getInstance()
        {
            if (_this == null) _this = new LogManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this._save_logs();
            _this = null;
        }

        private LogManagement()
        {
            //
            _dictionary = new JSONObject();
        }
        private void _save_logs()
        {
            string path;
            _create_log_directory(out path);
            foreach (var log in _dictionary)
            {
                JSONObject json_obj = new JSONObject();
                List<LogItem> logs = log.Value as List<LogItem>;
                foreach (LogItem val in logs)
                {
                    json_obj.Add(val.CreatedTimeTick.ToString(), val);
                }
                try
                {
                    string json = json_obj.ToJSONString();
                    File.WriteAllText(path + "\\" + log.Key + ".log", json);
                }
                catch (Exception Fail)
                {
                    Debug.WriteLine(Fail.Message);
                }

            }
        }

        private static string _create_log_directory(out string path)
        {

            FileInfo sol = new FileInfo(Watcher.Debugger.Debugger.getInstance().VSDebugger.DTE.Solution.FullName);
            path = sol.DirectoryName + "\\log\\";
            _check_directory_exist(path);
            path += System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            _check_directory_exist(path);
            return path;
        }

        private static void _check_directory_exist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public override void AddItem(LogItem target)
        {
            Object value;
            if (_dictionary.TryGetValue(target.Key, out value))
                _items = value as List<LogItem>;
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
            Object value;
            if (_dictionary.TryGetValue(Key, out value)) return value as List<LogItem>;
            return null;
        }
    }
}
