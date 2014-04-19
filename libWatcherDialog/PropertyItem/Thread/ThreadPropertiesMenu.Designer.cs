namespace libWatcherDialog.PropertyItem.Thread
{
    partial class ThreadPropertiesMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.breakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.nextStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delteteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListMenu.SuspendLayout();
            this.ItemMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListMenu
            // 
            this.ListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem,
            this.toolStripSeparator1,
            this.breakToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.toolStripSeparator2,
            this.stepToolStripMenuItem});
            this.ListMenu.Name = "PropertyViewMenu";
            this.ListMenu.Size = new System.Drawing.Size(153, 126);
            this.ListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ListMenu_Opening);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // breakToolStripMenuItem
            // 
            this.breakToolStripMenuItem.Name = "breakToolStripMenuItem";
            this.breakToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.breakToolStripMenuItem.Text = "Break";
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.continueToolStripMenuItem.Text = "Continue";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(110, 6);
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepIntoToolStripMenuItem,
            this.stepOutToolStripMenuItem,
            this.toolStripSeparator5,
            this.nextStepToolStripMenuItem});
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.stepToolStripMenuItem.Text = "Step";
            // 
            // stepIntoToolStripMenuItem
            // 
            this.stepIntoToolStripMenuItem.Name = "stepIntoToolStripMenuItem";
            this.stepIntoToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.stepIntoToolStripMenuItem.Text = "Step into";
            // 
            // stepOutToolStripMenuItem
            // 
            this.stepOutToolStripMenuItem.Name = "stepOutToolStripMenuItem";
            this.stepOutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.stepOutToolStripMenuItem.Text = "Step out";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(112, 6);
            // 
            // nextStepToolStripMenuItem
            // 
            this.nextStepToolStripMenuItem.Name = "nextStepToolStripMenuItem";
            this.nextStepToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.nextStepToolStripMenuItem.Text = "Next Step";
            // 
            // ItemMenu
            // 
            this.ItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.editToolStripMenuItem,
            this.delteteToolStripMenuItem,
            this.toolStripSeparator4,
            this.loadFormToolStripMenuItem,
            this.saveToToolStripMenuItem});
            this.ItemMenu.Name = "PropertyViewMenu";
            this.ItemMenu.Size = new System.Drawing.Size(130, 148);
            this.ItemMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ItemMenu_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Enable";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Disable";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // delteteToolStripMenuItem
            // 
            this.delteteToolStripMenuItem.Name = "delteteToolStripMenuItem";
            this.delteteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.delteteToolStripMenuItem.Text = "Deltete";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // loadFormToolStripMenuItem
            // 
            this.loadFormToolStripMenuItem.Name = "loadFormToolStripMenuItem";
            this.loadFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadFormToolStripMenuItem.Text = "Load form ..";
            // 
            // saveToToolStripMenuItem
            // 
            this.saveToToolStripMenuItem.Name = "saveToToolStripMenuItem";
            this.saveToToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToToolStripMenuItem.Text = "Save to ..";
            // 
            // ThreadPropertiesMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ThreadPropertiesMenu";
            this.ListMenu.ResumeLayout(false);
            this.ItemMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ContextMenuStrip ListMenu;
        protected System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem stepIntoToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem stepOutToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        protected System.Windows.Forms.ToolStripMenuItem nextStepToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem breakToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.ContextMenuStrip ItemMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delteteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loadFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToToolStripMenuItem;
    }
}
