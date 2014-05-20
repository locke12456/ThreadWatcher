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
        protected SourceFileInfo _breakpointInfo;
        protected List<Condition> _conditions;
        protected List<VirtualVariable> _vritualVariables;
    }
}
