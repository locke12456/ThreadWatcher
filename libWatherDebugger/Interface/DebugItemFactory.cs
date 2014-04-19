using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger
{
    public abstract class DebugItemFactory : IDebugItemFactory
    {
        protected Watcher.Debugger.Debugger _dbg = Watcher.Debugger.Debugger.getInstance();
        protected Object _materials;
        protected IDebugItem _product;
        protected List<IDebugItem> _productList;
        public virtual IDebugItem Product
        {
            get
            {
                return _product;
            }
        }
        public virtual List<IDebugItem> ProductList
        {
            get
            {
                return _productList;
            }
        }
        public virtual int CreateProduct()
        {
            if (VSConstants.S_OK != _initFactory())
            {
                return VSConstants.S_FALSE;
            }
            return VSConstants.S_OK;
        }
        public virtual int CreateProduct(Object material) {
            _initMaterials(material);
            return CreateProduct();
        }
        protected virtual int _initFactory()
        {
            return VSConstants.E_NOTIMPL;
        }
        protected virtual void _initMaterials(Object obj) 
        {
            _materials = obj;
        }
        protected virtual IDebugItem _createProduct()
        {
            return null;
        }
    }
}
