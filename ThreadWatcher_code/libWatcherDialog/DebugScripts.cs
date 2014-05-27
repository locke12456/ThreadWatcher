using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.DebugScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog
{
    public class DebugScripts : DebugScriptsRef
    {
        public DebugScripts()
            : base()
        {
            InitializeComponent();
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
            // splitContainer2
            // 
            this.splitContainer2.SplitterDistance = 153;
            // 
            // PropertyView
            // 
            this.PropertyView.Size = new System.Drawing.Size(228, 153);
            // 
            // Details
            // 
            this.Details.Size = new System.Drawing.Size(228, 144);
            // 
            // DebugScripts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(374, 302);
            this.Name = "DebugScripts";
            this.Text = "DebugScripts";
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
    public class DebugScriptsRef : PropertyForm<DebugScriptItem, DebugScriptProperty>
    {
        protected DebugScriptsMenu _property_menu;
        protected DebugScriptsManagement _scripts = DebugScriptsManagement.getInstance();
        public bool CanDistory { get; set; }
        public DebugScriptsRef()
            : base()
        {
            _management = _scripts;
            _init_status();
            _init_menus();
        }

        private void _init_status()
        {
            CanDistory = false;
            _scripts.PropertyAdded += _scripts_PropertyAdded;
            _scripts.PropertyChanged += _scripts_PropertyChanged;
            _scripts.PropertyRemoved += _scripts_PropertyRemoved;
            Disposed += DebugScriptsRef_Disposed;
        }

        protected void _scripts_PropertyRemoved(object sender, PropertyItem.EventArgs.PropertiesEventArgs<DebugScriptItem> e)
        {
            RemoveProprty(e.Item);
        }
        protected void _scripts_PropertyChanged(object sender, PropertyItem.EventArgs.PropertiesEventArgs<DebugScriptItem> e)
        {
            SetCurrentProperty(e.Item);
        }

        protected void _scripts_PropertyAdded(object sender, PropertyItem.EventArgs.PropertiesEventArgs<DebugScriptItem> e)
        {
            AddProprty(e.Item);
        }

        protected void DebugScriptsRef_Disposed(object sender, EventArgs e)
        {
            DebugScriptsManagement.Destroy();
        }

        private void _init_menus()
        {
            _property_menu = new DebugScriptsMenu();
            Properties.ContextMenuStrip = _property_menu.ListMenu;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!CanDistory)
            {
                Hide();
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }
    }
}
