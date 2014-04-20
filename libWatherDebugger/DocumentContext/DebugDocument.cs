using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using System;
using System.Collections.Generic;
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
        public string FunctionName {
            get {
                if (_code_info == null) _init();
                return _code_info == null ? "" : _code_info[0].bstrFunction;
            }
        }
        public string FileName
        {
            get
            {
                string filename;
                if (VSConstants.S_OK == DocumentContext.GetName((uint)enum_GETNAME_TYPE.GN_FILENAME, out filename))
                {
                    return filename;
                }
                return "";
            }
        }
        public uint StartPosition {
            get {
                _code();
                return _start[0].dwLine;
            }
        }
        public uint EndPosition
        {
            get
            {
                _code();
                return _end[0].dwLine;
            }
        }
        //public string SourceCodeBlack { get { return _code(); } }
        public CodeInformation()
        {
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
            //code_info[0]
            //string[] file = File.ReadAllLines(FileName);
            //for (uint i = _start[0].dwLine - 1; i < _end[0].dwLine; i++)
            //{
            //    code += file[i];
            //}
            _code_string = code;
            return code;
        }
    }


    public class DebugDocument : IDebugItem
    {
        public CodeInformation Code { get; private set; }
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
                return Code.FileName;
            }
        }
        public DebugDocument()
        {
            Code = new CodeInformation();
        }
    }
}
