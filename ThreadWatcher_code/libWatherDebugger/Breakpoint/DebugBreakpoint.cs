using EnvDTE90a;
using libWatcher.Constants;
using libWatherDebugger.DocumentContext;
using libWatherDebugger.Exception.DebugItem;
using libWatherDebugger.GeneralRules.Mode.BreakPoint;
using libWatherDebugger.Script;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
        private Watcher.Debugger.Debugger dbg = Watcher.Debugger.Debugger.getInstance();
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
        /// get or set
        /// set : 
        ///     設定一個 IDebugBoundBreakpoint2 並_get_pending_breakpoint method 嘗試初始化 IDebugPendingBreakpoint2
        /// </summary>
        public IDebugBoundBreakpoint2 Breakpoint
        {
            get { return _breakpoint; }
            set
            {
                _breakpoint = value;
                _try_get_pending_breakpoint();
            }
        }
        /// <summary>
        /// get :
        ///      precondition : 必須先設定 IDebugBoundBreakpoint2 
        ///      
        /// </summary>
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
        /// <summary>
        /// get :
        ///      precondition : 必須先設定 IDebugBoundBreakpoint2 
        ///    取得 BreakpointRequest3 中的所有欄位與資訊 
        /// </summary>
        public BP_REQUEST_INFO2 RequestInfo
        {
            get
            {
                _check_bp_req();
                return _try_get_breakpoint_request();
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
        /// <summary>
        /// 
        /// </summary>
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
                return Information.FunctionName;
            }
        }
        /// <summary>
        /// 設定 watchpoint 的 condition 必須等到程式中斷，因此這邊是註冊一個事件，等到debugger中斷時會重設condition
        /// </summary>
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
        /// <summary>
        /// 嘗試取得 breakpoint 中斷的資訊
        /// </summary>
        /// <returns></returns>
        private BP_REQUEST_INFO2 _try_get_breakpoint_request()
        {
            BP_REQUEST_INFO2[] req = new BP_REQUEST_INFO2[1];
            try
            {
                if( VSConstants.S_FALSE == _breakpointRequest3.GetRequestInfo2((uint)enum_BPREQI_FIELDS.BPREQI_ALLFIELDS, req))
                    throw (new GetBreakpointRequestInfoFail());
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw (exception);
            }
            return req[0];
        }
        /// <summary>
        /// 嘗試取得一個 IDebugPendingBreakpoint2 物件
        /// </summary>
        private void _try_get_pending_breakpoint()
        {
            try
            {
                IDebugPendingBreakpoint2 pendingBreakpoint;
                _breakpoint.GetPendingBreakpoint(out pendingBreakpoint);
                PendingBreakpoint = pendingBreakpoint;
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw (new GetPendingBreakpointFail());
            }
            finally
            {
                _check_bp_req();
            }
        }
        /// <summary>
        /// 若 _breakpointRequest3 為 null 時，呼叫 _try_get_bp_req3 method
        /// </summary>
        private void _check_bp_req()
        {
            if (_breakpointRequest3 == null) _try_get_bp_req3();
        }
        /// <summary>
        /// 嘗試取得一個 IDebugBreakpointRequest3 物件
        /// </summary>
        private void _try_get_bp_req3()
        {
            try
            {
                PendingBreakpoint.GetBreakpointRequest(out _breakpointRequest);
                _breakpointRequest3 = _breakpointRequest as IDebugBreakpointRequest3;
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw (new GetBreakpointRequestFail());
            }
        }
        /// <summary>
        /// 用於重新設定 condition
        /// 當中斷點觸發時，會嘗試觸發這個事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void _reset_condition(object sender, string value)
        {
            DebugBreakpoint bp = sender as DebugBreakpoint;
            bp._reset_condition_event -= _reset_condition;
            //若有多執行緒使用時，會在此等待同步
            if (DebugScript.HasASyncScript())
                DebugScript.WaitSync();
            //開啟另外的執行緒，重新設定中斷點的condition 
            ResetBreakpointCondition reset = new ResetBreakpointCondition(this, value);
            //ResetBreakpointCondition 是一個 繼承自 WatcherRule 的 class , 詳細流程參閱 WatcherRule 
            reset.RunRules();
            bp._breakpoint = null;
        }
        /// <summary>
        /// watchpoint 在設定時，無法設定成trace point
        /// 所以當第一次被中斷時，會嘗試將被hit到的watchpoint，設成trace point
        /// </summary>
        /// <param name="bpt"></param>
        /// <returns></returns>
        private bool _first_break(DebugBreakpoint bpt)
        {
            EnvDTE.Breakpoints bps = dbg.VSDebugger.Breakpoints;
            string data = bpt.Name.Replace("0x", "").ToUpper();
            _set_as_tracepoint(bpt, bps, data);
            _get_debug_breakpoint_infos(bpt);
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
        /// <summary>
        /// 將被hit到的中斷點設成trace point
        /// </summary>
        /// <param name="bpt">hit到的中斷點</param>
        /// <param name="bps">當前所有的中斷點</param>
        /// <param name="data">watchpoint的記憶體位置</param>
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
