using libWatcherDialog.DebugScriptEngine.Property;
using libWatcherDialog.PropertyItem.DebugScript;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Breakpoint
{
    public abstract class BreakpointRule : IDebugScriptRule
    {
        protected SourceFileInfo _breakpointInfo;
        protected List<Condition> _conditions;
        protected List<VirtualVariable> _vritualVariables;
        public BreakpointRule BreakPointInfo(object info) 
        {
            _breakpointInfo = new SourceFileInfo(info as Dictionary<string, object>);
            return this;
        }
        public DebugScriptItem GenerateScriptItem() 
        {
            DebugScriptItem item = null;
            return item;
        }
        public void AddCondition(object condition) 
        {
            Debug.Assert(condition is string);
            _conditions.Add(new Condition(condition));
        }
        public void AddVirtualVariable(object variable_info)
        {
            _vritualVariables.Add(new VirtualVariable(variable_info));
        }
        protected void _init_lists ()
        {
            _conditions = new List<Condition>();
            _vritualVariables = new List<VirtualVariable>();
        }
    }
}
