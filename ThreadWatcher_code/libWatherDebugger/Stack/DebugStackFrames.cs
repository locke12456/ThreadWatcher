using libWatherDebugger.DocumentContext;
using libWatherDebugger.Memory;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Stack
{
    public class DebugStackFrames : List<DebugStackFrame>
    {
        private List<string> _stack_tarces;
        private string _message;

        public DebugStackFrames(DebugStackFrame current_stack)
            : base()
        {
            _init_stack_list(current_stack);
            _init_stack_traces();
        }

        private void _init_stack_traces()
        {
            _stack_tarces = new List<string>();
            _message = "";
            foreach (DebugStackFrame stack in this)
            {
                _try_get_stack_trace_msg(stack);
            }
            Debug.WriteLine(_message);
        }

        private void _try_get_stack_trace_msg(DebugStackFrame stack)
        {
            try
            {
                string trace_msg = stack.FunctionName + ", " + _code_position_info(stack);
                _message += trace_msg + "\n";
                _stack_tarces.Add(trace_msg);
            }
            catch (System.Exception fail)
            {
                // no message can read
            }
        }

        private void _init_stack_list(DebugStackFrame current_stack)
        {
            DebugStackFrameFactory factory = new DebugStackFrameFactory(current_stack.Thread);
            factory.CreateProduct();
            AddRange(factory.ProductList);
            
        }
        public override string ToString()
        {
            return _message;
        }

        private static string _code_position_info(DebugStackFrame stack)
        {
            if (stack.Document == null) return "";
            return stack.Document.FileName + "(" + Convert.ToString(stack.Document.Code.StartPosition) + ")";
        }
    }
}
