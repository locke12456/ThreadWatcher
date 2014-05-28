namespace libWatcherDialog
{
    partial class PropertyForm<T,T2>
    {

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected virtual void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Properties = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Filter = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.PropertyView = new ComponentOwl.BetterListView.BetterListView();
            this.m_Name = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.m_Value = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.PropertyViewItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Details = new ComponentOwl.BetterListView.BetterListView();
            this.betterListViewColumnHeader1 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.betterListViewColumnHeader2 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.DetailMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertyViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.PropertyViewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MaximumSize = new System.Drawing.Size(374, 301);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(374, 301);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Properties);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(374, 301);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.TabIndex = 1;
            // 
            // Properties
            // 
            this.Properties.FormattingEnabled = true;
            this.Properties.Location = new System.Drawing.Point(6, 57);
            this.Properties.Name = "Properties";
            this.Properties.Size = new System.Drawing.Size(130, 238);
            this.Properties.TabIndex = 1;
            this.Properties.SelectedValueChanged += new System.EventHandler(this.Properties_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Filter);
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Filter
            // 
            this.Filter.Location = new System.Drawing.Point(5, 13);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(119, 20);
            this.Filter.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.PropertyView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Details);
            this.splitContainer2.Size = new System.Drawing.Size(228, 301);
            this.splitContainer2.SplitterDistance = 166;
            this.splitContainer2.TabIndex = 1;
            // 
            // PropertyView
            // 
            this.PropertyView.Columns.AddRange(new object[] {
            this.m_Name,
            this.m_Value});
            this.PropertyView.ContextMenuStrip = this.PropertyViewItemMenu;
            this.PropertyView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyView.LabelEditActivation = ((ComponentOwl.BetterListView.BetterListViewLabelEditActivation)((ComponentOwl.BetterListView.BetterListViewLabelEditActivation.Keyboard | ComponentOwl.BetterListView.BetterListViewLabelEditActivation.SingleClick)));
            this.PropertyView.LabelEditDefaultAccept = false;
            this.PropertyView.LabelEditModeItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.PropertyView.LabelEditModeSubItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.PropertyView.Location = new System.Drawing.Point(0, 0);
            this.PropertyView.MultiSelect = false;
            this.PropertyView.Name = "PropertyView";
            this.PropertyView.Size = new System.Drawing.Size(228, 166);
            this.PropertyView.TabIndex = 0;
            this.PropertyView.SelectedIndexChanged += new System.EventHandler(this.PropertyView_SelectedIndexChanged);
            // 
            // m_Name
            // 
            this.m_Name.AlignHorizontal = ComponentOwl.BetterListView.TextAlignmentHorizontal.Center;
            this.m_Name.AlignHorizontalImage = ComponentOwl.BetterListView.BetterListViewImageAlignmentHorizontal.BeforeTextCenter;
            this.m_Name.MaximumWidth = 128;
            this.m_Name.MinimumWidth = 84;
            this.m_Name.Name = "m_Name";
            this.m_Name.Style = ComponentOwl.BetterListView.BetterListViewColumnHeaderStyle.Nonclickable;
            this.m_Name.Text = "Name";
            this.m_Name.Width = 84;
            // 
            // m_Value
            // 
            this.m_Value.MaximumWidth = 160;
            this.m_Value.Name = "m_Value";
            this.m_Value.Text = "Value";
            this.m_Value.Width = 138;
            // 
            // PropertyViewItemMenu
            // 
            this.PropertyViewItemMenu.Name = "PropertyViewMenu";
            this.PropertyViewItemMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // Details
            // 
            this.Details.Columns.AddRange(new object[] {
            this.betterListViewColumnHeader1,
            this.betterListViewColumnHeader2});
            this.Details.ContextMenuStrip = this.DetailMenu;
            this.Details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Details.LabelEditActivation = ((ComponentOwl.BetterListView.BetterListViewLabelEditActivation)((ComponentOwl.BetterListView.BetterListViewLabelEditActivation.Keyboard | ComponentOwl.BetterListView.BetterListViewLabelEditActivation.SingleClick)));
            this.Details.LabelEditDefaultAccept = false;
            this.Details.LabelEditModeItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.Details.LabelEditModeSubItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.Details.Location = new System.Drawing.Point(0, 0);
            this.Details.MultiSelect = false;
            this.Details.Name = "Details";
            this.Details.Size = new System.Drawing.Size(228, 131);
            this.Details.TabIndex = 1;
            // 
            // betterListViewColumnHeader1
            // 
            this.betterListViewColumnHeader1.AlignHorizontal = ComponentOwl.BetterListView.TextAlignmentHorizontal.Center;
            this.betterListViewColumnHeader1.AlignHorizontalImage = ComponentOwl.BetterListView.BetterListViewImageAlignmentHorizontal.BeforeTextCenter;
            this.betterListViewColumnHeader1.MaximumWidth = 128;
            this.betterListViewColumnHeader1.MinimumWidth = 84;
            this.betterListViewColumnHeader1.Name = "m_Name";
            this.betterListViewColumnHeader1.Style = ComponentOwl.BetterListView.BetterListViewColumnHeaderStyle.Nonclickable;
            this.betterListViewColumnHeader1.Text = "Name";
            this.betterListViewColumnHeader1.Width = 84;
            // 
            // betterListViewColumnHeader2
            // 
            this.betterListViewColumnHeader2.MaximumWidth = 160;
            this.betterListViewColumnHeader2.Name = "m_Value";
            this.betterListViewColumnHeader2.Text = "Value";
            this.betterListViewColumnHeader2.Width = 139;
            // 
            // DetailMenu
            // 
            this.DetailMenu.Name = "PropertyViewMenu";
            this.DetailMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // PropertyViewMenu
            // 
            this.PropertyViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1});
            this.PropertyViewMenu.Name = "PropertyViewMenu";
            this.PropertyViewMenu.Size = new System.Drawing.Size(90, 26);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(89, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            // 
            // PropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 302);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PropertyForm";
            this.Text = "PropertyForm";
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
            this.PropertyViewMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.ListBox Properties;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox Filter;
        protected System.Windows.Forms.SplitContainer splitContainer2;
        protected ComponentOwl.BetterListView.BetterListView PropertyView;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader m_Name;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader m_Value;
        protected ComponentOwl.BetterListView.BetterListView Details;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader1;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader2;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        public System.Windows.Forms.ContextMenuStrip DetailMenu;
        public System.Windows.Forms.ContextMenuStrip PropertyViewMenu;
        public System.Windows.Forms.ContextMenuStrip PropertyViewItemMenu;
    }
}