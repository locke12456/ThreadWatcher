using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Thread
{
    public class DebugThreadFactory : ItemFactory<DebugThread>
    {
        private IEnumDebugThreads2 _enumDebugThread;
        private IDebugProcess2 _debugProcess;
        public DebugThreadFactory(IDebugProcess2 process)
        {
            _debugProcess = process;
            _productList = new List<DebugThread>();
        }
        public DebugThreadFactory(IDebugThread2 thread)
        {
            _initMaterials(thread);
        }

        public override int CreateProduct()
        {
            return (_materials != null) ? _initProduct() : base.CreateProduct();
        }
        public override int CreateProduct(object material)
        {
            _productList = new List<DebugThread>();
            _initMaterials(material);
            return _initProduct();
        }
        private int _initProduct()
        {
            DebugThread item = _createProduct();
            uint id;
            item.Thread.GetThreadId(out id);
            item.ID = id.ToString();
            _productList.Add(item);
            _product = item;
            return item == null ? VSConstants.S_FALSE : VSConstants.S_OK;
        }
        protected DebugThread _createProduct()
        {
            DebugThread thread = new DebugThread();
            thread.Thread = _materials as IDebugThread2;
            return thread;
        }
        protected override int _initFactory()
        {
            int result = VSConstants.S_FALSE;
            if (VSConstants.S_OK == _getEnumDebugThread())
            {
                result = _getDebugThreads();
            }
            return result;
        }
        private int _getEnumDebugThread()
        {
            int result = VSConstants.S_FALSE;
            if (VSConstants.S_OK == _debugProcess.EnumThreads(out _enumDebugThread))
            {
                result = _enumDebugThread.Reset();
            }
            return result;
        }
        private int _getDebugThreads()
        {
            int result = VSConstants.S_FALSE;
            uint celt = 1;
            uint upie = 0;
            _productList = new List<DebugThread>();
            IDebugThread2[] thread = new IDebugThread2[1];
            while (VSConstants.S_OK == _enumDebugThread.Next(celt, thread, ref upie))
            {
                _initMaterials(thread[0]);
                _productList.Add(_createProduct());
            }
            result = (ProductList.Count != 0) ? VSConstants.S_OK : result;
            return result;
        }
    }
}
