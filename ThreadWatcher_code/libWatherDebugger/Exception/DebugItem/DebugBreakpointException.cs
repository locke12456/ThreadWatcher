using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception.DebugItem
{
    public class DebugBreakpointException : ThreadDebuggerException
    {
        protected override string _type
        {
            get
            {
                return "DebugBreakpoint";
            }
        }
    }
}
