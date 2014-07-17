using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception.DebugItem
{
    public abstract class DebugBreakpointException : ThreadDebuggerException
    {
        protected override string _type
        {
            get
            {
                return "DebugBreakpoint";
            }
        }
    }
    public class GetBreakpointRequestFail : DebugBreakpointException 
    {
        protected override string _message
        {
            get
            {
                return "need to setting a Breakpoint in this object .";
            }
        }
    }
    public class GetBreakpointRequestInfoFail : DebugBreakpointException
    {
        protected override string _message
        {
            get
            {
                return "try get breakpoint request information fail .";
            }
        }
    }
    public class GetPendingBreakpointFail : DebugBreakpointException 
    {
        protected override string _message
        {
            get
            {
                return "try get a pending breakpoint fail .";
            }
        }
    }
}
