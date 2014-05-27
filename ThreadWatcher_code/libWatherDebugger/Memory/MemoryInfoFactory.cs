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
    public class MemoryInfoFactory : ItemFactory<MemoryInfo>
    {
        private bool _recursive = false;
        private DebugStackFrame _stack;
        private DEBUG_PROPERTY_INFO[] _info;
        private IEnumDebugPropertyInfo2 _propertyInfo;
        public MemoryInfoFactory(DebugStackFrame stack)
            : base()
        {
            _recursive = true;
            _stack = stack;
            _initParentInfo();
        }
        private MemoryInfoFactory(DEBUG_PROPERTY_INFO[] infos)
            : base()
        {
            _recursive = false;
            _info = infos;
            _initChildrenInfo();
        }
        public override int CreateProduct()
        {
            int result = base.CreateProduct();
            _buildAllProduct();
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
            _create_childern(meminfo);
            return meminfo;
        }

        private void _create_childern(MemoryInfo meminfo)
        {
            if (_recursive)
            {
                MemoryInfoFactory childs = new MemoryInfoFactory(_info);
                childs.CreateProduct();
                if (childs.ProductList.Count > 0)
                    meminfo.Members.AddRange(childs.ProductList);
            }
        }
        private void _buildAllProduct()
        {
            _productList = new List<MemoryInfo>();
            if (_propertyInfo == null) return;
            uint fetched = 0;
            while (_propertyInfo.Next(1, _info, out fetched) == 0)
            {
                _productList.Add(_createProduct());
            }
            if (_productList.Count > 0)
                _product = ProductList[0];
        }
    }
}
