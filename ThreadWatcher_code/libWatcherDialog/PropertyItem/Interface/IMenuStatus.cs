using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libWatcherDialog.PropertyItem
{
    interface IMenuStatus
    {
        void Register(ContainerControl target);
        void Change();
    }
}
