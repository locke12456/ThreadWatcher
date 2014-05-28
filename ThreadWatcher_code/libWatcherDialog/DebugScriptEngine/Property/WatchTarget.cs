using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class WatchType {
        public const string POINTER = "pointer";
        public const string VARIABLE = "variable";
    }
    public class WatchTargetProperties
    {
        public const string Type = "type";
    }

    public class WatchTarget : PropertyInfo
    {
        [Property(WatchTargetProperties.Type)]
        public string type { get; set; }
        public bool IsPointer { get { return type == WatchType.POINTER; } }
        public WatchTarget() : base() 
        {
        
        }
        public WatchTarget(Dictionary<string,object> json )
            : base(json)
        {

        }
    }
}
