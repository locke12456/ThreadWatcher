using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger
{
    public abstract class ItemFactory<T> : IDebugItemFactory<T> 
        where T : class
    {
        protected static readonly int Success = VSConstants.S_OK;
        protected static readonly int Fail = VSConstants.S_FALSE;

        protected Watcher.Debugger.Debugger _dbg = Watcher.Debugger.Debugger.getInstance();
        protected Object _materials;
        protected T _product;
        protected List<T> _productList;
        public virtual T Product
        {
            get
            {
                return _product;
            }
        }
        public virtual List<T> ProductList
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
        public virtual int CreateProduct(Object material)
        {
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
        protected virtual T _createProduct()
        {
            return null;
        }
    }
}
