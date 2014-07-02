
using EnvDTE;
using EnvDTE90a;
using libWatcher.Constants;
namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMallocEnable : WatchAPISetting
    {
        public SetWatchMallocEnable() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgRunMode;
        }
        protected override void _setting(Watcher.Debugger.Debugger dbg)
        {
            dbg.VSDebugger.Breakpoints.Add("", APICppFiles.APIFileName, APICppFiles.MemoryAllocLine, 1);
            //dbg.TryQuery("____watch_malloc_active = true");
            
        }
    }
}
