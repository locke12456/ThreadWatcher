using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger
{
    public interface IDebugItemFactory
    {
        IDebugItem Product
        {
            get;
        }
        List<IDebugItem> ProductList
        {
            get;
        }
        int CreateProduct();
        int CreateProduct(Object material);
    }
}
