﻿using EnvDTE90a;
using libWatcher.Constants;
using libWatherDebugger.DocumentContext;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.Breakpoint
{
    public class DebugBreakpoint : IDebugItem
    {
        private Debugger dbg = Debugger.getInstance();
        public DebugDocument Document { get; set; }
        private IDebugBoundBreakpoint2 _breakpoint;
        private IDebugBreakpointRequest2 _breakpointRequest;
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
        public IDebugBreakpointRequest2 BreakpointRequest
        {
            get
            {
                PendingBreakpoint.GetBreakpointRequest(out _breakpointRequest);
                return _breakpointRequest;
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
        public string Name { get; set; }
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
        public string Condition
        {
            get {
                EnvDTE90a.Breakpoint3 bp3 = Information as EnvDTE90a.Breakpoint3;
                return bp3.Condition;
            }
            set {
                BP_REQUEST_INFO[] bp_info = new BP_REQUEST_INFO[1];
                BreakpointRequest.GetRequestInfo((uint)enum_BPREQI_FIELDS.BPREQI_CONDITION,bp_info);
                bp_info[0].bpCondition.bstrCondition = value;
                Breakpoint.SetCondition(bp_info[0].bpCondition);
            }

        }
        public DebugBreakpoint()
        {
            _kinds = new Dictionary<enum_BP_TYPE, string>() {
                { enum_BP_TYPE.BPT_CODE,Types.Breakpoint },
                { enum_BP_TYPE.BPT_DATA,Types.DataBreakpoint },
            };
        }
        public void FirstBreak(DebugBreakpoint bpt) 
        {
            EnvDTE.Breakpoints bps = dbg.VSDebugger.Breakpoints;
            string data = bpt.Information.Name.Replace("0x", "").ToUpper();
            _set_as_tracepoint(bpt, bps, data);
            _get_debug_breakpoint_infos(bpt);
        }

        private void _get_debug_breakpoint_infos(DebugBreakpoint bpt)
        {
            Breakpoint = bpt.Breakpoint;
            BreakpointInfo = bpt.BreakpointInfo;
            BreakpointType = bpt.BreakpointType;
            Document = bpt.Document;
        }
        private void _set_information(DebugBreakpoint bpt, EnvDTE.Breakpoints bps, string data)
        {
            foreach (EnvDTE.Breakpoint bp_c in bps)
            {
                if (bp_c.Name.IndexOf(data) != -1)
                {
                    Information = bp_c;
                }
            }
        }
        private static void _set_as_tracepoint(DebugBreakpoint bpt, EnvDTE.Breakpoints bps, string data)
        {
            foreach (var bp_c in bps)
            {
                Breakpoint3 bp = bp_c as Breakpoint3;
                if (bp.Name.IndexOf(data) != -1)
                {
                    bp.BreakWhenHit = false;
                    bp.Message = bpt.Information.Name;
                }
            }
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
