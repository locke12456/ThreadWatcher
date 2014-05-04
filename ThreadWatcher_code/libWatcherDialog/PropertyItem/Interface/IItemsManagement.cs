using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    interface IItemsManagement<T>
    {
        T CurrentItem { get; }
    }
}
