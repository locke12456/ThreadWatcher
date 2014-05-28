using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.Interface
{
    interface IDebugBreakPoint
    {
        string File
        {
            get;
            set;
        }
        string Function
        {
            get;
            set;
        }
        int Line
        {
            get;
            set;
        }
        int Column
        {
            get;
            set;
        }
        string Condition
        {
            get;
            set;
        }
        EnvDTE.dbgBreakpointConditionType ConditionType
        {
            get;
            set;
        }
    }
}
