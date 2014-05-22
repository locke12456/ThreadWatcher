using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger.EventArgs;

namespace Watcher.Debugger
{
    public delegate void RuleEventHandler(object sender, RuleEventArgs e);
    interface IRuleEvent
    {
        event RuleEventHandler RuleProgressing;
        event RuleEventHandler RuleCompleted;
    }
}
