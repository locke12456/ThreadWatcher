using libWatcherDialog.DebugScriptEngine.Breakpoint;
using libWatherDebugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.DebugScript
{
    public class DebugScriptItemFactory : ItemFactory<DebugScriptItem>
    {

        public DebugScriptItemFactory(BreakpointRule info)  
        {
            _initFactory();
            _initMaterials( info );
        }
        public override int CreateProduct()
        {
            _product = _createProduct();
            _productList.Add(_product);
            return 0;
        }
        protected override int _initFactory()
        {
            _productList = new List<DebugScriptItem>();
            return 0;
        }
        protected override DebugScriptItem _createProduct()
        {
            BreakpointRule info = _materials as BreakpointRule;
            DebugScriptItem item = new DebugScriptItem();
            item.BreakpointInfo = info.BreakpointInfo;
            item.Conditions = info.Conditions;
            item.VirtualVariables = info.VirtualVariables;
            item.Target = info.Target;
            return item;
        }
    }
}
