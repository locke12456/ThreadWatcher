using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.GeneralRules.Mode.Debugger;

namespace libWatcherDialog.CombineRules.Script
{
    public class NotConcernedPoint : CombineRules
    {
        public NotConcernedPoint() 
        {
            _rules = new List<WatcherRule>() {
                new Continue() , LastRule
            };
        }
    }
}
