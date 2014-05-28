using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.ScriptEvent
{
    public interface IEventArgs
    {
        string Event
        {
            get;
        }
    }

    public class ScriptEventArgs
    {
        public ScriptEventArgs() { }
        public string Event
        {
            get;
            protected set;
        }
    }
    public class CompleteEventArgs : ScriptEventArgs
    {
        public CompleteEventArgs()
            : base()
        {
            Event = "Complete";
        }
    }
    public class PendingEventArgs : ScriptEventArgs
    {
        public PendingEventArgs()
            : base()
        {
            Event = "Pending";
        }
    }
}
