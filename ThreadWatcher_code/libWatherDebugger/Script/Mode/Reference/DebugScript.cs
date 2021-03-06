﻿using EnvDTE;
using libWatherDebugger.Script.ScriptEvent;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libWatherDebugger.Script
{
    /// <summary>
    /// DebugScript用於對IDE的操作，若要開新的操作
    /// 需要複寫以下方法 : 
    ///     必要 : _tryControl
    ///     次要 : _init 
    /// </summary>
    public abstract class DebugScript : IDebugScript
    {
        private static AutoResetEvent _sync;
        protected EnvDTE.Debugger _dbg;
        protected Dictionary<dbgDebugMode, Func<bool>> _switchMode;
        protected static Type _sync_type = null;
        protected static DebugScript _sync_script = null;
        public delegate void EventHandler(object obj, ScriptEventArgs e);
        public virtual event EventHandler ASyncCompleteEvent;
        public event EventHandler StartEvent;
        public event EventHandler CompleteEvent;
        public event EventHandler PendingEvent;
        public static Type ASyncEvent { get { return _sync_type; } }
        public DebugScript()
        {
            _init();
            _dbg = Watcher.Debugger.Debugger.getInstance().VSDebugger;
        }
        public DebugScript(EnvDTE.Debugger dbg)
        {
            _init();
            _dbg = dbg;
        }
        /// <summary>
        /// 執行操作
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Func<bool> mode;
            if (_switchMode.TryGetValue(_dbg.CurrentMode, out mode))
                return mode();
            return false;
        }
        /// <summary>
        /// 註冊同步規則
        /// </summary>
        public void RegisterASyncEvent()
        {
            StartEvent += IntoStartEvent;
            CompleteEvent += IntoCompleteEvent;
        }
        public static bool HasASyncScript()
        {
            return _sync_script != null;
        }

        public static void RegisterASyncEvent(Type type)
        {
            if (_sync == null) _sync = new AutoResetEvent(false);
            _sync.Reset();
            _sync_type = type;
        }

        public static void ResetASyncScript()
        {
            _sync_type = null;
            _sync_script = null;
        }
        public static void WaitSync()
        {
            _sync.WaitOne();
        }
        public static void FinishSync()
        {
            _sync.Set();
        }
        public virtual dbgDebugMode Mode
        {
            get;
            protected set;
        }
        public virtual Type ASyncEventType { get { return null; } }
        
        protected void IntoStartEvent(object obj, ScriptEvent.ScriptEventArgs e)
        {
            _sync_script = this;
            RegisterASyncEvent(ASyncEventType);
        }
        protected void IntoCompleteEvent(object obj, ScriptEvent.ScriptEventArgs e)
        {
            //WaitSync();
           // if (ASyncCompleteEvent != null) ASyncCompleteEvent(this, new ScriptEventArgs());
        }
        protected virtual void _init()
        {
            _switchMode = new Dictionary<dbgDebugMode, Func<bool>>() {
                {dbgDebugMode.dbgBreakMode , _command},
                {dbgDebugMode.dbgDesignMode , _wait},
                {dbgDebugMode.dbgRunMode , _wait},
            };
        }
        protected virtual bool _command()
        {
            bool result = false;
            dbgDebugMode mode = _dbg.CurrentMode;
            if (mode == Mode)
            {
                try
                {
                    if (StartEvent != null) StartEvent(this, new CompleteEventArgs());
                    result = _tyrToControl();
                    if (CompleteEvent != null) 
                        CompleteEvent(this, new CompleteEventArgs());
                }
                catch (System.Exception fail)
                {
                    Debug.WriteLine("fail");
                }
            }
            return result;
        }
        protected virtual bool _tyrToControl()
        {
            return true;
        }


        protected virtual bool _wait()
        {
            if (PendingEvent != null) PendingEvent(this, new PendingEventArgs());
            return false;
        }
    }
}
