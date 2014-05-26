using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.EventArgs;

namespace Watcher.Debugger
{
    public abstract class WatcherRule : IWatcherRule , IRuleEvent
    {
        
        private static AutoResetEvent sync = null;
        //public delegate void RuleEventHandler(object sender, RuleEventArgs e);
        public event RuleEventHandler RuleProgressing;
        public event RuleEventHandler RuleCompleted;
        protected Debugger _dbg = Debugger.getInstance();
        protected List<Func<bool>> _script_list;
        protected Dictionary<string, List<Func<bool>>> _conditions;
        public virtual bool RunRules() {
            _init();
            return _run();
        }
        protected virtual void _init()
        {
        
        }
        protected virtual bool _condition() {
            return true;
        }
        protected virtual bool _run() {
            Thread thread = new Thread(_thread);
            thread.Start();
            return true;
        }

        protected bool _exit()
        {
            return true;
        }
        protected virtual bool _start() 
        {
            return true;
        }
        protected virtual bool _wait()
        {
            if (sync == null) sync = new AutoResetEvent(false);
            else sync.WaitOne();
            _start();
            return true;
        }
        protected virtual bool _finish() {
            sync.Set();
            if (RuleCompleted != null) 
                RuleCompleted(this, new RuleEventArgs());
            return true;
        }
        protected virtual bool _reset()
        {
            sync.Reset();
            return true;
        }
        protected virtual void _step_start() 
        {
        
        }
        protected virtual void _step_finish() 
        {
        
        }
        protected virtual void _rules()
        {
            bool Break = false;
            int index = 0;
            while (!Break)
            {
                if (RuleProgressing != null) RuleProgressing(this, new RuleEventArgs());
                Func<bool> step = _script_list[index];
                _step_start();
                if (step())
                {
                    Break = _script_list[++index] == null;
                }
                _step_finish();
            }
        }
        protected virtual void _thread(object args) {
            _wait();
            try
            {
                _rules();
            }
            catch (Exception fail) {
                _reset();
                throw (fail);
            }
            _finish();
        }
        
    }
}
