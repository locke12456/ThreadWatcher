using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.EventArgs;

namespace Watcher.Debugger
{
    /// <summary>
    /// WatcherRule會開啟另外一條執行緒，去執行 _script_list 裡面所有的function
    /// </summary>
    public abstract class WatcherRule : IWatcherRule , IRuleEvent
    {
        private static AutoResetEvent sync = null;
        //public delegate void RuleEventHandler(object sender, RuleEventArgs e);
        public event RuleEventHandler RuleProgressing;
        public event RuleEventHandler RuleCompleted;
        protected Debugger _dbg = Debugger.getInstance();
        /// <summary>
        /// List 裡為 function pointer, 需要回傳 bool 的 function
        /// 繼承時需要自己宣告，需要依序執行的步驟之function
        /// e.g. 
        ///     DeleteDataBreakpoint 這個組合的rule
        ///     首先需要使用 RemoveWatchPonit class 的 rule.
        ///     然後判斷當前開啟的file是不是Watchapi的file , 如果是就把他關掉 .
        ///     所以第二個步驟是 DocumentClose class 的 rule
        ///     
        ///     list裡面的元素就必須有 : 
        ///     RemoveWatchPonit.Run , DocumentClose.Run , null
        ///     最後一個元素為null,表示已經是最後一個rule.
        ///     
        /// </summary>
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
        /// <summary>
        /// 若 sync 為 null 表示是程式剛啟動
        /// 則新創一個 AutoResetEvent 
        /// 若不是，則表示有另一個任務在執行
        /// 程式會等待同步，同步原理參考 AutoResetEvent
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 執行所有已經排在 _script_list 中的function
        /// </summary>
        protected virtual void _rules()
        {
            bool Break = false;
            int index = 0;
            while (!Break)
            {
                if (RuleProgressing != null) RuleProgressing(this, new RuleEventArgs());
                //取出第一個任務
                Func<bool> step = _script_list[index];
                _step_start();
                if (step())
                {
                    //如果檢查到元素為null則結束回圈
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
                Debug.WriteLine(fail.Message);
                _reset();
                //throw (fail);
            }
            _finish();
        }
        
    }
}
