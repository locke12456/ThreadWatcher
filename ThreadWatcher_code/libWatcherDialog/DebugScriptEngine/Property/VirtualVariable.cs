using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    
    public class VirtualVariable
    {
        public struct VariableInfo 
        {
            public string name;
            public string reference;
            public string type;
        }
        public VirtualVariable() { }
        public VirtualVariable(object variable_info) 
        {
            VariableInfo info = (VariableInfo)variable_info;
            
        }
    }
}
