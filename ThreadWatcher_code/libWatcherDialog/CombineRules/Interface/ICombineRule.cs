﻿using libWatcherDialog.GeneralRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.CombineRules
{
    interface ICombineRule
    {
        bool Run();
        event RuleEventHandler RuleProgressing;
        event RuleEventHandler RuleCompleted;
    }
}
