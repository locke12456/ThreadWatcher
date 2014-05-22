using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher.Debugger
{
    interface IWatcherRule
    {
        bool RunRules();
    }
}
