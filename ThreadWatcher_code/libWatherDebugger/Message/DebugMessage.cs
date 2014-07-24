using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Message
{
    /// <summary>
    /// 讀取來自trace ponit的訊息
    /// </summary>
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
                _init_msg();
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
        private void _init_msg()
        {

            string msg;
            _event.GetMessage(out MessageType, out msg, out TypeId, out HelpFileFame, out HelpId);
            Message = msg;
        }
    }
}
