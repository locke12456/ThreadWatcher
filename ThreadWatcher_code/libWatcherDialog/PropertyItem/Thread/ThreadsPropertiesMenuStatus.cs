using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Watcher.Debugger;

namespace libWatcherDialog.PropertyItem.Thread
{
    public class ThreadsPropertiesMenuStatus : MenuStatus
    {
        private Dictionary<EnvDTE.dbgDebugMode, List<bool>> _select_modes;
        public ThreadsPropertiesMenuStatus(List<ToolStripMenuItem> items)
        {
            _items = items;
            _status = new Dictionary<MenuState,Func<List<bool>>>()
            { 
                {MenuState.Normal           , _normal },
                {MenuState.ItemSelected     , _selected },
            };
            //logToolStripMenuItem      ,breakToolStripMenuItem     ,continueToolStripMenuItem,
            //stepToolStripMenuItem     ,stepIntoToolStripMenuItem  ,stepOutToolStripMenuItem,
            //nextStepToolStripMenuItem
            _select_modes = new Dictionary<EnvDTE.dbgDebugMode, List<bool>>()
            {
                { EnvDTE.dbgDebugMode.dbgBreakMode , new List<bool>()
                        { true , false , true , true , true, true , true  }
                },
                { EnvDTE.dbgDebugMode.dbgRunMode , new List<bool>()
                        { true , true , false , false , false, false , false  }
                },
            };
        }
        private List<bool> _normal()
        {
            return new List<bool>() { false, false, false, false, false, false, false };
        }
        private List<bool> _selected() 
        {
            List<bool> mode = null;
            if (_select_modes.TryGetValue(Debugger.getInstance().VSDebugger.CurrentMode, out mode))
            {
                return mode;
            }
            return null;
        }
    }
}
