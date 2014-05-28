using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.Stack
{
    public class DebugStack : DebugScript
    {
        public IDebugThread2 Thread
        {
            get;
            set;
        }
        public List<DebugStackFrame> StackList
        {
            get;
            private set;
        }
        public DebugStackFrameFactory StackFrame
        {
            get;
            protected set;
        }
        public DebugStack() : base() {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        public DebugStack(EnvDTE.Debugger dbg) : base(dbg) {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            DebugStackFrameFactory factory = new DebugStackFrameFactory(Thread);
            int result = factory.CreateProduct();
            if (VSConstants.S_OK == result)
            {
                StackList = factory.ProductList;
                StackFrame = factory;
            }
            return VSConstants.S_OK == result;
        }
    }
}
