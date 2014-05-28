using Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem.Logger
{
    public partial class Logger<T> : Form
    {
        public delegate void LoggedEventHandler(object sender, T item);
        public event LoggedEventHandler LogAddedEvent;
        private CrossThreadProtected gui;
        private bool _close = false;
        public List<T> Logs
        {
            get
            {
                return Log.Items.Cast<T>().ToList();
            }
        }
        public void AddLogs(List<LogItem> logs)
        {

            Log.Items.Clear();
            Log.Items.AddRange(logs.Cast<object>().ToArray());
        }
        public Logger()
        {
            gui = new CrossThreadProtected(this);
            InitializeComponent();
        }
        public void HideLogger()
        {
            _close = false;
        }
        public void DestroyLogger()
        {
            _close = true;
        }
        public void AppendLog(LogItem msg)
        {
            gui.UpdateGUI_ByCallMethod(Modify.Add, msg, Log.Items);
            gui.UpdateGUI_BySetValue("TopIndex", Log.Items.Count - 1, Log);
        }
        public void AppendLog(T msg)
        {
            gui.UpdateGUI_ByCallMethod(Modify.Add, msg, Log.Items);
            gui.UpdateGUI_BySetValue("TopIndex", Log.Items.Count - 1, Log);
            _log_added_trigger(msg);
        }

        private void _log_added_trigger(T msg)
        {
            if (LogAddedEvent != null) LogAddedEvent(this, msg);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}
