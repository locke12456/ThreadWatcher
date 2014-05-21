
using libWatherDebugger;
using libWatherDebugger.Memory;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.PropertyItem.Logger
{
    public class ThreadLog : Log
    {
        private static readonly uint MessageMaxLength = 46;
        public DebugStackFrames StackList { get; private set; }
        public DebugStackFrame Stack { get; private set; }
        public List<MemoryInfo> Locals
        {
            get
            {
                return _locals;
            }
        }
        private List<MemoryInfo> _locals;
        public ThreadLog()
        {
            CreatedTimeTick = System.DateTime.Now.Ticks;
            Debugger dbg = Debugger.getInstance();
            Stack = dbg.CurrentStackFrame as DebugStackFrame;

            _init();
            _setMessage();
        }
        private void _init()
        {
            MemoryInfoFactory factory = new MemoryInfoFactory(Stack);
            factory.CreateProduct();
            _locals = factory.ProductList;
        }
        private void _setMessage()
        {
            string msg = " ";
            foreach (var item in _locals.Select((val2, idx2) => new { Index = idx2, Value = val2 as MemoryInfo }))
            {
                char[] array = item.Value.ToString().ToArray();
                foreach( char char_item in array)
                    msg = (msg.Length <= MessageMaxLength) ? msg + char_item : msg;
                if (msg.Length > MessageMaxLength)
                {
                    msg += " ...";
                    break;
                }
            }
            Message = msg;
        }
    }
}
