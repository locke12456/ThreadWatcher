using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog
{
    public partial class Threads : ThreadsRef
    {
        private System.ComponentModel.IContainer components;
        private ThreadsManagement _threads = ThreadsManagement.getInstance() as ThreadsManagement;
        private ThreadPropertiesMenu _menus;
        public Threads()
            : base()
        {
            InitializeComponent();
            Disposed += Threads_Disposed;
            _initContextMenu();
        }

        private void Threads_Disposed(object sender, EventArgs e)
        {
            foreach (ThreadItem item in Properties.Items)
                item.CloseLogger();
            ThreadsManagement.Destroy();
        }
        protected override void _initContextMenu()
        {
            _menus = new ThreadPropertiesMenu();
            Properties.ContextMenuStrip = _menus.ListMenu;
        }
        public void AddThread(DebugThread thread)
        {
            thread.Name = "Thread " + (Properties.Items.Count + 1);
            ThreadItem item = new ThreadItem();
            item.Thread = thread;
            _threads.AddItem(item);
            AddProprty(item);
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

        private void Threads_Load(object sender, EventArgs e)
        {

        }

        private void Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            _menus.PropertiesChanged(Properties);
        }
    }
    public class ThreadsRef : PropertyForm<ThreadItem,ItemProperties>
    {
        public ThreadsRef() : base() { }
    }
}
