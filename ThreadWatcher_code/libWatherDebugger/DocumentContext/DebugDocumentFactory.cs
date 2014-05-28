using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.DocumentContext
{
    public class DebugDocumentFactory : ItemFactory<DebugDocument>
    {
        private IDebugCodeContext2 _codeContext;
        private IDebugDocument2 _document;
        private IDebugDocumentContext2 _documentContext;
        public DebugDocumentFactory(IDebugCodeContext2 codeContext) {
            _productList = new List<DebugDocument>();
            _codeContext = codeContext;
        }
        public override int CreateProduct()
        {
            int result = base.CreateProduct();
            _product = _createProduct();
            _productList.Add(_product);
            return result;
        }
        protected override int _initFactory()
        {
            if (VSConstants.S_OK == _codeContext.GetDocumentContext(out _documentContext))
            {
                _getDocument();
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        protected virtual DebugDocument _createProduct()
        {
            DebugDocument document = new DebugDocument();
            document.CodeContext = _codeContext;
            document.DocumentContext = _documentContext;
            document.Document = _document;
            return document;
        }
        private int _getDocument()
        {

            if (VSConstants.S_OK == _documentContext.GetDocument(out _document))
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
    }
}
