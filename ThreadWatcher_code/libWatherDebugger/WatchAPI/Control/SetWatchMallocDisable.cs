
using EnvDTE;
using EnvDTE90a;
using libWatcher.Constants;
using System.IO;
namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMallocDisable : WatchAPISetting
    {
        public SetWatchMallocDisable() 
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override void _setting(Watcher.Debugger.Debugger dbg)
        {
            //dbg.TryQuery("____watch_malloc_active = false");
            Breakpoints bps = dbg.VSDebugger.Breakpoints;

            foreach (Breakpoint3 bp in bps)
            {
                FileInfo file = new FileInfo(bp.File);
                if (file.Name == APICppFiles.APIFileName)
                    if (bp.FileLine == APICppFiles.MemoryAllocLine)
                    {
                        bp.Delete();
                        break;
                    }
            }
        }
    }
}
