using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libUtilities
{
    public interface IDebuggerMemory
    {
        string Address { get; }
        string Type { get;  }
        string ThreadInfo { get;  }
        string Variable { get;  }
        string Value { get; set; }
        string Size { get; set; }
        bool IsPointer { get; }
        bool IsNullPointer { get; }
        List<IDebuggerMemory> Members { get;  }
    }
}
