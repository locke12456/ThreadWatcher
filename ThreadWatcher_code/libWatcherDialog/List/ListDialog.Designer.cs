namespace libWatcherDialog.List
{
    partial class ListDialog<T>
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
            this.Items = new System.Windows.Forms.ListBox();
            this.Modify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Items
            // 
            this.Items.Dock = System.Windows.Forms.DockStyle.Top;
            this.Items.FormattingEnabled = true;
            this.Items.Location = new System.Drawing.Point(0, 0);
            this.Items.MaximumSize = new System.Drawing.Size(210, 251);
            this.Items.MinimumSize = new System.Drawing.Size(210, 251);
            this.Items.Name = "Items";
            this.Items.Size = new System.Drawing.Size(210, 251);
            this.Items.TabIndex = 0;
            // 
            // Modify
            // 
            this.Modify.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Modify.Location = new System.Drawing.Point(0, 250);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(210, 23);
            this.Modify.TabIndex = 0;
            this.Modify.Text = "modify";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.Modify_Click);
            // 
            // ListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 273);
            this.Controls.Add(this.Items);
            this.Controls.Add(this.Modify);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ListForm";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ListBox Items;
        protected System.Windows.Forms.Button Modify;
    }
}