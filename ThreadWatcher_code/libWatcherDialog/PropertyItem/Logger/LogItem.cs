using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.Logger
{
    public abstract class LogItem 
    {
        public long CreatedTimeTick { get; protected set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }
        public override string ToString()
        {
            return Name + " : " + Message;
        }
    }
    public class Log : LogItem 
    {
        public static Log Create(string name, string message)
        {
            Log log = new Log();
            log.Name = name; log.Message = message;
            return log;
        }
        public Log() 
        {
            CreatedTimeTick = System.DateTime.Now.Ticks;
        }
    }
}

