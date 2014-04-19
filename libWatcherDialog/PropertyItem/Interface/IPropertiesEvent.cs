using libWatcherDialog.PropertyItem.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    public delegate void PropertiesEventHandler<T>(object sender, PropertiesEventArgs<T> e);
    interface IPropertiesEvent<T>
    {
        event PropertiesEventHandler<T> PropertyChanged;
        event PropertiesEventHandler<T> PropertyAdded;
        event PropertiesEventHandler<T> PropertyRemoved;
    }
}
