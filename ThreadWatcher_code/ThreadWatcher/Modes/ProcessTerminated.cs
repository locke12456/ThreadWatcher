using Microsoft.VisualStudio.Debugger.Interop;
using System.Collections.Generic;
using ThreadWatcher.GUI;
using System.IO;
using libWatcher.Constants;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.DebugScript;
namespace ThreadWatcher.Modes
{
    public class ProcessTerminated : EventMode<IDebugProgramDestroyEvent2>
    {
        public ProcessTerminated()
        {
        }
        protected override void _rule()
        {
            _remove_breakpoint();
            GUIManagement.Destroy();
        }

        private void _remove_breakpoint()
        {
            List<EnvDTE.Breakpoint> bps = new List<EnvDTE.Breakpoint>();
            _api_bp_collection(bps);
            foreach (EnvDTE.Breakpoint bp in bps)
                bp.Delete();
        }

        private void _api_bp_collection(List<EnvDTE.Breakpoint> bps)
        {
            foreach (EnvDTE.Breakpoint bp in _gui.Debugger.VSDebugger.Breakpoints)
            {
                bool result = _is_watchpoint(bp.Name);
                if (bp.File != "")
                {
                    FileInfo file = new FileInfo(bp.File);
                    result = _is_api_file(file) || _is_script_item(bp);
                }
                if (result)
                    bps.Add(bp);
            }
        }

        private static bool _is_api_file(FileInfo file)
        {
            return file.Name == APICppFiles.APIFileName;
        }
        private static bool _is_script_item(EnvDTE.Breakpoint bp)
        {
            DebugScriptsManagement scripts = DebugScriptsManagement.getInstance();
            DebugScriptItem script = scripts.GetItem(bp);
            return script != null;
        }
        private static bool _is_watchpoint(string name)
        {
            return name.IndexOf("0x") >= 0;
        }

    }
}
