using libWatcher.Constants;
using libWatherDebugger.DocumentContext;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Breakpoint
{
    public class DebugBreakpoint : IDebugItem
    {
        public DebugDocument Document { get; set; }
        private IDebugBoundBreakpoint2 _breakpoint;
        private Dictionary<enum_BP_TYPE, string> _kinds;
        public EnvDTE.Breakpoint Information { get; set; }
        public IDebugBoundBreakpoint2 Breakpoint
        {
            get { return _breakpoint; }
            set
            {
                _breakpoint = value;
                IDebugPendingBreakpoint2 pendingBreakpoint;
                _breakpoint.GetPendingBreakpoint(out pendingBreakpoint);
                PendingBreakpoint = pendingBreakpoint;
            }
        }
        public IDebugPendingBreakpoint2 PendingBreakpoint { get; private set; }
        public BP_RESOLUTION_INFO BreakpointInfo { get; set; }
        public enum_BP_TYPE BreakpointType
        {
            get;
            set;
        }
        public string BreakpointKind
        {
            get { return _kinds[BreakpointType]; }
        }
        public bool IsCodeBreakpoint
        {
            get
            {
                return BreakpointType == enum_BP_TYPE.BPT_CODE;
            }
        }
        public bool IsDataBreakpoint
        {
            get
            {
                return BreakpointType == enum_BP_TYPE.BPT_DATA;
            }
        }
        public string FileName
        {
            get {
                return Document.FileName;
            }
        }
        public string FunctionName
        {
            get
            {

                //if (Information == null) _init();
                return Information.FunctionName;
            }
        }
        public DebugBreakpoint()
        {
            _kinds = new Dictionary<enum_BP_TYPE, string>() {
                { enum_BP_TYPE.BPT_CODE,Types.Breakpoint },
                { enum_BP_TYPE.BPT_DATA,Types.DataBreakpoint },
            };
        }
        public bool Enable()
        {
            PendingBreakpoint.Enable(1);
            return true;
        }
        public bool Delete()
        {
            PendingBreakpoint.Delete();
            return true;
        }
        public bool GetRequest()
        {
            IDebugBreakpointRequest2 req;
            PendingBreakpoint.GetBreakpointRequest(out req);
            //req.GetRequestInfo(enum_BPREQI_FIELDS.BPREQI_ALLFIELDS,
            return true;
        }
        public bool Equals(string name)
        {
            name = name.Replace("0x", "").ToUpper();
            return Information.Name.IndexOf(name) != -1;
        }
    }
}
