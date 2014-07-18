using libWatherDebugger.Exception.DebugItemFactory;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <summary>
        /// 初始化 DebugDocument 物件
        /// </summary>
        /// <param name="codeContext"></param>
        public DebugDocumentFactory(IDebugCodeContext2 codeContext) {
            _productList = new List<DebugDocument>();
            _codeContext = codeContext;
        }
        /// <summary>
        /// create DebugDocument object
        /// </summary>
        /// <returns></returns>
        public override int CreateProduct()
        {
            int result = base.CreateProduct();
            try
            {
                _product = _createProduct();
            }
            catch (System.Exception expception) 
            {
                Debug.WriteLine(expception.Message);
                throw (new CreateDebugDocumentFail());
            }
            finally
            {
                _productList.Add(_product);
            }
            return result;
        }
        /// <summary>
        /// try to get DocumentContext & Doucment
        /// </summary>
        /// <returns> S_OK or S_FALSE </returns>
        protected override int _initFactory()
        {
            try
            {
                if (VSConstants.S_OK == _codeContext.GetDocumentContext(out _documentContext))
                {
                    _getDocument();
                    return VSConstants.S_OK;
                }
            }
            catch (System.Exception exception) {
                Debug.WriteLine(exception.Message);
                throw (new GetDocumentContextFail());
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// create a DebugDocument object
        /// </summary>
        /// <returns></returns>
        protected virtual DebugDocument _createProduct()
        {
            DebugDocument document = new DebugDocument();
            document.CodeContext = _codeContext;
            document.DocumentContext = _documentContext;
            document.Document = _document;
            return document;
        }
        /// <summary>
        /// try to get a Document
        /// </summary>
        /// <returns> S_OK or S_FALSE </returns>
        private int _getDocument()
        {
            try
            {
                if (VSConstants.S_OK == _documentContext.GetDocument(out _document))
                {
                    return VSConstants.S_OK;
                }
            }
            catch (System.Exception exception) 
            {
                Debug.WriteLine(exception.Message);
                throw (new GetDocumentFail());
            }
            return VSConstants.S_FALSE;
        }
    }
}
