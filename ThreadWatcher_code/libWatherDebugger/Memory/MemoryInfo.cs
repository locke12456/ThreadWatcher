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
        private IDebugThread2 Thread { get { return (Parent as MemoryInfo)._thread; } }
        private IDebuggerMemory Parent { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string ThreadInfo { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }
        public string Size { get; set; }
        private bool IsRoot {
            get {
                return Parent == this;
            }
        }
        private bool IsRootIsPointer
        {
            get
            {
                return IsRoot ? IsPointer : Parent.IsPointer;
            }
        }
        public bool IsPointer
        {
            get
            {
                return Type.IndexOf('*') != -1;
            }
        }
        public bool IsNullPointer
        {
            get
            {
                return IsPointer && (Address == "0xcccccc" || Address == "0x00000000");
            }
        }
        private string AddressQuery
        {
            get
            {
                MemoryInfo parent = Parent as MemoryInfo;
                string addrq = _addressQuery;
                return ( IsPointer ? ( IsRootIsPointer ? "" : "&" ) : "&") + addrq;
            }
        }
        private string _addressQuery
        {
            get {
                MemoryInfo parent = Parent as MemoryInfo;
                string addrq = "";
                addrq = parent != this ? parent._addressQuery + (parent.IsPointer?"->":".") : "";
                return addrq + Variable;
            }
        }
        public List<IDebuggerMemory> Members { get; private set; }

        public MemoryInfo()
        {
            _dte = Watcher.Debugger.Debugger.getInstance();
            Members = new List<IDebuggerMemory>();
            Parent = this;
        }
        public void Init(IDebugThread2 thread)
        {
            MemoryInfo parent = Parent as MemoryInfo;
            parent._thread = thread;
        }
        public void Add(IDebuggerMemory item)
        {
            MemoryInfo child = item as MemoryInfo;
            child.Parent = this;
            Members.Add(item);
        }
        public override string ToString()
        {
            return Variable + " { "+Value+" }";
        }
    }
}
