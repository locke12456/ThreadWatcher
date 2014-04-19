using libWatherDebugger.DocumentContext;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Thread
{
    public class DebugThread : IDebugItem
    {
        public IDebugThread2 Thread { get; set; }
        public DebugDocument Document
        {
            get
            {
                DebugStackFrameFactory factory = new DebugStackFrameFactory(Thread);
                factory.CreateProduct();
                DebugStackFrame stack = factory.Product as DebugStackFrame;
                return stack.Document;
            }
        }
        public string ID
        {
            get
            {
                uint id;
                Thread.GetThreadId(out id);
                return id.ToString();
            }
        }
        public string Name
        {
            get;
            set;
        }
        public DebugThread()
        {

        }
        public override string ToString()
        {
            return Name + " { id: " + ID + "}";
        }
        public override bool Equals(object obj)
        {
            return Thread == obj;
        }
    }
}
