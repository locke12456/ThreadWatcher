namespace libWatcherDialog.PropertyItem.DebugScript
{
    partial class DebugScriptsMenu
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
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.debugScriptEnabe = new System.Windows.Forms.ToolStripMenuItem();
            this.debugScriptDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
            this.debugScriptOpen = new System.Windows.Forms.OpenFileDialog();
            this.ListMenu.SuspendLayout();
            this.ItemMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListMenu
            // 
            this.ListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptToolStripMenuItem,
            this.toolStripSeparator2,
            this.debugScriptEnabe,
            this.debugScriptDisable,
            this.toolStripSeparator1});
            this.ListMenu.Name = "PropertyViewMenu";
            this.ListMenu.Size = new System.Drawing.Size(105, 82);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.scriptToolStripMenuItem.Text = "Script";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(101, 6);
            // 
            // debugScriptEnabe
            // 
            this.debugScriptEnabe.Name = "debugScriptEnabe";
            this.debugScriptEnabe.Size = new System.Drawing.Size(104, 22);
            this.debugScriptEnabe.Text = "Enable";
            this.debugScriptEnabe.Click += new System.EventHandler(this.debugScriptEnabe_Click);
            // 
            // debugScriptDisable
            // 
            this.debugScriptDisable.Name = "debugScriptDisable";
            this.debugScriptDisable.Size = new System.Drawing.Size(104, 22);
            this.debugScriptDisable.Text = "Disable";
            this.debugScriptDisable.Click += new System.EventHandler(this.debugScriptDisable_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(101, 6);
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
            // debugScriptOpen
            // 
            this.debugScriptOpen.Filter = "Script Files(.js)|*.js";
            this.debugScriptOpen.Title = "Select DebugScript";
            // 
            // DebugScriptsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DebugScriptsMenu";
            this.ListMenu.ResumeLayout(false);
            this.ItemMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ContextMenuStrip ListMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugScriptEnabe;
        private System.Windows.Forms.ToolStripMenuItem debugScriptDisable;
        private System.Windows.Forms.OpenFileDialog debugScriptOpen;
    }
}
