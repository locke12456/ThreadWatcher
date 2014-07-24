using libWatherDebugger.DocumentContext;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio.Debugger.Interop;

namespace libWatherDebugger.Thread
{
    /// <summary>
    /// 利用DebugThread,可對單個執行緒中所有可查詢的資料作查詢
    /// </summary>
    public class DebugThread : IDebugItem
    {
        public IDebugThread2 Thread { get; set; }
        public DebugStackFrame Stack { get; set; }

        public DebugDocument Document
        {
            get
            {
                return Stack.Document;
            }
        }
        public string ID
        {
            get;
            set;
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
