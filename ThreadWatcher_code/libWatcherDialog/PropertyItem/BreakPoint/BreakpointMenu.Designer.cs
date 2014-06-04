namespace libWatcherDialog.PropertyItem.BreakPoint
{
    partial class BreakpointMenu
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
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripAddProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.virtualVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
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
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripSeparator1,
            this.modeToolStripMenuItem,
            this.scriptToolStripMenuItem});
            this.ListMenu.Name = "PropertyViewMenu";
            this.ListMenu.Size = new System.Drawing.Size(153, 170);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataBreakpointToolStripMenuItem,
            this.breakpointToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // dataBreakpointToolStripMenuItem
            // 
            this.dataBreakpointToolStripMenuItem.Name = "dataBreakpointToolStripMenuItem";
            this.dataBreakpointToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.dataBreakpointToolStripMenuItem.Text = "Data Breakpoint";
            this.dataBreakpointToolStripMenuItem.Click += new System.EventHandler(this.dataBreakpointToolStripMenuItem_Click);
            // 
            // breakpointToolStripMenuItem
            // 
            this.breakpointToolStripMenuItem.Name = "breakpointToolStripMenuItem";
            this.breakpointToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.breakpointToolStripMenuItem.Text = "Breakpoint";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "Enable";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "Disable";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualModeToolStripMenuItem,
            this.scriptModeToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modeToolStripMenuItem.Text = "Mode";
            this.modeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.modeToolStripMenuItem_Click);
            this.modeToolStripMenuItem.Click += new System.EventHandler(this.modeToolStripMenuItem_Click);
            // 
            // manualModeToolStripMenuItem
            // 
            this.manualModeToolStripMenuItem.Name = "manualModeToolStripMenuItem";
            this.manualModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.manualModeToolStripMenuItem.Text = "Manual Mode";
            this.manualModeToolStripMenuItem.Click += new System.EventHandler(this.manualModeToolStripMenuItem_Click);
            // 
            // scriptModeToolStripMenuItem
            // 
            this.scriptModeToolStripMenuItem.Checked = true;
            this.scriptModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scriptModeToolStripMenuItem.Name = "scriptModeToolStripMenuItem";
            this.scriptModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.scriptModeToolStripMenuItem.Text = "Script Mode";
            this.scriptModeToolStripMenuItem.Click += new System.EventHandler(this.scriptModeToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.scriptToolStripMenuItem.Text = "Script";
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.scriptToolStripMenuItem_Click);
            // 
            // ItemMenu
            // 
            this.ItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAddProperty,
            this.toolStripSeparator5,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.editToolStripMenuItem,
            this.delteteToolStripMenuItem,
            this.toolStripSeparator4,
            this.loadFormToolStripMenuItem,
            this.saveToToolStripMenuItem});
            this.ItemMenu.Name = "PropertyViewMenu";
            this.ItemMenu.Size = new System.Drawing.Size(130, 176);
            // 
            // toolStripAddProperty
            // 
            this.toolStripAddProperty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conditionToolStripMenuItem,
            this.virtualVariableToolStripMenuItem});
            this.toolStripAddProperty.Name = "toolStripAddProperty";
            this.toolStripAddProperty.Size = new System.Drawing.Size(129, 22);
            this.toolStripAddProperty.Text = "Add";
            // 
            // conditionToolStripMenuItem
            // 
            this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
            this.conditionToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.conditionToolStripMenuItem.Text = "Condition";
            // 
            // virtualVariableToolStripMenuItem
            // 
            this.virtualVariableToolStripMenuItem.Name = "virtualVariableToolStripMenuItem";
            this.virtualVariableToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.virtualVariableToolStripMenuItem.Text = "Virtual variable";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(126, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem1.Text = "Enable";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "Disable";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(126, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // delteteToolStripMenuItem
            // 
            this.delteteToolStripMenuItem.Name = "delteteToolStripMenuItem";
            this.delteteToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.delteteToolStripMenuItem.Text = "Deltete";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
            // 
            // loadFormToolStripMenuItem
            // 
            this.loadFormToolStripMenuItem.Name = "loadFormToolStripMenuItem";
            this.loadFormToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.loadFormToolStripMenuItem.Text = "Load form ..";
            // 
            // saveToToolStripMenuItem
            // 
            this.saveToToolStripMenuItem.Name = "saveToToolStripMenuItem";
            this.saveToToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.saveToToolStripMenuItem.Text = "Save to ..";
            // 
            // BreakpointMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "BreakpointMenu";
            this.Size = new System.Drawing.Size(150, 138);
            this.ListMenu.ResumeLayout(false);
            this.ItemMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ContextMenuStrip ListMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBreakpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        protected System.Windows.Forms.ContextMenuStrip ItemMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delteteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loadFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripAddProperty;
        private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem virtualVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptModeToolStripMenuItem;
    }
}
