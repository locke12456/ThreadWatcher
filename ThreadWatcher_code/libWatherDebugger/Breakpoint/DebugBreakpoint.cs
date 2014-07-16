using EnvDTE90a;
using libWatcher.Constants;
using libWatherDebugger.DocumentContext;
using libWatherDebugger.GeneralRules.Mode.BreakPoint;
using libWatherDebugger.Script;
using libWatherDebugger.Script.Mode.BreakPoint;
using libWatherDebugger.Script.Mode.VSDebugger;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatherDebugger.Breakpoint
{
    /// <summary>
    /// Breakpoint information item
    /// </summary>
    public class DebugBreakpoint : IDebugItem
    {
        //private delegate void _set_condition(string val);
        private delegate void BreakTriggered(object sender, string value);
        private BreakTriggered _reset_condition_event;
        private Debugger dbg = Debugger.getInstance();
        private IDebugBoundBreakpoint2 _breakpoint;
        private IDebugBreakpointRequest2 _breakpointRequest;
        private IDebugBreakpointRequest3 _breakpointRequest3;
        private Dictionary<enum_BP_TYPE, string> _kinds;
        private string _condition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DebugBreakpoint()
        {
            _kinds = new Dictionary<enum_BP_TYPE, string>() {
                { enum_BP_TYPE.BPT_CODE,Types.Breakpoint },
                { enum_BP_TYPE.BPT_DATA,Types.DataBreakpoint },
            };
        }
        /// <summary>
        /// 使用於當 type 為 DataBreakpoint 時
        /// 
        /// 若有設定或重設Condition，此時會註冊 _reset_condition_event  
        /// Condition 等到 watchpoint 中斷時， Condition 才會被重設
        /// 
        /// 如果是第一次中斷，會將 watchpoint 設成 trace point
        /// </summary>
        /// <param name="bpt"></param>
        /// <returns>true or false</returns>
        public bool FirstBreak(DebugBreakpoint bpt)
        {
            if (_breakpoint != null)
            {
                if (_reset_condition_event != null) _reset_condition_event(this, _condition);
                return false;
            }
            return _first_break(bpt);
        }
        /// <summary>
        /// no used
        /// </summary>
        /// <returns></returns>
        public bool Enable()
        {
            PendingBreakpoint.Enable(1);
            return true;
        }
        /// <summary>
        /// no used
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            PendingBreakpoint.Delete();
            return true;
        }
        /// <summary>
        /// no used
        /// </summary>
        /// <returns></returns>
        public bool GetRequest()
        {
            IDebugBreakpointRequest2 req;
            PendingBreakpoint.GetBreakpointRequest(out req);
            //req.GetRequestInfo(enum_BPREQI_FIELDS.BPREQI_ALLFIELDS,
            return true;
        }
        /// <summary>
        /// no used
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Equals(string name)
        {
            name = name.Replace("0x", "").ToUpper();
            return Information.Name.IndexOf(name) != -1;
        }
        /// <summary>
        /// 中斷位置的資訊
        /// </summary>
        public DebugDocument Document { get; set; }
        public EnvDTE.Breakpoint Information { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDebugBoundBreakpoint2 Breakpoint
        {
            get { return _breakpoint; }
            set
            {
                _breakpoint = value;
                IDebugPendingBreakpoint2 pendingBreakpoint;
                _breakpoint.GetPendingBreakpoint(out pendingBreakpoint);
                PendingBreakpoint = pendingBreakpoint;
                _check_bp_req();
            }
        }
        public IDebugBreakpointRequest3 BreakpointRequest
        {
            get
            {
                _check_bp_req();
                return _breakpointRequest3;
            }
        }

        public IDebugPendingBreakpoint2 PendingBreakpoint { get; private set; }
        public BP_RESOLUTION_INFO BreakpointInfo { get; set; }
        public BP_REQUEST_INFO2 RequestInfo
        {
            get
            {
                _check_bp_req();
                BP_REQUEST_INFO2[] req = new BP_REQUEST_INFO2[1];
                _breakpointRequest3.GetRequestInfo2((uint)enum_BPREQI_FIELDS.BPREQI_ALLFIELDS, req);
                return req[0];
            }
        }
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
        public string Name { get { return Marshal.PtrToStringBSTR(RequestInfo.bpLocation.unionmember3); } }
        public string FileName
        {
            get
            {
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
            get
            {
                EnvDTE90a.Breakpoint3 bp3 = Information as EnvDTE90a.Breakpoint3;
                return bp3.Condition;
            }
            set
            {
                _condition = value;
                if (_reset_condition_event == null)
                    _reset_condition_event += _reset_condition;
            }

        }
        private void _check_bp_req()
        {
            if (_breakpointRequest3 == null) _get_bp_req3();
        }
        private void _get_bp_req3()
        {
            PendingBreakpoint.GetBreakpointRequest(out _breakpointRequest);
            _breakpointRequest3 = _breakpointRequest as IDebugBreakpointRequest3;
        }
        private void _reset_condition(object sender, string value)
        {
            DebugBreakpoint bp = sender as DebugBreakpoint;
            bp._reset_condition_event -= _reset_condition;
            if (DebugScript.HasASyncScript())
                DebugScript.WaitSync();
            ResetBreakpointCondition reset = new ResetBreakpointCondition(this, value);
            reset.RunRules();
            bp._breakpoint = null;
        }
        private bool _first_break(DebugBreakpoint bpt)
        {
            EnvDTE.Breakpoints bps = dbg.VSDebugger.Breakpoints;
            string data = bpt.Name.Replace("0x", "").ToUpper();
            _set_as_tracepoint(bpt, bps, data);
            _get_debug_breakpoint_infos(bpt);
            //_set_information();
            return true;
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
        private void _set_as_tracepoint(DebugBreakpoint bpt, EnvDTE.Breakpoints bps, string data)
        {
            foreach (var bp_c in bps)
            {
                Breakpoint3 bp = bp_c as Breakpoint3;
                if (bp.Name.IndexOf(data) != -1)
                {
                    bp.BreakWhenHit = false;
                    bp.Message = bpt.Name;
                    Information = bp as EnvDTE.Breakpoint;
                }
            }
        }

    }
}
