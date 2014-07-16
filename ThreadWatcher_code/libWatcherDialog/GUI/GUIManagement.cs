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
            _set_gui_distory_option();
            _distory_gui();
            _this = null;
        }
        private static void _set_gui_distory_option()
        {
            _this.Threads.CanDistory = true;
            _this.Breakpoints.CanDistory = true;
            _this.DebugScripts.CanDistory = true;
        }
        private static void _distory_gui()
        {
            _this.Threads.Close();
            _this.Breakpoints.Close();
            _this.DebugScripts.Close();
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
