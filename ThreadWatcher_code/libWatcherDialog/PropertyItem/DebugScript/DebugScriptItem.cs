using libWatcherDialog.DebugScriptEngine.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public class DebugScriptItem : PropertyItem<DebugScripts, DebugScriptProperty>
    {
        public SourceFileInfo BreakpointInfo
        {
            get;
            set;
        }
        public List<Condition> Conditions
        {
            get;
            set;
        }
        public List<VirtualVariable> VirtualVariables
        {
            get;
            set;
        }
        public DebugScriptItem()
        {
        }
    }
}
