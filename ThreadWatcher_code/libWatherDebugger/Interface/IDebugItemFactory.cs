using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger
{
    public interface IDebugItemFactory<T>
    {
        T Product
        {
            get;
        }
        List<T> ProductList
        {
            get;
        }
        int CreateProduct();
        int CreateProduct(Object material);
    }
}
