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
        public SourceFileInfo BreakpointInfo
        {
            get;
            protected set;
        }
        
        public Condition Conditions
        {
            get;
            protected set;
        }
        public List<VirtualVariable> VirtualVariables
        {
            get;
            protected set;
        }
        public WatchTarget Target
        {
            get;
            set;
        }

        public BreakpointRule BreakPointInfo(object info) 
        {
            BreakpointInfo = new SourceFileInfo(info as Dictionary<string, object>);
            return this;
        }
        public DebugScriptItem GenerateScriptItem() 
        {
            DebugScriptItemFactory factory = new DebugScriptItemFactory(this);
            factory.CreateProduct();
            DebugScriptItem item = factory.Product;
            DebugScriptsManagement.getInstance().AddItem(item);
            return item;
        }
        public void SetWatchTarget(object target) 
        {
            Target = new WatchTarget(target as Dictionary<string, object>);
        }
        public void AddCondition(object condition) 
        {
            Debug.Assert(condition is string);
            Conditions = (new Condition(condition));
        }
        public void AddVirtualVariable(object variable_info)
        {
            VirtualVariables.Add(new VirtualVariable(variable_info));
        }
        protected void _init_lists ()
        {
            Conditions = new Condition();
            VirtualVariables = new List<VirtualVariable>();
        }
    }
}
