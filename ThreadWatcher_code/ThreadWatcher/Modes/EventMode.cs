using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadWatcher.GUI;

namespace ThreadWatcher.Modes
{
    public abstract class EventMode<T> : IThreadWatcherMode , IThreadWatcherEvent<T>
    {
        protected GUIManagement _gui = GUIManagement.getInstance();
        public T Event { get; protected set; }
        public Type Type { get { return Event.GetType(); } }
        protected IDebugEngine2 _engine;
        protected IDebugProcess2 _process;
        protected IDebugProgram2 _program;
        protected IDebugThread2 _thread;
        public void Init(IDebugEngine2 engine, IDebugProcess2 process, IDebugProgram2 program, IDebugThread2 thread, IDebugEvent2 pEvent)
        {
            _engine = engine;
            _process = process;
            _program = program;
            _thread = thread;
            Event = (T)pEvent;
        }
        public virtual void Rule()
        {
            if (_precondition())
            {
                _rule();
            }
        }
        protected virtual bool _precondition()
        {
            return true;
        }
        protected virtual void _rule()
        {
            throw (new NotImplementedException());
        }
    }
}
