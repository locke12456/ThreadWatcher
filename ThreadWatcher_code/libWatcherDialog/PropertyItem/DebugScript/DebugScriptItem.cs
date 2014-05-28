using EnvDTE;
using libWatcherDialog.DebugScriptEngine.Property;
using libWatcherDialog.PropertyItem.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public class DebugScriptItem : PropertyItem<DebugScripts, DebugScriptProperty>
    {
        private bool _enable = false;

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                _debug_script_enable();
            }
        }

        private void _debug_script_enable()
        {
            if (BreakpointInfo == null) return;
            if (!_enable)
            {
                Infomation.Delete();
                return;
            }
            EnvDTE.Breakpoints bps = Watcher.Debugger.Debugger.getInstance().VSDebugger.Breakpoints.Add("", BreakpointInfo.filename, BreakpointInfo.line, 1);
            foreach (Breakpoint bp in bps)
            {
                FileInfo file = new FileInfo(bp.File);
                if (BreakpointInfo.filename == file.Name && BreakpointInfo.line == bp.FileLine)
                {
                    Infomation = bp;
                    return;
                }
            }
        }
        public Breakpoint Infomation
        {
            get;
            private set;
        }
        public SourceFileInfo BreakpointInfo
        {
            get;
            set;
        }
        public Condition Conditions
        {
            get;
            set;
        }
        public List<VirtualVariable> VirtualVariables
        {
            get;
            set;
        }
        public WatchTarget Target
        {
            get;
            set;
        }
        public DebugScriptItem()
        {
        }
        public override string ToString()
        {
            string msg = BreakpointInfo.code;
            msg = msg.Replace("\t", " ");
            string enable = Enable ? "[+]" : "[ ]";
            return enable + msg;
            //return "{"+BreakpointInfo.line+"}"+BreakpointInfo.filename;
        }
    }
}