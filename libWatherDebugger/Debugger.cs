using EnvDTE;
using EnvDTE80;
using EnvDTE90a;
using libUtilities;
using libWatherDebugger;
using libWatherDebugger.Memory;
using libWatherDebugger.Property;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Watcher.Debugger
{
    public class Debugger
    {
        private static Debugger _this;
        private static AutoResetEvent sync;
        private EnvDTE.Debugger _that;
        private IDebugThread2 _thread;
        private DebugThread _debugthread;
        private List<IDebugItem> _stackFrame = null;
        private Dictionary<string, IDebuggerMemory> _queryList = new Dictionary<string, IDebuggerMemory>();
        private IDebugItem _currentStackFrame = null;
        private IDebugProperty2 _debugProperty = null;

        public IDebugItem CurrentStackFrame
        {
            get { return _stackFrame[0]; }
        }
        public IDebugItem CurrentThread
        {
            get {
                return _debugthread;
            }
        }
        public List<IDebugItem> StackList
        {
            get { return _stackFrame; }
        }
        public bool IsInited
        {
            get;
            private set;
        }
        public static Debugger getInstance()
        {
            if (_this == null) _this = new Debugger();
            return _this;
        }
        public void Init(EnvDTE.Debugger that)
        {
            VSDebugger = that;
        }

        public EnvDTE.Debugger VSDebugger
        {
            get
            {
                return _that;
            }
            private set
            {
                _that = value;
            }
        }
        private EnvDTE90a.StackFrame2 StackFrame2 
        {
            get { return VSDebugger.CurrentStackFrame as EnvDTE90a.StackFrame2; }
        }
        public string FileName
        {
            get
            {
                return VSDebugger.CurrentStackFrame != null ? StackFrame2.FileName : "";
            }
        }
        public string FunctionName
        {
            get
            {
                return VSDebugger.CurrentStackFrame != null ? StackFrame2.FunctionName : "";
            }
        }
        public uint LineNumber
        {
            get
            {
                return VSDebugger.CurrentStackFrame != null ? StackFrame2.LineNumber : 0;
            }
        }
        public void DebugScript(List<Func<bool>> list)
        {
            System.Threading.Thread _continue = new System.Threading.Thread(
            (object var) =>
            {
                if (sync == null) sync = new AutoResetEvent(false);
                else sync.WaitOne();
                bool Break = false;
                List<Func<bool>> script_list = var as List<Func<bool>>;
                int index = 0;
                while (!Break)
                {
                    Func<bool> step = script_list[index];
                    if (step())
                    {
                        Break = script_list[++index] == null;
                    }
                }
                sync.Set();
            });
            _continue.Start(list);
        }
        public int InitStackFrame(IDebugThread2 thread)
        {
            _thread = thread;
            _debugthread = new DebugThread();
            _debugthread.Thread = _thread;
            DebugStackFrameFactory factory = new DebugStackFrameFactory(_thread);
            int result = factory.CreateProduct();
            if (VSConstants.S_OK == result) 
            {
                _stackFrame = factory.ProductList;
            }
            // Get the current frame.
            return result;
        }
        
        public IDebuggerMemory Query(string name, IDebugThread2 thread)
        {
            if (VSConstants.S_OK == InitStackFrame(thread))
            {
                IDebuggerMemory info = _buildMemoryInfo(name);
                return info;
            }
            return null;
        }
        public IDebuggerMemory Query(string name)
        {

            EnvDTE.Expression variable = _that.GetExpression(name, true, 100);
            MemoryInfo info = new MemoryInfo();
            info = _buildMemoryInfo(variable, info) as MemoryInfo;
            return info;
        }
        public Dictionary<string, IDebuggerMemory> Locals(DebugStackFrame stack)
        {
            MemoryInfoFactory factory = new MemoryInfoFactory(stack);
            factory.CreateProduct();
            Dictionary<string, IDebuggerMemory> keyValue = new Dictionary<string, IDebuggerMemory>();
            foreach (MemoryInfo mem in factory.ProductList) 
            {
                keyValue.Add(mem.Variable, mem);
            }
            return keyValue;
        }
        public IDebuggerMemory Update(MemoryInfo info)
        {

            EnvDTE.Expression variable = _that.GetExpression(info.Address, true, 100);
            //MemoryInfo info = new MemoryInfo(this);
            //info = _buildMemoryInfo(variable, info) as MemoryInfo;
            return info;
        }
        private IDebuggerMemory _buildMemoryInfo(EnvDTE.Expression variable, MemoryInfo info)
        {
            info.Variable = variable.Name;
            info.Value = variable.Value;
            info.Type = variable.Type;
            info.Address = _tryGetAddress(info.AddressQuery);
            if (info.IsNullPointer) return info;
            foreach (Expression sub in variable.DataMembers)
            {
                MemoryInfo child = new MemoryInfo();
                info.Add(child);
                child = _buildMemoryInfo(sub, child) as MemoryInfo;
            }
            return info;
        }
        private Debugger()
        {
            IsInited = false;
        }
        private Debugger(DTE2 application)
        {
            _that = application.Debugger;
            IsInited = false;
        }
        private string _tryGetAddress(string queryString)
        {
            string addr = "";
            EnvDTE.Expression variable = _that.GetExpression(queryString, true, 100);

            addr = variable.Value.Split(new char[] { ' ' })[0];
            Debug.WriteLine(queryString + " : " + addr);
            return addr;
        }
        private IDebuggerMemory _buildMemoryInfo(string addressExpressionString)
        {
            MemoryInfo _info = null;

            DebugPropertyFactory factory = new DebugPropertyFactory(CurrentStackFrame as IDebugStackFrame2);

            if(VSConstants.S_OK != factory.CreateProduct(addressExpressionString)) return _info;

            DebugProperty property = factory.ProductList[0] as DebugProperty;

            property.DataSize = 4;

            byte[] data = property.GetData();// = _getMemoryData(debugProperty);

            if (data != null)
            {
                // Read successful.
                _info = new MemoryInfo();
                _info.Type = "adderss";
                _info.Address = addressExpressionString;
                _info.Variable = addressExpressionString;
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(data);
                _info.Value = Convert.ToString(BitConverter.ToInt32(data, 0));
            }
            return _info;
        }
        //public IDebugProperty2 GetDebugProperty(string address)
        //{
        //    //if (VSConstants.S_OK != InitStackFrame(_thread)) {
        //    //    return null;
        //    //}
        //    // Get a context for evaluating expressions.
        //    IDebugExpressionContext2 expressionContext;
        //    if (VSConstants.S_OK != _getDebugExpressionContex(out expressionContext))
        //    {
        //        return null;
        //    }

        //    // Parse the expression string.
        //    IDebugExpression2 expression;
        //    string error;
        //    uint errorCharIndex;

        //    if (VSConstants.S_OK != expressionContext.ParseText(address,
        //        (uint)enum_PARSEFLAGS.PARSE_EXPRESSION, 10, out expression, out error, out errorCharIndex))
        //    {
        //        Debug.WriteLine("Failed to parse expression.");
        //        return null;
        //    }

        //    // Evaluate the parsed expression.
        //    IDebugProperty2 DebugProperty = null;
        //    if (VSConstants.S_OK != expression.EvaluateSync((uint)enum_EVALFLAGS.EVAL_NOSIDEEFFECTS,
        //        unchecked((uint)Timeout.Infinite), null, out DebugProperty))
        //    {
        //        Debug.WriteLine("Failed to evaluate expression.");
        //        return null;
        //    }

        //    return DebugProperty;
        //}
        //private Byte[] _getMemoryData(IDebugProperty2 DebugProperty)
        //{
        //    // Get memory context for the property.
        //    IDebugMemoryContext2 memoryContext;
        //    if (VSConstants.S_OK != DebugProperty.GetMemoryContext(out memoryContext))
        //    {
        //        // In practice, this is where it seems to fail if you enter an invalid expression.
        //        Debug.WriteLine("Failed to get memory context.");
        //        return null;
        //    }

        //    // Get memory bytes interface.
        //    IDebugMemoryBytes2 memoryBytes;

        //    if (VSConstants.S_OK != DebugProperty.GetMemoryBytes(out memoryBytes))
        //    {
        //        Debug.WriteLine("Failed to get memory bytes interface.");
        //        return null;
        //    }
        //    return _readMemoeyValue(memoryContext, memoryBytes, _getVariableSize(DebugProperty));

        //}
        //private Byte[] _readMemoeyValue(IDebugMemoryContext2 memoryContext, IDebugMemoryBytes2 memoryBytes, uint dataSize)
        //{
        //    // Allocate space for the result.
        //    byte[] data = new byte[dataSize];
        //    uint writtenBytes = 0;

        //    // Read data from the debuggee.
        //    uint unreadable = 0;
        //    int hr = memoryBytes.ReadAt(memoryContext, dataSize, data, out writtenBytes, ref unreadable);
        //    return VSConstants.S_OK != hr ? null : data;
        //}
        //private uint _getVariableSize(IDebugProperty2 DebugProperty)
        //{
        //    DEBUG_PROPERTY_INFO[] info = new DEBUG_PROPERTY_INFO[1];
        //    DebugProperty.GetPropertyInfo((uint)enum_DEBUGPROP_INFO_FLAGS.DEBUGPROP_INFO_ALL, 10, unchecked((uint)Timeout.Infinite), null, 0, info);

        //    // The number of bytes to read.
        //    uint dataSize = 4;
        //    return dataSize;
        //}
        //private int _getDebugExpressionContex(out IDebugExpressionContext2 expressionContext)
        //{
        //    // Get a context for evaluating expressions.
        //    DebugStackFrame stack = CurrentStackFrame as DebugStackFrame;
        //    if (VSConstants.S_OK != stack.Stack.GetExpressionContext(out expressionContext))
        //    {
        //        Debug.WriteLine("Failed to get expression context.");
        //        return VSConstants.S_FALSE;
        //    }
        //    return VSConstants.S_OK;
        //}
    }
}
