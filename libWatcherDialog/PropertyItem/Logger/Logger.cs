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
        private CrossThreadProtected gui;
        private bool _close = false;
        public List<T> Logs 
        {
            get {
                return Log.Items.Cast<T>().ToList();
            }
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
        }
        public void AppendLog(T msg)
        {
            gui.UpdateGUI_ByCallMethod(Modify.Add, msg, Log.Items);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!_close) {
                Hide();
                e.Cancel = true; 
            }
        }
    }
}
