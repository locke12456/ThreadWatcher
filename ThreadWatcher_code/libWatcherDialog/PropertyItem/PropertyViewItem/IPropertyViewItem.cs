using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.PropertyViewItem
{
    interface IPropertyViewItem
    {
        string Name { get; }
        string Value { get; }
    }
}
