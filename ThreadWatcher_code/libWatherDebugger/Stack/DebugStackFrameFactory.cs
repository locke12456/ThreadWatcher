using EnvDTE90a;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Stack
{
    public class DebugStackFrameFactory : ItemFactory<DebugStackFrame>
    {
        private EnvDTE.Debugger _that = Watcher.Debugger.Debugger.getInstance().VSDebugger;
        private IDebugThread2 _thread;
        private IEnumDebugFrameInfo2 _enumDebugFrameInfo2;
        private uint _currentFrameDepth = 0;

        public uint CurrentFrameDepth
        {
            get { return _currentFrameDepth; }
        }
        public DebugStackFrameFactory(IDebugThread2 thread)
        {
            _thread = thread;
        }
        public override int CreateProduct()
        {
            int result = base.CreateProduct();
            if (VSConstants.S_OK == result) _product = _productList[0];
            return result;
        }
        protected override int _initFactory()
        {
            if (VSConstants.S_OK == _initStack())
            {
                return VSConstants.S_OK;
            }
            return VSConstants.S_FALSE;
        }
        protected DebugStackFrame _createProduct()
        {
            DebugStackFrame stack = new DebugStackFrame();
            stack.Thread = _thread;
            stack.Stack = _materials as IDebugStackFrame2;
            IDebugCodeContext2 codeContext;
            if (VSConstants.S_OK == stack.Stack.GetCodeContext(out codeContext))
            {
                stack.CodeContext = codeContext;
            }
            return stack;
        }
        private int _initStack()
        {

            if (_that.CurrentStackFrame != null)
            {
                // Cast to StackFrame2, as it contains the Depth property that we need.
                StackFrame2 currentFrame2 = _that.CurrentStackFrame as StackFrame2;
                if (currentFrame2 == null)
                {
                    Debug.WriteLine("CurrentStackFrame is not a StackFrame2.");
                    return VSConstants.S_FALSE;
                }

                // Depth property is 1-based.
                _currentFrameDepth = currentFrame2.Depth - 1;
            }
            // Get frame info enum interface.
            if (VSConstants.S_OK != _getDebugFrameInfo(_currentFrameDepth, out _enumDebugFrameInfo2))
            {
                return VSConstants.S_FALSE;
            }
            // Get the current frame.
            return _getAllStackFrame(_enumDebugFrameInfo2);
        }
        private int _getDebugFrameInfo(uint currentFrameDepth, out IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {

            int result = VSConstants.S_FALSE;
            // Get frame info enum interface.
            if (VSConstants.S_OK != _thread.EnumFrameInfo((uint)enum_FRAMEINFO_FLAGS.FIF_FRAME, 0, out enumDebugFrameInfo2))
            {
                Debug.WriteLine("Could not enumerate stack frames.");
                return result;
            }
            // Skip frames above the current one.
            result = enumDebugFrameInfo2.Reset();

            if (VSConstants.S_OK != enumDebugFrameInfo2.Skip(currentFrameDepth))
            {
                Debug.WriteLine("Current stack frame could not be enumerated.");
                return VSConstants.S_FALSE;
            }

            return result;
        }
        private int _getAllStackFrame(IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {
            int result = VSConstants.S_FALSE;

            // Get all stack frame.
            FRAMEINFO[] frameInfo = new FRAMEINFO[1];
            _productList = new List<DebugStackFrame>();
            uint fetched = 0;
            while (enumDebugFrameInfo2.Next(1, frameInfo, ref fetched) == VSConstants.S_OK)
            {
                _initMaterials(frameInfo[0].m_pFrame);
                _productList.Add(_createProduct());
            }
            result = (ProductList.Count != 0) ? VSConstants.S_OK : result;
            return result;
        }
    }
}
