using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    public abstract class PropertyItem<T,T2> : IPropertyItem<T,T2>
    {
        public List<T2> Children
        {
            get;
            protected set;
        }
        public T Parent
        {
            get;
            protected set;
        }
    }
}
