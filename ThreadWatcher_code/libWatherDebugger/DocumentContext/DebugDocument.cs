using libWatherDebugger.Exception.DebugItem;
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
        /// <summary>
        /// init code information 
        /// 如果取得資訊失敗，可能當前的stack是不可取得資訊的區域
        /// 例如: 中斷在外部程式函式庫中
        /// </summary>
        public CodeInformation()
        {
            try
            {
                _init();
            }
            catch (DebugDocumentException fail)
            {
                Debug.WriteLine(fail.Message);
            }
        }
        /// <summary>
        /// try to get CodeContext information
        /// </summary>
        private void _init()
        {
            try
            {
                CONTEXT_INFO[] code_info = new CONTEXT_INFO[1];
                CodeContext.GetInfo((uint)enum_CONTEXT_INFO_FIELDS.CIF_ALLFIELDS, code_info);
                _code_info = code_info;
            }
            catch (System.Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw (new GetCodeContextInfoFail());
            }
        }
        /// <summary>
        /// stack 的 function name
        /// </summary>
        public string FunctionName
        {
            get
            {
                if (_code_info == null) _init();
                return _code_info == null ? "" : _code_info[0].bstrFunction;
            }
        }
        /// <summary>
        /// 嘗試取得程式碼文件名稱
        /// 若是失敗的話，可能是在外部程式函式褲
        /// </summary>
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
                catch (System.Exception fail)
                {
                    Debug.WriteLine(fail.Message);
                }
                return "";
            }
        }
        /// <summary>
        /// 中斷時的程式碼行號
        /// e.g.
        ///  | line|     code
        ///  ----------------------------------------------------
        ///  |  5  |  exp = malloc( ... ); << StartPosition = 5 
        /// </summary>
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
        /// <summary>
        /// 中斷時的程式碼行號
        /// e.g.
        ///  | line|     code            |      comment
        ///  ----------------------------------------------------
        ///  |  5  |  exp = new a(  b     << StartPosition = 5
        ///  |  6  |               ,c
        ///  |  7  |               ,d  ); << EndPosition   = 7
        /// </summary>
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
        /// <summary>
        /// try to get source code in DocumentContext
        /// </summary>
        /// <returns></returns>
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

        public DebugDocument()
        {
            Code = new CodeInformation();
        }
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
        /// <summary>
        /// set by DebugDocumentFactory when factory creare a object
        /// </summary>
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

        /// <summary>
        /// set by DebugDocumentFactory when factory creare a object
        /// </summary>
        public IDebugDocument2 Document { get; set; }
        /// <summary>
        /// set by DebugDocumentFactory when factory creare a object
        /// </summary>
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
        /// <summary>
        /// get current stack/breakpoint filename
        ///  
        /// </summary>
        public string FileName
        {
            get
            {
                try
                {
                    return Code.FileName;
                }
                catch (System.Exception fail)
                {
                    Debug.WriteLine(fail.Message);
                }
                finally
                {
                }
                return "";
            }
        }
    }
}
