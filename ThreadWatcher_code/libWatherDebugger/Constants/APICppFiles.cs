using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcher.Constants
{
    public class APICppFiles
    {
        public static string APIFileName         { get { return "____watch_alloc.cpp"; } }
        public static string MemoryAlloc         { get { return "____watch_malloc"; } }
        public static int    MemoryAllocLine     { get { return 7; } }
        public static string MemoryFree          { get { return "____watch_free"; } }
        public static int    MemoryFreeLine      { get { return 12; } }
        public static string OperatorNew         { get { return "operator new"; } }
        public static string OperatorDelete      { get { return "operator delete"; } }
        public static string OperatorNewArray    { get { return "operator new[]"; } }
        public static string OperatorDeleteArray { get { return "operator delete[]"; } }
        public static List<string> APIs = new List<string>() { 
            MemoryAlloc            ,
            MemoryFree             ,
            OperatorNew            ,
            OperatorDelete         ,
            OperatorNewArray       ,
            OperatorDeleteArray    ,
        };
    }
}
