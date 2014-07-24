using libWatherDebugger.DocumentContext;
using libWatherDebugger.Memory;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Stack
{
    /// <summary>
    /// DebugThread 需要使用 DebugStackFrame內的 property 來做為查詢和建立物件 
    /// </summary>
    public class DebugStackFrame : IDebugItem
    {
        private DebugDocument _doucment;
        public DebugDocument Document {
            get {
                if (_doucment != null) return _doucment;
                _doucment = _init_document();
                return _doucment;
            }
        }
        public IDebugThread2 Thread
        {
            get;
            set;
        }
        public IDebugStackFrame2 Stack
        {
            get;
            set;
        }
        public IDebugCodeContext2 CodeContext
        {
            get;
            set;
        }
        public string FunctionName
        {
            get
            {
                string name;
                Stack.GetName(out name);
                return name;
            }
        }
        public DebugStackFrame()
        {

        }
        public List<MemoryInfo> GetAllLocalVariable() 
        {
            List<MemoryInfo> locals = new List<MemoryInfo>();
            return locals;
        }
        private DebugDocument _init_document() 
        {
            if (CodeContext == null) return null;
            DebugDocumentFactory factory = new DebugDocumentFactory(CodeContext);
            factory.CreateProduct();
            return factory.Product as DebugDocument;
        }
    }
}
