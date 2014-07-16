using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception.DebugItemFactory
{
    public abstract class DebugDocumentFactoryException : ThreadDebuggerException
    {
        protected override string _type
        {
            get
            {
                return "DebugDocumentFactory";
            }
        }
    }
}
