using libWatherDebugger.Memory;
using libWatherDebugger.Property;
using libWatherDebugger.Stack;
using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.WatchAPI.Control
{
    public class SetWatchMalloc
    {
        public SetWatchMalloc(DebugStackFrame stack) 
        {
            DebugPropertyFactory factory = new DebugPropertyFactory(stack);

            if (VSConstants.S_OK != factory.CreateProduct("____watch_malloc_active")) return;

            DebugProperty property = factory.ProductList[0] as DebugProperty;

            property.DataSize = 4;
        }
        public void Enable()
        {
        
        }
        public void Disable() 
        {
        
        }
    }
}
