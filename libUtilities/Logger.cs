using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libUtilities
{

    public class LogTypes
    {
        public static string DEBUG = "Debug";
        public static string SYSTEM = "System";
        public static string LOG = "Log";

    }
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class LogAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string _logType;
        readonly string _logFile;
        // This is a positional argument
        public LogAttribute(string LogType, string LogFile)
        {
            _logType = LogType;
            _logFile = LogFile;
            // TODO: Implement code here
            //throw new NotImplementedException();
        }

        public string LogType
        {
            get { return _logType; }
        }

        public string LogFile
        {
            get { return _logFile; }
        }
        // This is a named argument
        public int NamedInt { get; set; }
    }
    public class Logger
    {
        private static Logger _logger = null;
        private static AutoResetEvent sync;// = new AutoResetEvent(false);
        private bool _isLogging = false;
        private Dictionary<string, StreamWriter> logs;

        public static Logger getInstance()
        {
            if (_logger == null) _logger = new Logger();
            return _logger;
        }
        public static bool Distroy()
        {
            _logger._close();
            _logger = null;
            return true;
        }
        public bool IsLogging
        {
            get { return _isLogging; }
        }
        [Log("Log", "Log.log")]
        public void Log(string msg)
        {
            _write(msg, LogTypes.LOG);
        }
        [Log("System", "SystemLog.log")]
        public void System(string msg)
        {
            _write(msg, LogTypes.SYSTEM);
        }
        [Log("Debug", "DebugLog.log")]
        public void Debug(string msg)
        {
#if DEBUG
            _write(msg, LogTypes.DEBUG);
#endif
        }
        private Logger()
        {
            _init();
        }
        private void _init()
        {
            List<string> methods = new List<string>() { LogTypes.DEBUG, LogTypes.SYSTEM , LogTypes.LOG };
            logs = new Dictionary<string, StreamWriter>();
            foreach (string method in methods)
            {
                MethodInfo info = GetType().GetMethod(method);
                foreach (LogAttribute attr in LogAttribute.GetCustomAttributes(info))
                {
                    string _syslog = attr.LogFile;
                    if (!File.Exists(_syslog))
                        using (FileStream log_file = File.Create(_syslog))
                        {
                            log_file.Close();
                        }

                    logs.Add(attr.LogType, File.AppendText(_syslog));
                }
            }
            sync = new AutoResetEvent(false);
            _isLogging = false;
        }
        private void _close()
        {
            _wait();
            foreach (StreamWriter log in logs.Values)
            {
                log.Close();
            }
            _reset();
        }
        private void _write(string msg, string type)
        {
            StreamWriter log;
            if (!logs.TryGetValue(type, out log)) return;
            _wait();
            log.WriteLine(" [" + DateTime.Now.ToString() + "][" + type + "] " + msg + ".");
            log.Flush();
            _reset();
        }
        private void _wait()
        {
            if (_isLogging) sync.WaitOne();
            sync.Reset();
            _isLogging = true;
        }
        private void _reset()
        {
            _isLogging = false;
            sync.Set();
        }

    }

}
