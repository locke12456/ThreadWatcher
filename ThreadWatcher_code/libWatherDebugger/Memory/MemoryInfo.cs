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
        private bool IsRoot
        {
            get
            {
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
                catch (Exception fail)
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
                catch (Exception fail)
                {

                    Debug.WriteLine(fail.Message);
                    return "";
                }
            }
        }
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
