using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.List
{

    public partial class DataBreakpointList : DataBreakpointListRef
    {
        public DataBreakpointList()
            : base()
        {
            InitializeComponent();
        }
        public void AddDatas(List<DataBreakpointListItem> datas)
        {
            foreach (DataBreakpointListItem data in datas)
                AddItem(data);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Modify
            // 
            this.Modify.Text = "Add";
            // 
            // DataBreakpointList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(210, 273);
            this.Name = "DataBreakpointList";
            this.Text = "DataBreakpoints";
            this.ResumeLayout(false);

        }

    }
    public class DataBreakpointListRef : ListDialog<DataBreakpointListItem> 
    {
        public DataBreakpointListRef() : base() { }
    }
}
