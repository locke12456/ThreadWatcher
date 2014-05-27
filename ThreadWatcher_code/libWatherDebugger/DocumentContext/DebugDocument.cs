using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.DocumentContext
{
    public class CodeInformation
    {
        private CONTEXT_INFO[] _code_info = null;
        public IDebugCodeContext2 CodeContext { get; set; }
        public IDebugDocumentContext2 DocumentContext { get; set; }
        private TEXT_POSITION[] _start;
        private TEXT_POSITION[] _end;
        private string _code_string = "";
        public string FunctionName
        {
            get
            {
                if (_code_info == null) _init();
                return _code_info == null ? "" : _code_info[0].bstrFunction;
            }
        }
        public string FileName
        {
            get
            {
                try
                {
                    string filename;
                    if (DocumentContext != null)
                        if (VSConstants.S_OK == DocumentContext.GetName((uint)enum_GETNAME_TYPE.GN_FILENAME, out filename))
                        {
                            return filename;
                        }
                }
                catch (Exception fail)
                {
                    Debug.WriteLine(fail.Message);
                }
                return "";
            }
        }
        public uint StartPosition
        {
            get
            {
                if (DocumentContext != null)
                {
                    _code();
                    return _start[0].dwLine;
                }
                return 1;
            }
        }
        public uint EndPosition
        {
            get
            {
                if (DocumentContext != null)
                {
                    _code();
                    return _end[0].dwLine;
                }
                return 1;
            }
        }
        //public string SourceCodeBlack { get { return _code(); } }
        public CodeInformation()
        {
            try
            {
                _init();
            }
            catch (Exception fail)
            {

            }
        }
        private void _init()
        {

            CONTEXT_INFO[] code_info = new CONTEXT_INFO[1];
            CodeContext.GetInfo((uint)enum_CONTEXT_INFO_FIELDS.CIF_ALLFIELDS, code_info);
            _code_info = code_info;
        }
        private string _code()
        {
            if (_code_string != "") return _code_string;
            string code = " ";
            _start = new TEXT_POSITION[1];
            _end = new TEXT_POSITION[1];
            DocumentContext.GetSourceRange(_start, _end);
            _code_string = code;
            return code;
        }
    }

    public class DebugDocument : IDebugItem
    {
        private CodeInformation _code;
        public CodeInformation Code
        {
            get
            {
                return _code;
            }
            private set
            {
                _code = value;
            }
        }
        public IDebugCodeContext2 CodeContext
        {
            get
            {
                return Code.CodeContext;
            }
            set
            {
                Code.CodeContext = value;
            }
        }
        public IDebugDocument2 Document { get; set; }
        public IDebugDocumentContext2 DocumentContext
        {
            get
            {
                return Code.DocumentContext;
            }
            set
            {
                Code.DocumentContext = value;
            }
        }

        public string FileName
        {
            get
            {
                try
                {
                    return Code.FileName;
                }
                catch (Exception fail)
                {
                    return "";
                }
            }
        }
        public DebugDocument()
        {
            Code = new CodeInformation();
        }
    }
}
