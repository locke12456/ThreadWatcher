using libUtilities;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.GeneralRules.Mode.BreakPoint;

namespace libWatcherDialog.CombineRules
{
    public class BreakpointTriggerFromAPI : CombineRules, IBreakPoint
    {
        public virtual DebugBreakpoint Breakpoint { get; set; }
        public virtual IDebuggerMemory Data { get; set; }
        protected enum APITypes
        {
            Malloc, Free
        }
        protected APITypes api_type;
        public BreakpointTriggerFromAPI()
        {
            Data = null;
            _rules = new List<WatcherRule>() {
                 LastRule
            };
        }
        protected override void _init()
        {
            if (Data == null)
            {
                _initData();
            }
        }
        protected override void _finish()
        {
            base._finish();
            Data = null;
        }
        protected void _initData()
        {
            Dictionary<APITypes, Func<bool>> modes = _init_api_modes();
            Func<bool> mode;
            if (modes.TryGetValue(api_type, out mode))
            {
                mode();
            }
        }

        private Dictionary<APITypes, Func<bool>> _init_api_modes()
        {
            Dictionary<APITypes, Func<bool>> modes = new Dictionary<APITypes, Func<bool>>() 
            {
                {APITypes.Malloc , _malloc},{APITypes.Free,_free}
            };

            return modes;
        }
        protected bool _malloc()
        {
            Dictionary<string, IDebuggerMemory> mem_info;
            DebugStackFrame stack = _dbg.CurrentStackFrame as DebugStackFrame;
            mem_info = _dbg.Locals(stack);
            _get_data_breakpont_info(mem_info);
            return true;
        }

        private void _get_data_breakpont_info(Dictionary<string, IDebuggerMemory> mem_info)
        {
            IDebuggerMemory autoAddWatchPoint;
            IDebuggerMemory size;
            if (mem_info.TryGetValue("pMem", out autoAddWatchPoint))
            {
                size = _getDataSize(mem_info, autoAddWatchPoint);
            }
        }

        private IDebuggerMemory _getDataSize(Dictionary<string, IDebuggerMemory> mem_info, IDebuggerMemory autoAddWatchPoint)
        {
            IDebuggerMemory size;
            if (mem_info.TryGetValue("size", out size))
            {
                HeapMemory memoory_addr = _create_memory_data(autoAddWatchPoint, size);
                Data = memoory_addr;
            }
            return size;
        }

        private static HeapMemory _create_memory_data(IDebuggerMemory data, IDebuggerMemory size)
        {
            HeapMemory memoory_addr = new HeapMemory();
            memoory_addr.Variable = data.Value;
            _set_size(size, memoory_addr);
            return memoory_addr;
        }

        private static void _set_size(IDebuggerMemory size, HeapMemory memoory_addr)
        {
            if (size != null)
                memoory_addr.Size = size.Value;
        }
        protected bool _free()
        {
            Dictionary<string, IDebuggerMemory> mem_info;
            DebugStackFrame stack = _dbg.CurrentStackFrame as DebugStackFrame;
            mem_info = _dbg.Locals(stack);
            IDebuggerMemory pUserData;
            pUserData = _get_freed_data_breakpoint_info(mem_info);
            return true;
        }

        private IDebuggerMemory _get_freed_data_breakpoint_info(Dictionary<string, IDebuggerMemory> mem_info)
        {
            IDebuggerMemory pUserData;
            pUserData = _get_freed_variable(mem_info);
            return pUserData;
        }

        private IDebuggerMemory _get_freed_variable(Dictionary<string, IDebuggerMemory> mem_info)
        {
            IDebuggerMemory pUserData;
            if (mem_info.TryGetValue("ptr", out pUserData))
            {
                MemoryInfo watchInfo = _create_memory_data(pUserData, null);
                Data = watchInfo;
            }
            return pUserData;
        }
    }
}
