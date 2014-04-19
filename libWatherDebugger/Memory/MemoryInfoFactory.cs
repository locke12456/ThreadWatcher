using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Memory
{
    public class MemoryInfoFactory : DebugItemFactory
    {
        private DebugStackFrame _stack;
        private DEBUG_PROPERTY_INFO[] _info;
        private IEnumDebugPropertyInfo2 _propertyInfo;
        public MemoryInfoFactory(DebugStackFrame stack)
            : base()
        {
            _stack = stack;
            _initParentInfo();
        }
        //protected MemoryInfoFactory(DEBUG_PROPERTY_INFO[] infos)
        //    : base()
        //{
        //    _info = infos;
        //    _initChildrenInfo();
        //    //_info[0].pProperty
        //}
        public override int CreateProduct()
        {
            int result = base.CreateProduct();
            _buildAllProduct();
            _product = ProductList[0];
            return result;
        }
        protected override int _initFactory()
        {
            return VSConstants.S_OK;
        }
        private void _initParentInfo()
        {
            uint fetched = 0;
            Guid FilterLocalsGuid = ThreadWatcher.GUIDs.FilterAllLocalsGuid;
            if (VSConstants.S_OK == _stack.Stack.EnumProperties((uint)enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_ALL, 10, ref FilterLocalsGuid, 0, out fetched, out _propertyInfo))
            {
                _info = new DEBUG_PROPERTY_INFO[1];
                _propertyInfo.Reset();
            }
        }
        private void _initChildrenInfo()
        {
            //DEBUG_PROPERTY_INFO info = _info[0];
            Guid FilterLocalsGuid = ThreadWatcher.GUIDs.FilterAllLocalsGuid;
            if (VSConstants.S_OK == _info[0].pProperty.EnumChildren((uint)enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_ALL, 10, ref FilterLocalsGuid, 0x00000000ffffffff, _info[0].bstrName, 0, out _propertyInfo))
            {
                _propertyInfo.Reset();
            }
        }
        private MemoryInfo _createProduct()
        {
            MemoryInfo meminfo = new MemoryInfo();
            meminfo.Variable = _info[0].bstrName;
            meminfo.Value = _info[0].bstrValue;
            meminfo.Type = _info[0].bstrType;
            return meminfo;
        }
        private void _buildAllProduct()
        {
            _productList = new List<IDebugItem>();
            uint fetched = 0;
            while (_propertyInfo.Next(1, _info, out fetched) == 0)
            {
                _productList.Add(_createProduct());
                Debug.WriteLine(_info[0].bstrName + " : " + _info[0].bstrValue + "(" + _info[0].bstrType + ")");
            }
        }
    }
}
