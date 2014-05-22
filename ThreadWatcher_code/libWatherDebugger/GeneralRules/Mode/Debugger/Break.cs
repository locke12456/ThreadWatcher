using libWatherDebugger.Script.Mode.VSDebugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace Watcher.Debugger.GeneralRules.Mode.Debugger
{
    public class Break : WatcherRule
    {
        public Break() : base()
        {
            _script_list = new List<Func<bool>>() 
            {
                 _exit,null
            };
        }
        
        protected override void _init()
        {
            if (_dbg.VSDebugger.CurrentMode != EnvDTE.dbgDebugMode.dbgBreakMode) 
            {
                _script_list = new List<Func<bool>>() 
                {
                    new DebuggerBreak().Run,null
                };
            }
        }
    }
}
