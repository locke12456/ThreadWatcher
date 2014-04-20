
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.DocumentContext;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
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
        public List<ThreadLog> Logs
        {
            get
            {
                return _log.Logs;
            }
        }
        private ThreadLogger _log = null;
        public ThreadItem()
        {
            _log = new ThreadLogger();
            _log.LogAddedEvent += _log_LogAddedEvent;
        }

        private void _log_LogAddedEvent(object sender, ThreadLog item)
        {
            BreakpointItem target = BreakpointsManagement.getInstance().GetItem(item.Name);
            target.HitLocations.BreakpointHit(this);
        }

        public void ShowLogger()
        {
            _log.Text = Thread.ToString();
            _log.Show();
        }
        public void CloseLogger()
        {
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
            if (_log != null)
                _log.AppendLog(log);
        }
        public override string ToString()
        {
            return Thread.ToString();
        }
    }
}
