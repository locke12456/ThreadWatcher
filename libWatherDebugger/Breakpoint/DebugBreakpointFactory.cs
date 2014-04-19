using libWatherDebugger.DocumentContext;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Breakpoint
{
    public class DebugBreakpointFactory : DebugItemFactory
    {
        private IEnumDebugBoundBreakpoints2 _dbgbp;
        private IDebugCodeContext2 _codeContext;
        private DebugDocument _document;
        private IDebugBoundBreakpoint2[] _breakpoint;
        private IDebugBreakpointResolution2 _breakpointResolution;
        private BP_RESOLUTION_INFO[] _breakpointInfo;
        private IDebugBreakpointEvent2 _event;
        private string _filename = "";
        public DebugBreakpointFactory(IDebugBreakpointEvent2 Event)
        {
           
            _event = Event;
            int result = _initFactory();
            if (VSConstants.S_OK != result)
                throw (new Exception("fail"));
        }
        protected override int _initFactory()
        {
            int result = VSConstants.S_FALSE;
            _productList = new List<IDebugItem>();
            result = _getEnumDebugBoundBreakpoints(_event);
            return result;
        }
        protected override IDebugItem _createProduct()
        {
            return _createBreakPoint();
        }
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
            catch (Exception fail)
            {
                throw (fail);
            }
            return result;
        }
        public override int CreateProduct(object material)
        {
            return CreateProduct();
        }

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
        private int _initBreakPoint()
        {

            if (VSConstants.S_OK == _getNextDebugBoundBreakpoint())
            {
                if (VSConstants.S_OK == _getBreakpointResolution())
                    if (VSConstants.S_OK == _getResolutionInfo())
                    {
                        if (BreakpointType == enum_BP_TYPE.BPT_CODE)
                        {
                            if (VSConstants.S_OK == _getDocumentContext(_breakpointInfo[0]))
                                return VSConstants.S_OK;
                        }
                        return VSConstants.S_OK;
                    }
            }
            return VSConstants.S_FALSE;
        }
        private DebugBreakpoint _createBreakPoint()
        {
            DebugBreakpoint bp = new DebugBreakpoint();
            bp.Breakpoint = _breakpoint[0];
            bp.BreakpointInfo = _breakpointInfo[0];
            bp.BreakpointType = BreakpointType;
            bp.Document = _document;
            return bp;
        }
        private int _getEnumDebugBoundBreakpoints(IDebugBreakpointEvent2 Event)
        {
            if (VSConstants.S_OK == Event.EnumBreakpoints(out _dbgbp))
            {
                _dbgbp.Reset();
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
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
        private int _getBreakpointResolution()
        {
            if (VSConstants.S_OK == _breakpoint[0].GetBreakpointResolution(out _breakpointResolution))
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        private int _getResolutionInfo()
        {
            _breakpointInfo = new BP_RESOLUTION_INFO[1];
            if (VSConstants.S_OK == _breakpointResolution.GetResolutionInfo((uint)enum_BPRESI_FIELDS.BPRESI_ALLFIELDS, _breakpointInfo))
            {

                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        private int _getDocumentContext(BP_RESOLUTION_INFO breakpointInfo)
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
        

    }
}
