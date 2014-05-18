using libWatcherDialog.DebugScriptEngine.Property;
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

        protected List<Condition> _conditions;
        protected List<VirtualVariable> _vritualVariables;
        public BreakpointRule BreakPointInfo(string filename, int line) 
        {

            return this;
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
    }
}
