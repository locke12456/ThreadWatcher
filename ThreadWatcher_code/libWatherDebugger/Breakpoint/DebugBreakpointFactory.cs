using libWatherDebugger.DocumentContext;
using libWatherDebugger.Exception.DebugItemFactory;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using libWatherDebugger.Exception.DebugItem;

namespace libWatherDebugger.Breakpoint
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugBreakpointFactory : ItemFactory<DebugBreakpoint>
    {
        private IEnumDebugBoundBreakpoints2 _dbgbp;
        private IDebugCodeContext2 _codeContext;
        private DebugDocument _document;
        private IDebugBoundBreakpoint2[] _breakpoint;
        private IDebugBreakpointResolution2 _breakpointResolution;
        private BP_RESOLUTION_INFO[] _breakpointInfo;
        private IDebugBreakpointEvent2 _event;
        private string _filename = "";
        /// <summary>
        /// 建構子
        /// 初始化DebugBreakpoint
        /// </summary>
        /// <param name="Event">
        /// 來自Debuuger回傳的callback event
        /// </param>
        public DebugBreakpointFactory(IDebugBreakpointEvent2 Event)
        {
            _event = Event;
            int result = _initFactory();
            if (VSConstants.S_OK != result)
                throw (new DebugBreakpointFactoryInitFail());
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        protected override int _initFactory()
        {

            int result = VSConstants.S_FALSE;
            _productList = new List<DebugBreakpoint>();
            result = _getEnumDebugBoundBreakpoints(_event);
            return result;
        }
        protected override DebugBreakpoint _createProduct()
        {
            try
            {
                return _createBreakPoint();
            }
            catch (System.Exception fail)
            {
                Debug.WriteLine(fail.Message);
                throw (new DebugBreakpointFactoryCreateProductFail());
            }
        }
        /// <summary>
        /// 初始化 & 建立一個物件
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        public override int CreateProduct()
        {
            int result = _initBreakPoint();
            try
            {
                //result = _getNextDebugBoundBreakpoint();
                if (VSConstants.S_OK == result)
                {
                    _product = _createProduct();
                    if (_product != null) ProductList.Add(_product);
                }
            }
            catch (DebugBreakpointFactoryCreateProductFail fail)
            {
                throw (fail);
            }
            return result;
        }
        /// <summary>
        /// don't call this method
        /// Not Implemented
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public override int CreateProduct(object material)
        {
            throw (new NotImplementedException());
        }
        /// <summary>
        /// enum_BP_TYPE : 
        ///     BPT_NONE = 0,        
        ///     BPT_CODE = 1,
        ///     BPT_DATA = 2,
        ///     BPT_SPECIAL = 3,
        /// </summary>
        private enum_BP_TYPE BreakpointType
        {
            get
            {
                uint type;
                _breakpointResolution.GetBreakpointType(out type);
                return (enum_BP_TYPE)type;
            }
        }
        private string FileName
        {
            get
            {
                return _filename;
            }

        }
        /// <summary>
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        private int _initBreakPoint()
        {

            if (VSConstants.S_OK == _getNextDebugBoundBreakpoint())
            {
                if (VSConstants.S_OK == _getBreakpointResolution())
                    if (VSConstants.S_OK == _getResolutionInfo())
                    {
                        return _init_breakpoint_info();
                    }
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// 判斷 breakpoint type
        /// 若是程式碼中的中斷點，就嘗試去取得程式碼資料
        /// </summary>
        /// <returns> S_OK </returns>
        private int _init_breakpoint_info()
        {
            if (BreakpointType == enum_BP_TYPE.BPT_CODE)
            {
                if (VSConstants.S_OK == _getDocumentContext(_breakpointInfo[0]))
                    return VSConstants.S_OK;
            }
            return VSConstants.S_OK;
        }
        /// <summary>
        /// 初始化 DebugBreakpoint 物件
        /// </summary>
        /// <returns>
        /// DebugBreakpoint
        /// </returns>
        private DebugBreakpoint _createBreakPoint()
        {
            try
            {
                DebugBreakpoint bp = new DebugBreakpoint();
                bp.Breakpoint = _breakpoint[0];
                bp.BreakpointInfo = _breakpointInfo[0];
                bp.BreakpointType = BreakpointType;
                bp.Document = _document;
                return bp;
            }
            catch (DebugBreakpointException fail)
            {
                throw (fail);
            }
        }
        /// <summary>
        /// 透過 EnumBreakpoints 取得 IEnumDebugBoundBreakpoints2 資料
        /// </summary>
        /// <param name="Event">
        /// 來自Debuuger回傳的callback event
        /// </param>
        private int _getEnumDebugBoundBreakpoints(IDebugBreakpointEvent2 Event)
        {
            if (VSConstants.S_OK == Event.EnumBreakpoints(out _dbgbp))
            {
                _dbgbp.Reset();
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// 取得當前 breakpoint 的資訊
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        private int _getNextDebugBoundBreakpoint()
        {
            uint refrglt = 0;
            _breakpoint = new IDebugBoundBreakpoint2[1];
            if (VSConstants.S_OK == _dbgbp.Next(1, _breakpoint, ref refrglt))
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// get Breakpoint Resolution
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        private int _getBreakpointResolution()
        {
            if (VSConstants.S_OK == _breakpoint[0].GetBreakpointResolution(out _breakpointResolution))
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// get resolution info
        /// </summary>
        /// <returns> S_FALSE or S_OK </returns>
        private int _getResolutionInfo()
        {
            _breakpointInfo = new BP_RESOLUTION_INFO[1];

            if (VSConstants.S_OK == _breakpointResolution.GetResolutionInfo((uint)enum_BPRESI_FIELDS.BPRESI_ALLFIELDS, _breakpointInfo))
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// 初始化 DebugDocument 物件
        /// </summary>
        /// <param name="breakpointInfo">
        /// 初始化自 _getResolutionInfo
        /// </param>
        /// <returns> S_FALSE or S_OK </returns>
        private int _getDocumentContext(BP_RESOLUTION_INFO breakpointInfo)
        {
            try
            {
                _codeContext = (IDebugCodeContext2)Marshal.GetObjectForIUnknown(breakpointInfo.bpResLocation.unionmember1);
                DebugDocumentFactory factory = new DebugDocumentFactory(_codeContext);

                if (VSConstants.S_OK == factory.CreateProduct())
                {
                    _document = factory.Product as DebugDocument;
                    return VSConstants.S_OK;
                }
                return VSConstants.S_FALSE;
            }
            catch (DebugDocumentFactoryException fail)
            {
                throw (fail);
            }
        }


    }
}
