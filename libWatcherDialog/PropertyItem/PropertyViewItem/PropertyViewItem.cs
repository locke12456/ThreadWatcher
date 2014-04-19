using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.PropertyViewItem
{
    public abstract class PropertyViewItem : ItemProperties, IPropertyViewItem
    {
        public string Name { get; protected set; }
        public string Value { get; protected set; }
        public PropertyViewItem(string[] items)
            : base(items)
        {
            
        }
        public PropertyViewItem(string name, string value)
            : base(new string[] { name, value })
        {
            Name = name;
            Value = Value;
        }
    }
}
