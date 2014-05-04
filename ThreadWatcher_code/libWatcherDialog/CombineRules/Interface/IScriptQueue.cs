using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.CombineRules
{
    public interface IScriptQueue<T>
    {
        Queue<T> Scripts { get; set; }
        void AddRule(T script);
        void RunRule();
    }
}
