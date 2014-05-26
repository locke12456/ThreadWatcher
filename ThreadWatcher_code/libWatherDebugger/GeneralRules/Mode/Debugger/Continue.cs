using libWatherDebugger.Script.Mode.VSDebugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher.Debugger.GeneralRules.Mode.Debugger
{
    public class Continue : WatcherRule
    {
        public Continue()
        {
            _script_list = new List<Func<bool>>() { 
                   _exit, null
            };
        }
        protected override void _init()
        {
                _script_list = new List<Func<bool>>() { 
                    new DebuggerContinue().Run , null
                };
            
        }
    }
}
