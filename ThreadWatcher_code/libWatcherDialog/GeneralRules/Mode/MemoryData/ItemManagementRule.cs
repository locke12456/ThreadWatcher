﻿using libUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules.Mode.MemoryData
{
    public abstract class ItemManagementRule : WatcherRule 
    {
       public IDebuggerMemory Data { get; set; }
    }
}