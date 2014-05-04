using libWatcherDialog.GeneralRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.CombineRules
{
    public enum ScriptQueueOrder
    {
        QueueNull, QueueReady, QueueBusy
    };
    public class ScriptQueue<T> : IScriptQueue<T> where T : WatcherRule
    {
        public Queue<T> Scripts { get; set; }
        public ScriptQueue() {
            Scripts = new Queue<T>();
        }
        
        public void RunRule()
        {
            _switch_order_mode();
        }
        public void AddRule(T script)
        {
            _add_script_to_quque(script);
        }

        private void _switch_order_mode()
        {
            Dictionary<int, Func<bool>> _modes = new Dictionary<int, Func<bool>>() {
                { (int)ScriptQueueOrder.QueueReady , _runScript }
            };
            Func<bool> mode;
            if (_modes.TryGetValue(Scripts.Count, out mode))
                mode();
        }

        private void _add_script_to_quque(T script)
        {
            Scripts.Enqueue(script);
        }

        private void RuleCompleted(object sender, GeneralRules.EventArgs.RuleEventArgs e)
        {
            T script = Scripts.Dequeue();
            _switch_order_mode();
        }

        private bool _runScript()
        {
            T script = Scripts.Peek();
            if (script != null)
            {
                script.RuleCompleted += RuleCompleted;
                script.RunRules();
            }
            return true;
        }
    }
    public class WatcherRuleQueue : ScriptQueue<WatcherRule> {
        public WatcherRuleQueue() {
        }
    }
}
