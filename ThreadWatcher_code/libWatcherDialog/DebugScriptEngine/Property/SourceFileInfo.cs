using libWatcherDialog.PropertyItem.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class SourceFileInfoProperties
    {
        public const string FileName = "filename";
        public const string Line = "line";
        public const string Code = "code";
        public const string Function = "function";
        public const string Type = "type";
    }


    public class SourceFileInfo : PropertyInfo
    {
        private string _code = "";
        [Property(SourceFileInfoProperties.FileName)]
        public string filename { get; set; }
        [Property(SourceFileInfoProperties.Line)]
        public int line { get; set; }
        [Property(SourceFileInfoProperties.Code)]
        public string code
        {
            get
            {
                _try_get_code_information();
                return _code;
            }
            set
            { _code = value; }
        }

        [Property(SourceFileInfoProperties.Function)]
        public string function { get; set; }
        [Property(SourceFileInfoProperties.Type)]
        public string type { get; set; }
        public SourceFileInfo() { }
        public SourceFileInfo(Dictionary<string, object> json)
            : base(json)
        {
           // type = "";
        }
        private void _try_get_code_information()
        {
            if (_code != "") return;
            try
            {
                string file = Watcher.Debugger.Debugger.getInstance().VSDebugger.DTE.Solution.FindProjectItem(filename).Document.FullName;
                List<string> codes = CodeMenagement.getInstance().GetItem(file);
                _code = codes[line - 1];
            }
            catch (Exception fail)
            {
                Debug.WriteLine(fail.Message);
                _code = "setting error";
            }
        }
    }
}
