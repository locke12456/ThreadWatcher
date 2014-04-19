using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.List
{
    public abstract class ListItem : IListItem
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name + " (" + Message + ")";
        }
    }
}
