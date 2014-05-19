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
        public const string Name = "name";
        public const string Reference = "reference";
        public const string Type = "type";
    }

    public class VariableInfo : PropertyInfo
    {
        [Property(VariableInfoProperties.Name)]
        public string name { get; set; }
        [Property(VariableInfoProperties.Reference)]
        public string reference { get; set; }
        [Property(VariableInfoProperties.Type)]
        public string type { get; set; }
        public VariableInfo() : base() { }
        public VariableInfo(Dictionary<string, object> json)
            : base(json)
        {

        }
    }
    public class VirtualVariable
    {
        public VariableInfo Information { get; private set; }
        public VirtualVariable() { }
        public VirtualVariable(object variable_info)
        {
            _init_information(variable_info);
        }

        private void _init_information(object variable_info)
        {
            Dictionary<string, object> var_info = variable_info as Dictionary<string, object>;
            Debug.Assert(var_info != null);
            Information = new VariableInfo(var_info);
        }
    }
}
