using Microsoft.VisualStudio.Debugger.Interop;

namespace ThreadWatcher.Modes
{
    interface IThreadWatcherEvent<T>
    {
        T Event { get; }
    }
    interface IThreadWatcherMode {
        void Init(IDebugEngine2 engine, IDebugProcess2 process, IDebugProgram2 program, IDebugThread2 thread, IDebugEvent2 pEvent);
        void Rule();
    }
}
