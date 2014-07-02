using libWatherDebugger.Script.Mode.VSDebugger;
using System;
using System.Collections.Generic;
using System.Threading;
using Watcher.Debugger;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetMallocActiveEnable : WatcherRule
    {
        private WatchAPISetting _setEnable;
        public SetMallocActiveEnable(bool enable)
        {
            _setting(enable);
        }

        private void _setting(bool enable)
        {
            Dictionary<bool, Func<bool>> setting = new Dictionary<bool, Func<bool>>() 
            {
                {true , _enable} , { false , _disable }
            };
            Func<bool> mode;
            if (setting.TryGetValue(enable, out mode))
                mode();
        }

        private bool _enable()
        {
            _setEnable = new SetWatchMallocEnable();
            return true;
        }
        private bool _disable()
        {
            _setEnable = new SetWatchMallocDisable();
            return true;
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() {
                //new DebuggerBreak().Run , 
                _setEnable.Run , null
            };
        }

    }
}
