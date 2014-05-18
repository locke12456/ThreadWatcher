using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class SourceFileInfo
    {
        public string FileName { get; set; }
        public int Line { get; set; }
        public string Code { get; set; }
        public string Function { get; set; }
        public string Type { get; set; }
        public SourceFileInfo() { }
    }
}
