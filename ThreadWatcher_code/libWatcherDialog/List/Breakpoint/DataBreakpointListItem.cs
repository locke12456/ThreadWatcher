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
        public string FileName {
            get {
                return File != null ? File.Name : "";
            }
        }
        public DataBreakpointListItem()
        {
            Position = 0;
        }
        public override string ToString()
        {
            Name = MemoryAddressInfo.Variable;
            Message = FileName + " Line :" + Position.ToString();
            return base.ToString();
        }
    }
}
