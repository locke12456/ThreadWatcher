using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.Stack
{
    public class NextStatement : DebugStack
    {
        public NextStatement() : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            bool result = base._tyrToControl();
            uint index = StackFrame.CurrentFrameDepth+1;
            if (index < StackList.Count)
            {
                DebugStackFrame stack = StackList[(int)index] as DebugStackFrame;
                if (VSConstants.S_OK == Thread.CanSetNextStatement(stack.Stack, stack.CodeContext))
                {
                    result = true;
                }
            }
            else result = false;
            return result;
        }
    }
}
