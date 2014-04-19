using libWatcherDialog.GeneralRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.CombineRules
{
    public class CombineRules : ICombineRule
    {
        public event RuleEventHandler RuleProgressing;
        public event RuleEventHandler RuleCompleted;
        protected Debugger _dbg = Debugger.getInstance();
        protected List<WatcherRule> _rules;
        protected const WatcherRule LastRule = null;
        protected int _index;
        public virtual bool Run()
        {
            _index = 0;
            _next();
            return true;
        }
        protected virtual void _init()
        {

        }
        protected void _next()
        {
            if (_rules == null) return;
            if (_index >= _rules.Count) return;
            WatcherRule rule = _rules[_index++];
            if (rule == LastRule)
            {
                _finish();
                return;
            }
            _init();
            rule.RuleCompleted += rule_RuleCompleted;
            rule.RunRules();
        }
        protected virtual void _finish()
        {
            if (RuleCompleted != null) RuleCompleted(this, new GeneralRules.EventArgs.RuleEventArgs());
        }
        protected void rule_RuleCompleted(object sender, GeneralRules.EventArgs.RuleEventArgs e)
        {
            WatcherRule rule = sender as WatcherRule;
            rule.RuleCompleted -= rule_RuleCompleted;
            _next();
        }
    }
}
