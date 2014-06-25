using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetMallocActiveEnable : WatcherRule
    {
        private SetWatchMallocEnable _setEnable;
        public SetMallocActiveEnable(DebugStackFrame stack)
        {
            _setEnable = new SetWatchMallocEnable(stack);
            _setEnable.RegisterASyncEvent();
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() {
                _setEnable.Run
            };
        }
    }
}
