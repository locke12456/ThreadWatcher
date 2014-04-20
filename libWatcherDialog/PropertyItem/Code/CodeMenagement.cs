using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.Code
{
    public class CodeMenagement : ItemsManagement<List<string>>
    {
        protected static CodeMenagement _this;
        private Dictionary<string, List<string>> _files;
        public static CodeMenagement getInstance()
        {
            if (_this == null) _this = new CodeMenagement();
            return _this;
        }
        public static void Destroy()
        {
            _this = null;
        }
        public void AddItem(string filename,List<string> target)
        {
            GetItem(filename);
        }
        public override void AddItem(List<string> target)
        {
        }
        public List<string> GetItem(string filename)
        {
            List<string> file = _find_file(filename);
            return _open_file(filename, ref file);
        }

        private List<string> _open_file(string filename, ref List<string> file)
        {
            if (file == null)
            {
                file = File.ReadAllLines(filename).ToList();
                _add_filesKeyValue(filename, file);
            }
            return file;
        }

        private void _add_filesKeyValue(string key, List<string> value)
        {
            _files.Add(key, value);
        }

        private List<string> _find_file(string filename)
        {
            List<string> file;
            if (_files.TryGetValue(filename, out file))
                return file;
            return null;
        }
        private CodeMenagement()
        {
            _items = new List<List<string>>();
            _files = new Dictionary<string, List<string>>();
        }
    }
}
