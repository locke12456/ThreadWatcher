using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception.DebugItem
{
    public abstract class DebugDocumentException : ThreadDebuggerException
    {
        protected override string _type
        {
            get
            {
                return "DebugDocument";
            }
        }
    }
    public class GetCodeContextInfoFail : DebugDocumentException
    {
        protected override string _message
        {
            get
            {
                return "get code information fail .";
            }
        }
    }
}
