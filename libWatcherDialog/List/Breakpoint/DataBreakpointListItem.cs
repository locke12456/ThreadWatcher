using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.List
{
    public class DataBreakpointListItem : ListItem
    {
        public HeapMemory MemoryAddressInfo { get; set; }
        public FileInfo File { get; set; }
        public uint Position { get; set; }
        public DataBreakpointListItem()
        {
        }
        public override string ToString()
        {
            Name = MemoryAddressInfo.Variable;
            Message = File.Name + " Line :" + Position.ToString();
            return base.ToString();
        }
    }
}
