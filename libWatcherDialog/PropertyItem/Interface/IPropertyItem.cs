using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    public interface IPropertyItem<T,T2>
    {
        List<T2> Children
        {
            get;
            //private set;
        }
        T Parent
        {
            get;
            //private set;
        }
    }
}
