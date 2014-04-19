using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Property
{
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
    }
}
