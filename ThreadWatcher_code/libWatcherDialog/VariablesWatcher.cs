using libUtilities;
using libWatcherDialog.PropertyItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog
{
    public partial class VariablesWatcher : PropertyForm<object, ItemProperties>
    {
        private System.Windows.Forms.ColumnHeader m_Size;

        public delegate void QueryHandler(object sender);
        public event QueryHandler AddEvent;
        private Dictionary<string, IDebuggerMemory> _watchpoints;
        public string QueryString { get; private set; }
        public string QueryType { get; private set; }
        public VariablesWatcher() : base() {
        }

        protected void InitializeComponent()
        {
            this.m_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer2
            // 
            // 
            // m_Size
            // 
            this.m_Size.Text = "Size";
            // 
            // VariablesWatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(374, 279);
            this.Name = "VariablesWatcher";
            this.Text = "Variables";
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
            this.ResumeLayout(false);

        }
        private void _addWatchpoint(string addr, Func<string, IDebuggerMemory> query)
        {
            IDebuggerMemory detail = query(addr);
            _watchpoints.Add(detail.Variable, detail);
            _addWatchpoint(detail);
        }
        private void _addWatchpoint(IDebuggerMemory detail)
        {
            string value = detail.Value;
            string variable = detail.Variable;
            string size = detail.Size;
            _watchpoints.Add(detail.Variable, detail);
            ListViewItem item = new ListViewItem(new string[] { value });
            ListViewItem.ListViewSubItem var = new ListViewItem.ListViewSubItem(item, variable);
            item.SubItems.Add(var);
            ListViewItem.ListViewSubItem Size = new ListViewItem.ListViewSubItem(item, size);
            item.SubItems.Add(Size);
        }

    }
}
