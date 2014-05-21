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
    public class DebugStackFrames : List<DebugStackFrame>
    {        
        
        public DebugStackFrames(DebugStackFrame current_stack) : base()
        {
            DebugStackFrameFactory factory = new DebugStackFrameFactory( current_stack.Thread );
            AddRange(factory.ProductList);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
