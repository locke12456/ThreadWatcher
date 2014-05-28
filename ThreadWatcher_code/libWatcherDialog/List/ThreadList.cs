using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.List
{
    public class ThreadList : ListDialog<ThreadItem>
    {
        public ThreadList() : base() 
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(0, 246);
            // 
            // ThreadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(210, 269);
            this.Name = "ThreadList";
            this.ResumeLayout(false);

        }
    }
}
