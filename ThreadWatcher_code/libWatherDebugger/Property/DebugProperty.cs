using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Diagnostics;

namespace libWatherDebugger.Property
{
    /// <summary>
    /// 用於讀取或修改Debugger中的Expression
    /// </summary>
    public class DebugProperty : IDebugItem
    {
        private IDebugMemoryContext2 _memoryContext;
        private IDebugMemoryBytes2 _memoryBytes;
        public IDebugProperty2 Property { get; set; }
        public IDebugExpressionContext2 ExpressionContext { get; set; }
        public IDebugExpression2 Expression { get; set; }
        public uint DataSize { get; set; }

        public DebugProperty() 
        {
            DataSize = 0;
        }
        /// <summary>
        /// Get memory context for the property.
        /// </summary>
        /// <returns></returns>
        public Byte[] GetData()
        {
            // Get memory context for the property.
            if (VSConstants.S_OK != _init())
            {
                // In practice, this is where it seems to fail if you enter an invalid expression.
                return null;
            }
            // Get memory bytes .
            return _readMemoeyValue( _memoryContext , _memoryBytes , DataSize );

        }
        /// <summary>
        /// write value to memory
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetData(Byte[] data) 
        {
            // Get memory context for the property.
            if (VSConstants.S_OK != _init())
            {
                // In practice, this is where it seems to fail if you enter an invalid expression.
                return false;
            }
            // Get memory bytes .
            return VSConstants.S_OK != _writeMemoeyValue(_memoryContext , _memoryBytes, data);
        }
        /// <summary>
        /// Get memory context for the property.
        /// Get memory bytes interface.
        /// </summary>
        /// <returns></returns>
        private int _init() 
        {
            int result = VSConstants.S_FALSE;
            // Get memory context for the property.
            if (VSConstants.S_OK != Property.GetMemoryContext(out _memoryContext))
            {
                // In practice, this is where it seems to fail if you enter an invalid expression.
                Debug.WriteLine("Failed to get memory context.");
                return result;
            }
            // Get memory bytes interface.
            if (VSConstants.S_OK != Property.GetMemoryBytes(out _memoryBytes))
            {
                Debug.WriteLine("Failed to get memory bytes interface.");
                return result;
            }
            return VSConstants.S_OK;
        }
        /// <summary>
        /// Read data from the debuggee.
        /// </summary>
        /// <param name="memoryContext">
        /// IDebugMemoryContext2 
        /// MSDN 
        /// how to get IDebugMemoryContext2 
        /// http://msdn.microsoft.com/zh-tw/library/bb145855(v=vs.110).aspx
        /// </param>
        /// <param name="memoryBytes"></param>
        /// http://msdn.microsoft.com/zh-tw/library/bb161954(v=vs.110).aspx
        /// <param name="dataSize">
        /// memory size
        /// </param>
        /// <returns></returns>
        private Byte[] _readMemoeyValue(IDebugMemoryContext2 memoryContext, IDebugMemoryBytes2 memoryBytes, uint dataSize)
        {
            // Allocate space for the result.
            byte[] data = new byte[dataSize];
            uint writtenBytes = 0;

            // Read data from the debuggee.
            uint unreadable = 0;
            int hr = memoryBytes.ReadAt(memoryContext, dataSize, data, out writtenBytes, ref unreadable);
            return VSConstants.S_OK != hr ? null : data;
        }
        /// <summary>
        /// write value to debuggee
        /// <param name="memoryContext">
        /// IDebugMemoryContext2 
        /// MSDN 
        /// how to get IDebugMemoryContext2 
        /// http://msdn.microsoft.com/zh-tw/library/bb145855(v=vs.110).aspx
        /// </param>
        /// <param name="memoryBytes"></param>
        /// http://msdn.microsoft.com/zh-tw/library/bb161954(v=vs.110).aspx
        /// <param name="data"></param>
        /// <returns></returns>
        private int _writeMemoeyValue(IDebugMemoryContext2 memoryContext, IDebugMemoryBytes2 memoryBytes,  byte[] data)
        {
            int hr = memoryBytes.WriteAt(memoryContext, DataSize , data);
            return hr;
        }
    }
}
