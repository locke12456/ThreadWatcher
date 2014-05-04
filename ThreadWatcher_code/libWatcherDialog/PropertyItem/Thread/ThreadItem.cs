
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.DocumentContext;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.PropertyItem.Thread
{
    public class ThreadItem : PropertyItem<Threads, ItemProperties>
    {

        public DebugThread Thread { get; set; }
        private CodeInformation _code;
        private List<LogItem> _logs;
        public List<LogItem> Logs
        {
            get { return LogManagement.getInstance().GetLog(Thread.ID); }
        }
        private ThreadLogger _log = null;
        public ThreadItem()
        {
            //_log.LogAddedEvent += _log_LogAddedEvent;
            _logs = new List<LogItem>();
        }
        private void _log_LogAddedEvent(object sender, ThreadLog item)
        {
            BreakpointItem target = BreakpointsManagement.getInstance().GetItem(item.Name);
            target.HitLocations.BreakpointHit(Thread);
        }

        public void ShowLogger()
        {
            _remove_break_log_event();
            _log = new ThreadLogger();
            _log.Text = Thread.ToString();
            List<LogItem> log = new List<LogItem>();
            _init_log_data(log);
            _add_break_log_event();
            _log.Show();
        }

        private void _init_log_data(List<LogItem> log)
        {
            log.AddRange(Logs);
            log.AddRange(_logs);
            log.Sort(_compare);
            _log.AddLogs(log);
        }

        private void _add_break_log_event()
        {
            LogManagement target = LogManagement.getInstance();
            target.LoggedEvent += LogManagement_LoggedEvent;
        }

        private void LogManagement_LoggedEvent(object sender, LoggedEventArgs log)
        {
            if (log.ID != Thread.ID) return;
            _log.AppendLog(log.Log);
        }
        private void _remove_break_log_event()
        {
            if (_log == null) return;
            LogManagement target = LogManagement.getInstance();
            target.LoggedEvent -= LogManagement_LoggedEvent;
        }
        private int _compare(LogItem x, LogItem y)
        {
            return (int)(x.CreatedTimeTick - y.CreatedTimeTick);// ? (x.CreatedTimeTick == y.CreatedTimeTick)?-1:0  : 1;
        }
        public void CloseLogger()
        {
            if (_log == null) return;
            _log.DestroyLogger();
            _log.Close();
        }
        public void WriteLog(LogItem msg)
        {
            _log.AppendLog(msg);
        }
        public void WriteLog(string msg)
        {
            ThreadLog log = new ThreadLog();
            log.Name = msg;
            Logs.Add(log);
            if (_log != null)
                _log.AppendLog(log);
        }
        public override string ToString()
        {
            return Thread.ToString();
        }
    }
}
