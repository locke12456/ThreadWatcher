using libUtilities;
using libWatcherDialog.DebugScriptEngine.Breakpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine
{
    public class DebugScriptEngine
    {
        private static DebugScriptEngine _this;
        private JavaScriptEngine _engine = JavaScriptEngine.getInstance();
        private Dictionary<string, object> _apis;
        private DataBreakpointScriptApi _watchpointApi;
        public static DebugScriptEngine getInstance()
        {
            if (_this == null) _this = new DebugScriptEngine();
            return _this;
        }
        public object RunScript(string filename) 
        {
            _engine.Parameters = _apis;
            return _engine.RunScript(filename);
        }
        private DebugScriptEngine() 
        {
            _watchpointApi = new DataBreakpointScriptApi();

            _apis = new Dictionary<string, object>() {
                {"addWatchponitFormAPi" , _watchpointApi}
            };

        }

    }
}
