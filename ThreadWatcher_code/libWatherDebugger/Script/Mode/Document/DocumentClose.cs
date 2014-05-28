using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatherDebugger.Script.Mode.Document
{
    public class DocumentClose : Document
    {
        public DocumentClose()
            : base()
        {
            Mode = EnvDTE.dbgDebugMode.dbgBreakMode;
        }
        protected override bool _tyrToControl()
        {
            EnvDTE.Document document = null;
            foreach (EnvDTE.Document doc in _dbg.DTE.Documents) 
            {
                FileInfo file = new FileInfo(doc.FullName);
                if (file.Name == FileName) { document = doc; break; }
            }
            if (document != null)
                document.Close();
            return true;
        }
    }
}
