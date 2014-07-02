using libWatherDebugger.Script;
using System.Threading;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMalloc
    {
        private AutoResetEvent _sync;
        public SetWatchMalloc()
        {
            _sync = new AutoResetEvent(false);
        }
        public void Enable()
        {
            bool setting = true;
            _set_enable(setting);
        }

        public void Disable()
        {
            bool setting = false;
            _set_enable(setting);
        }
        private void _set_enable(bool setting)
        {
            SetMallocActiveEnable enable = new SetMallocActiveEnable(setting);
            enable.RuleCompleted += enable_RuleCompleted;
            enable.RunRules();
          
            _sync.WaitOne();
        }

        private void enable_RuleCompleted(object sender, Watcher.Debugger.EventArgs.RuleEventArgs e)
        {
            _sync.Set();
        }
    }
}
