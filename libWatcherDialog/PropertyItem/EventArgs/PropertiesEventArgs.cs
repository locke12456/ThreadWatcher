using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.EventArgs
{
    public class PropertiesEventArgs<T>
    {
        public T Item { get; private set; }
        public PropertiesEventArgs(T item)
        {
            Item = item;
        }
    }
}
