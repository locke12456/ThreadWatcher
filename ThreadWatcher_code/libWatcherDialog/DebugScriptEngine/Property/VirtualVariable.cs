using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class VariableInfoProperties
    {
        public const string Name      = "name";
        public const string Reference = "reference";
        public const string Type      = "type";
    }
    
    public class VariableInfo : PropertyInfo
    {
        [Property(VariableInfoProperties.Name)]
        public string name { get; set; }
        [Property(VariableInfoProperties.Reference)]
        public string reference { get; set; }
        [Property(VariableInfoProperties.Type)]
        public string type { get; set; }
        public VariableInfo() :base() { }
        public VariableInfo(Dictionary<string, object> json) : base(json)
        {

        }
    }
    public class VirtualVariable
    {

        public VirtualVariable() { }
        public VirtualVariable(object variable_info)
        {
            VariableInfo info = new VariableInfo(variable_info as Dictionary<string , object>);

        }
    }
}
