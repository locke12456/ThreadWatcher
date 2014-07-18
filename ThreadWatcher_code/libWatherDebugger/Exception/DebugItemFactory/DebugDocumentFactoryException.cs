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
    public class CreateDebugDocumentFail : DebugDocumentFactoryException 
    {
        protected override string _message
        {
            get
            {
                return "create debug document fail .";
            }
        }
    }
    public class GetDocumentContextFail : DebugDocumentFactoryException 
    {
        protected override string _message
        {
            get
            {
                return "get DocumentContext fail .";
            }
        }
    }
    public class GetDocumentFail : DebugDocumentFactoryException
    {
        protected override string _message
        {
            get
            {
                return "get Document fail .";
            }
        }
    }
}
