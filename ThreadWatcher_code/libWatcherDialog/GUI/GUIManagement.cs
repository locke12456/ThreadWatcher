using libWatcherDialog;
using Watcher.Debugger;
namespace ThreadWatcher.GUI
{
    public class GUIManagement
    {
        public Threads Threads { get; private set; }
        public BreakPoints Breakpoints { get; private set; }
        public DebugScripts DebugScripts { get; private set; }
        public Debugger Debugger { get; private set; }
        private static GUIManagement _this = null;
        public static GUIManagement getInstance()
        {
            if (_this == null) _this = new GUIManagement();
            return _this;
        }
        public static void Destroy()
        {
            _this.Threads.Close();
            _this.Breakpoints.Close();
            _this.DebugScripts.Close();
            _this = null;
        }
        private GUIManagement()
        {
            Debugger = Debugger.getInstance(); 
            Threads = new Threads();
            Breakpoints = new BreakPoints();
            DebugScripts = new DebugScripts();
        }

    }
}
