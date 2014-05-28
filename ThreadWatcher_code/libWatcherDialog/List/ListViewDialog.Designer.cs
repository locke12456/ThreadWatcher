namespace libWatcherDialog.List
{
    partial class ListViewDialog<T>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.PropertyView = new ComponentOwl.BetterListView.BetterListView();
            this.m_Name = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.m_Value = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.Modify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).BeginInit();
            this.SuspendLayout();
            // 
            // PropertyView
            // 
            this.PropertyView.Columns.AddRange(new object[] {
            this.m_Name,
            this.m_Value});
            this.PropertyView.Dock = System.Windows.Forms.DockStyle.Top;
            this.PropertyView.LabelEditActivation = ((ComponentOwl.BetterListView.BetterListViewLabelEditActivation)((ComponentOwl.BetterListView.BetterListViewLabelEditActivation.Keyboard | ComponentOwl.BetterListView.BetterListViewLabelEditActivation.SingleClick)));
            this.PropertyView.LabelEditDefaultAccept = false;
            this.PropertyView.LabelEditModeItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.PropertyView.LabelEditModeSubItems = ComponentOwl.BetterListView.BetterListViewLabelEditMode.CustomControl;
            this.PropertyView.Location = new System.Drawing.Point(0, 0);
            this.PropertyView.MultiSelect = false;
            this.PropertyView.Name = "PropertyView";
            this.PropertyView.Size = new System.Drawing.Size(227, 244);
            this.PropertyView.TabIndex = 1;
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
            // Modify
            // 
            this.Modify.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Modify.Location = new System.Drawing.Point(0, 246);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(227, 23);
            this.Modify.TabIndex = 2;
            this.Modify.Text = "modify";
            this.Modify.UseVisualStyleBackColor = true;
            // 
            // ListViewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 269);
            this.Controls.Add(this.Modify);
            this.Controls.Add(this.PropertyView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ListViewDialog";
            this.Text = "ListViewDialog";
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected ComponentOwl.BetterListView.BetterListView PropertyView;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader m_Name;
        protected ComponentOwl.BetterListView.BetterListViewColumnHeader m_Value;
        protected System.Windows.Forms.Button Modify;
    }
}