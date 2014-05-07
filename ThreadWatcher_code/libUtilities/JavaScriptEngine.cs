using Noesis.Javascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libUtilities
{
    public class JavaScriptEngine
    {
        private static JavaScriptEngine _this;
        private JavascriptContext _context;

        public static JavaScriptEngine getInstance()
        {
            if (_this == null) _this = new JavaScriptEngine();
            return _this;
        }
        public Dictionary<string, object> Parameters
        {
            get;
            set;
        }

        public object RunScript(string filename)
        {
            _init_engine();
            return _try_run_script(filename);
        }

        private void _reset_parameter()
        {
            Parameters = new Dictionary<string, object>();
        }

        private object _try_run_script(string filename)
        {
            object result = null;
            try
            {
                _context.Run(filename);
                result = _context.GetParameter("result");
                _reset_parameter();
            }
            catch (Exception fail)
            {
                throw (fail);
            }
            return result;
        }
        private JavaScriptEngine()
        {

            _reset_parameter();
            //_init_engine();
        }

        private void _init_engine()
        {
            _context = new JavascriptContext();
            foreach (var parameter in Parameters)
            {
                _context.SetParameter(parameter.Key, parameter.Value);
            }
        }
    }
}
