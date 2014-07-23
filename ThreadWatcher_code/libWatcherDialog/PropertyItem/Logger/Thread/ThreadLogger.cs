using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.Logger
{
    public partial class ThreadLogger : ThreadLoggerRef
    {
        public ThreadLogger()
            : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Log
            // 
            this.Log.ItemHeight = 12;
            this.Log.Size = new System.Drawing.Size(339, 112);
            // 
            // ThreadLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(339, 120);
            this.Name = "ThreadLogger";
            this.ResumeLayout(false);

        }
    }
    public class ThreadLoggerRef : Logger<ThreadLog> {
        public ThreadLoggerRef() : base() { }
    }
}
