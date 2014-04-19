using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.Document
{
    public class Document : DebugScript
    {
        public string FileName { set; get; }
        public Document() : base() { }
    }
}
