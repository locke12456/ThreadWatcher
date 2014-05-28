using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Message
{
    public class DebugMessageFactory : ItemFactory<DebugMessage>
    {
        public DebugMessageFactory(IDebugMessageEvent2 Event)
        {
            _productList = new List<DebugMessage>();
            _initMaterials(Event);
        }
        public override int CreateProduct()
        {
            _product = _createProduct();
            _productList.Add(_product);
            return VSConstants.S_OK;
        }
        public override int CreateProduct(object material)
        {
            return base.CreateProduct(material);
        }
        protected override int _initFactory()
        {
            return VSConstants.S_OK;
        }
        protected DebugMessage _createProduct()
        {
            DebugMessage item = new DebugMessage();
            if (_materials != null)
                item.Event = _materials as IDebugMessageEvent2;
            return item;
        }

    }
}
