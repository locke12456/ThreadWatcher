using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.List
{
    interface IListItem
    {
        string Message { get; set; }
        string Name { get; set; }
    }
}
