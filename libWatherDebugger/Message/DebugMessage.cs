using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Message
{
    public class DebugMessage : IDebugItem
    {
        private IDebugMessageEvent2 _event;
        public IDebugMessageEvent2 Event
        {
            get
            {
                return _event;
            }
            set {
                _event = value;
                string msg;
                _event.GetMessage(out MessageType, out msg, out TypeId, out HelpFileFame, out HelpId);
                Message = msg;
            }
        }
        public uint MessageType;
        public uint TypeId;
        public uint HelpId;
        public string HelpFileFame;
        public string Message { get;private set; }
        public DebugMessage()
        {

        }
    }
}
