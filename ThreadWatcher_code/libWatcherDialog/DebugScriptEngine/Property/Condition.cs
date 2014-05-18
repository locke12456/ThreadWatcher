using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class Condition
    {
        public string DebugCondition { get; set; }
        public Condition() 
        {
        
        }
        public Condition(object condition)
        {
            DebugCondition = condition.ToString();
        }
    }
}
