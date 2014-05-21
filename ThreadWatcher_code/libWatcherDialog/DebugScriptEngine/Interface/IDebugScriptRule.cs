using libWatcherDialog.PropertyItem.DebugScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine
{
    public interface IDebugScriptRule
    {
        DebugScriptItem GenerateScriptItem();
    }
}
