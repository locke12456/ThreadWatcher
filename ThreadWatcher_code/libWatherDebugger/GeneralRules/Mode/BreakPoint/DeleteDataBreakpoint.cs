using libUtilities;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Script.Mode.BreakPoint;
using libWatherDebugger.Script.Mode.Document;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace Watcher.Debugger.GeneralRules.Mode.BreakPoint
{
    public class DeleteDataBreakpoint : WatcherRule, IBreakPoint
    {

        public DebugBreakpoint Breakpoint { get; set; }
        public IDebuggerMemory Data { get; set; }
        public DeleteDataBreakpoint() { }
        private DocumentClose _doc;
        protected override void _init()
        {
            _doc = new DocumentClose();
            _doc.FileName = libWatcher.Constants.APICppFiles.APIFileName;
            RemoveWatchPonit remove_script = new RemoveWatchPonit();
            remove_script.Data = Data.Variable;
            _script_list = new List<Func<bool>>() {
                   remove_script.Run, _doc.Run , null
                };
        }
    }
}
