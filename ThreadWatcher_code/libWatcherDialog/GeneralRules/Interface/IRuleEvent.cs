using libWatcherDialog.GeneralRules.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules
{
    public delegate void RuleEventHandler(object sender, RuleEventArgs e);
    interface IRuleEvent
    {
        event RuleEventHandler RuleProgressing;
        event RuleEventHandler RuleCompleted;
    }
}
