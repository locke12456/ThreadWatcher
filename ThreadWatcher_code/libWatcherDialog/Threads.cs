using libWatcherDialog.CombineRules;
using libWatcherDialog.GeneralRules.Mode.Thread;
using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog
{
    public partial class Threads : ThreadsRef
    {
        private static AutoResetEvent sync; 
        private ThreadsManagement _threads = ThreadsManagement.getInstance() as ThreadsManagement;
        private CreateThreadItem _create_thread;
        private ThreadPropertiesMenu _menus;
        private ScriptQueue<CreateThreadItem> _queue;
        public Threads()
            : base()
        {
            _management = _threads;
            InitializeComponent();
            _register_events();
            _initContextMenu();
            _initQueue();
        }

        private void _initQueue()
        {
            _queue = new ScriptQueue<CreateThreadItem>();
        }

        private void _register_events()
        {
            Disposed += Threads_Disposed;
            _threads.PropertyAdded += _threads_PropertyAdded;
            _threads.PropertyRemoved += _threads_PropertyRemoved;
        }

        private void _threads_PropertyAdded(object sender, PropertyItem.EventArgs.PropertiesEventArgs<ThreadItem> e)
        {
            AddProprty(e.Item);
        }
        private void _threads_PropertyRemoved(object sender, PropertyItem.EventArgs.PropertiesEventArgs<ThreadItem> e)
        {
            RemoveProprty(e.Item);
        }
        private void Threads_Disposed(object sender, EventArgs e)
        {
            foreach (ThreadItem item in Properties.Items)
                item.CloseLogger();
            ThreadsManagement.Destroy();
            LogManagement.Destroy();
        }
        protected override void _initContextMenu()
        {
            _menus = new ThreadPropertiesMenu();
            Properties.ContextMenuStrip = _menus.ListMenu;
        }
        /*
         *   Create thread item ,excute on other thread 
         */
        public void AddThread(DebugThread thread)
        {
            _wait();
            _create_thread = new CreateThreadItem();
            _create_thread.Thread = thread;
            _add_script_to_quque(_create_thread);
            _finish();
        }

        private void _add_script_to_quque(CreateThreadItem script)
        {
            script.Name = "Thread";
            _queue.AddRule(script);
            _queue.RunRule();
        }


        private static void _finish()
        {
            sync.Set();
        }

        private static void _wait()
        {
            if (sync == null) sync = new AutoResetEvent(false);
            else sync.WaitOne();
        }
        public void RemoveThread(object thread)
        {
            ThreadItem item = _threads.GetItem(thread) as ThreadItem;
            if (item != null)
            {
                _threads.RemoveItem(item);
                Properties.Items.Remove(item);
            }
        }

        private void Threads_Shown(object sender, EventArgs e)
        {
            //foreach (ThreadItem item in Properties.Items)
            //    item.ShowLogger();
        }

        private void Threads_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

        }
        private void Threads_Load(object sender, EventArgs e)
        {

        }

        private void Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            _menus.PropertiesChanged(Properties);
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Details)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // Properties
            // 
            this.Properties.SelectedIndexChanged += new System.EventHandler(this.Properties_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            // 
            // Threads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(374, 302);
            this.Name = "Threads";
            this.Text = "Threads";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Threads_FormClosed);
            this.Load += new System.EventHandler(this.Threads_Load);
            this.Shown += new System.EventHandler(this.Threads_Shown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Details)).EndInit();
            this.ResumeLayout(false);

        }

    }
    public class ThreadsRef : PropertyForm<ThreadItem, ItemProperties>
    {
        public ThreadsRef() : base() { }
    }
}
