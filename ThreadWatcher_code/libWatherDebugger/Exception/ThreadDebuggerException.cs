using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Exception
{
    public abstract class ThreadDebuggerException : System.Exception
    {
        protected virtual string _base {
            get {
                return "ERROR";
            }
        } 
        protected virtual string _type
        {
            get
            {
                return "";
            }
        }
        protected virtual string _message {
            get {
                return "";
            }
        }
        public override string Message
        {
            get
            {
                return "["+_base+"][" + _type + "][message]" + _message;
            }
        }
    }
}
