using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception.DebugItemFactory
{
    public abstract class DebugBreakpointFactoryException : ThreadDebuggerException
    {
        protected override string _type
        {
            get
            {
                return "DebugBreakpointFactory";
            }
        }
    }
    public class DebugBreakpointFactoryInitFail : DebugBreakpointFactoryException 
    {
        protected override string _message
        {
            get
            {
                return " DebugBreakpointFactory init fail . ";
            }
        }
    }
    public class DebugBreakpointFactoryCreateProductFail : DebugBreakpointFactoryException
    {
        protected override string _message
        {
            get
            {
                return " create DebugBreakpoint fail . ";
            }
        }
    }
}
