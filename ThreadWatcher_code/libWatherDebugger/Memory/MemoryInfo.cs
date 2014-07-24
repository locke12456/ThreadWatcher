using libUtilities;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Memory
{
    public class MemoryInfo : IDebuggerMemory, IDebugItem
    {
        private IDebugThread2 _thread;
        private Watcher.Debugger.Debugger _dte;
        private IDebugThread2 Thread { get { return Parent._thread; } }
        private MemoryInfo Parent { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string ThreadInfo { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }
        public string Size { get; set; }
        /// <summary>
        /// 是否為結構最頂端
        /// </summary>
        private bool IsRoot
        {
            get
            {
                return Parent == this;
            }
        }
        /// <summary>
        /// 最頂端是否為 指標
        /// </summary>
        private bool IsRootIsPointer
        {
            get
            {
                return IsRoot ? IsPointer : Parent.IsPointer;
            }
        }
        protected bool IsPointer
        {
            get
            {
                return Type.IndexOf('*') != -1;
            }
        }

        private bool _isNullPointer
        {
            get
            {
                try
                {
                    return IsPointer && (_is_null_pointer(Value) && _is_null_pointer(Address));
                }
                catch (System.Exception fail)
                {
                    Debug.WriteLine(fail.Message);
                    return true;
                }
            }
        }

        private bool _is_null_pointer(string Address)
        {
            return Address != null && (Address.IndexOf("0xcccccccc") > 0 /*|| Address.IndexOf("0xcdcdcdcd") > 0*/ || Address.IndexOf("0x00000000") > 0);
        }
        /// <summary>
        /// 可以向debugger查詢address的字串
        /// 判斷自己是不是一個pointer , 若不是的話則加『&』在最前面
        /// </summary>
        public string AddressQuery
        {
            get
            {
                try
                {
                    MemoryInfo parent = Parent as MemoryInfo;
                    string addrq = _addressQuery;
                    return (IsPointer ? (IsRootIsPointer ? "" : "&") : "&") + addrq;
                }
                catch (System.Exception fail)
                {

                    Debug.WriteLine(fail.Message);
                    return "";
                }
            }
        }
        /// <summary>
        /// 可以向debugger查詢address的字串
        /// e.g.
        ///     a是父項目 , a 是 pointer
        ///     則查尋字串為  a->b
        /// </summary>
        private string _addressQuery
        {
            get
            {
                MemoryInfo parent = Parent as MemoryInfo;
                string addrq = "";
                addrq = parent != this ? parent._addressQuery + (parent.IsPointer ? "->" : ".") : "";
                return addrq + Variable;
            }
        }
        public List<IDebuggerMemory> Members { get; set; }

        public MemoryInfo()
        {
            _dte = Watcher.Debugger.Debugger.getInstance();
            Members = new List<IDebuggerMemory>();
            Parent = this;
        }
        public bool IsNullPointer() {
            return _isNullPointer;
        }
        public void Init(IDebugThread2 thread)
        {
            MemoryInfo parent = Parent as MemoryInfo;
            parent._thread = thread;
        }
        /// <summary>
        /// 加入子項目
        /// e.g. 
        /// ----  debuggee  ----------------
        ///     struct a {
        ///         int b,c;
        ///     };
        /// --------------------------------    
        /// ---- memory info factory ------------
        ///     mem.Add( b ); 
        ///     mem.Add( c ); 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IDebuggerMemory item)
        {
            MemoryInfo child = item as MemoryInfo;
            child.Parent = this;
            Members.Add(item);
        }
        public override string ToString()
        {
            return Variable + " { " + Value + " }";
        }
    }
}
