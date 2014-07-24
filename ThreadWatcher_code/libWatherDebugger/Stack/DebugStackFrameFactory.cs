using EnvDTE90a;
using libWatherDebugger.Thread;
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
    /// <summary>
    /// factory 架構參考 DebugDocumentFactory
    /// </summary>
    public class DebugStackFrameFactory : ItemFactory<DebugStackFrame>
    {
        private EnvDTE.Debugger _that = Watcher.Debugger.Debugger.getInstance().VSDebugger;
        private IDebugThread2 _thread;
        private IEnumDebugFrameInfo2 _enumDebugFrameInfo2;
        private uint _currentFrameDepth = 0;
        /// <summary>
        /// 若有去觀看其他stack，這裡為當前被操作的stack frame
        /// 沒有的話就是stack top = 0
        /// </summary>
        public uint CurrentFrameDepth
        {
            get { return _currentFrameDepth; }
        }
        public DebugStackFrameFactory(IDebugThread2 thread)
        {
            _thread = thread;
        }
        public DebugStackFrameFactory(DebugThread thread)
        {
            _thread = thread.Thread;
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
            int result = _get_frame_info();
            // Get the current frame.
            return _getAllStackFrame(_enumDebugFrameInfo2);
        }
        /// <summary>
        /// Get frame info enum interface.
        /// MSDN :
        /// IEnumDebugFrameInfo2 
        /// http://msdn.microsoft.com/zh-tw/library/bb147119(v=vs.110).aspx
        /// </summary>
        /// <returns></returns>
        private int _get_frame_info()
        {
            // Get frame info enum interface.
            if (VSConstants.S_OK != _getDebugFrameInfo(_currentFrameDepth, out _enumDebugFrameInfo2))
            {
                return VSConstants.S_FALSE;
            }
            return VSConstants.S_FALSE;
        }
        /// <summary>
        /// Get frame info enum interface.
        /// </summary>
        /// <param name="currentFrameDepth"></param>
        /// <param name="enumDebugFrameInfo2"></param>
        /// <returns></returns>
        private int _getDebugFrameInfo(uint currentFrameDepth, out IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {

            int result = VSConstants.S_FALSE;
            // Get frame info enum interface.
            result = _enumerate_frame_info(result, out enumDebugFrameInfo2);
            // Skip frames above the current one.
            return _skip_current_one(currentFrameDepth, enumDebugFrameInfo2, ref result);
        }
        /// <summary>
        /// Skip frames above the current one.
        /// </summary>
        /// <param name="currentFrameDepth"></param>
        /// <param name="enumDebugFrameInfo2"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static int _skip_current_one(uint currentFrameDepth, IEnumDebugFrameInfo2 enumDebugFrameInfo2, ref int result)
        {
            result = enumDebugFrameInfo2.Reset();

            if (VSConstants.S_OK != enumDebugFrameInfo2.Skip(currentFrameDepth))
            {
                Debug.WriteLine("Current stack frame could not be enumerated.");
                return VSConstants.S_FALSE;
            }

            return result;
        }
        /// <summary>
        /// Get frame info enum interface.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="enumDebugFrameInfo2"></param>
        /// <returns></returns>
        private int _enumerate_frame_info(int result, out IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {
            if (VSConstants.S_OK != _thread.EnumFrameInfo((uint)enum_FRAMEINFO_FLAGS.FIF_FRAME, 0, out enumDebugFrameInfo2))
            {
                Debug.WriteLine("Could not enumerate stack frames.");
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get all stack frame from IEnumDebugFrameInfo2
        /// </summary>
        /// <param name="enumDebugFrameInfo2"></param>
        /// <returns></returns>
        private int _getAllStackFrame(IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {
            // Get all stack frame.
            _create_products(enumDebugFrameInfo2);
            return _has_product();
        }

        private int _has_product()
        {
            int result = VSConstants.S_FALSE;
            result = (ProductList.Count != 0) ? VSConstants.S_OK : result;
            return result;
        }

        private void _create_products(IEnumDebugFrameInfo2 enumDebugFrameInfo2)
        {
            FRAMEINFO[] frameInfo = new FRAMEINFO[1];
            _productList = new List<DebugStackFrame>();
            uint fetched = 0;
            while (enumDebugFrameInfo2.Next(1, frameInfo, ref fetched) == VSConstants.S_OK)
            {
                _initMaterials(frameInfo[0].m_pFrame);
                _productList.Add(_createProduct());
            }
        }
    }
}
