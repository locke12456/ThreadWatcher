using libUtilities;
using libWatcher.Constants;
using libWatcherDialog.CombineRules;
using libWatcherDialog.CombineRules.Script;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.DebugScript;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Logger;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Stack;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watcher.Debugger;
using Watcher.Debugger.EventArgs;
using Watcher.Debugger.GeneralRules.Mode.BreakPoint;
using Watcher.Debugger.GeneralRules.Mode.Debugger;

namespace libWatcherDialog
{
    public partial class BreakPoints : BreakPointsRef
    {

        public delegate void BreakPointEventHandler(object sender);
        public BreakPoints()
            : base()
        {

            InitializeComponent();
            _initContextMenu();
        }
        private void BreakPoints_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
        }

        protected void Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearProprtyView();
            ClearDetails();
            if (Properties.SelectedItem != null)
            {
                BreakpointItem item = (Properties.SelectedItem as BreakpointItem);
                BreakpointsManagement.getInstance().CurrentItem = (item);
                AddProprtyViewItems(item.Children);
            }
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Details)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // Properties
            // 
            this.Properties.Location = new System.Drawing.Point(0, 0);
            this.Properties.Size = new System.Drawing.Size(142, 303);
            this.Properties.SelectedIndexChanged += new System.EventHandler(this.Properties_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.SplitterDistance = 153;
            // 
            // PropertyView
            // 
            this.PropertyView.Size = new System.Drawing.Size(228, 153);
            // 
            // Details
            // 
            this.Details.Size = new System.Drawing.Size(228, 144);
            // 
            // BreakPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(374, 302);
            this.Name = "BreakPoints";
            this.Text = "BreakPoints";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BreakPoints_FormClosed);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PropertyView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Details)).EndInit();
            this.ResumeLayout(false);

        }

    }
    public enum BreakPointsModes
    {
        Manual, Script
    }
    public class BreakPointsRef : PropertyForm<BreakpointItem, BreakPointProperty>
    {
        protected BreakpointsManagement _breakpoints = BreakpointsManagement.getInstance();
        protected DebugScriptsManagement _scripts = DebugScriptsManagement.getInstance();
        protected Debugger _watcher = Debugger.getInstance();
        protected Dictionary<BreakPointsModes, Func<bool>> _modes;
        protected Dictionary<string, CombineRules.CombineRules> _breakpoint_mode;
        protected Dictionary<string, Func<bool>> _modify_mode;
        protected Dictionary<DebugScriptItem, Func<bool>> _script_modify_mode;
        protected DebugBreakpoint _breakpoint;
        protected BreakpointMenu _menus;

        protected AddDataBreakpointFormScript _addDataFormScript;
        protected AddDataBreakpointToList _addDataToList;
        protected RemoveDataBreakPointFromAPI _removeData;
        
        public BreakPointsRef()
            : base()
        {
            _management = _breakpoints;
            _breakpoints.Mode = BreakPointsModes.Script;
            _create_modes();
            _init_events();
            _init_modes();
        }

        public bool BreakPointTriggered(DebugBreakpoint breakpoint)
        {
            _breakpoint = breakpoint; Func<bool> mode;
            if (_modify_mode.TryGetValue(_breakpoint.BreakpointKind, out mode))
            {
                mode();
            }
            return true;
        }

        private bool _script_mode()
        {
            DebugScriptItem script = _scripts.GetItem(_breakpoint);
            if (script != null)
            {
                _breakpoints.ConcernedTarget = script;
                _continue_debugging();
            }
            else
            {
                if (_breakpoints.ConcernedTarget != null)
                {
                    _do_script_modify();
                }
                else
                    _continue_debugging();
            }

            return true;

        }

        private void _do_script_modify()
        {
            _breakpoint_mode[APICppFiles.MemoryAlloc] = _addDataFormScript;
            _manaul_mode();
            _breakpoint_mode[APICppFiles.MemoryAlloc] = _addDataToList;
        }
        private void _continue_debugging()
        {
            _breakpoint_mode[APICppFiles.MemoryAlloc] = new NotConcernedPoint();
            _manaul_mode();
            _breakpoint_mode[APICppFiles.MemoryAlloc] = _addDataToList;
        }
        private bool _manaul_mode()
        {
            CombineRules.CombineRules mode;
            string name = (Debugger.getInstance().CurrentStackFrame as DebugStackFrame).FunctionName;
            if (_breakpoint_mode.TryGetValue(name, out mode))
            {
                _try_set_add_target(mode);
                mode.Run();
            }
            else
                return false;
            return true;
        }

        private void _try_set_add_target(CombineRules.CombineRules mode)
        {
            if (!(mode is BreakpointTriggerFromAPI))
                return;
            IBreakPoint bp = mode as IBreakPoint;
            bp.Breakpoint = _breakpoint;
        }
        private void _init_modes()
        {
            _modes = new Dictionary<BreakPointsModes, Func<bool>>() {
                {BreakPointsModes.Manual , _manaul_mode},
                {BreakPointsModes.Script , _script_mode}
            };
            _breakpoint_mode = new Dictionary<string, CombineRules.CombineRules>() { 
                 { APICppFiles.MemoryAlloc, _addDataToList },
                 { APICppFiles.MemoryFree, _removeData }
            };
            _modify_mode = new Dictionary<string, Func<bool>>() { 
                {Types.DataBreakpoint   , _data_breakpoint } , 
                {Types.Breakpoint       , _code_breakpoint } , 
            };
        }

        private void _init_events()
        {
            //removemode.RuleCompleted += removemode_RuleCompleted;
            _breakpoints.PropertyAdded += _breakpoints_PropertyAdded;
            _breakpoints.PropertyRemoved += _breakpoints_PropertyRemoved;
            _breakpoints.PropertyChanged += _breakpoints_PropertyChanged;
            _addDataFormScript.RuleCompleted += _addDataFormScript_RuleCompleted;
            Disposed += BreakPoints_Disposed;
        }

        private void _create_modes()
        {
            try
            {
                _addDataToList = new AddDataBreakpointToList();
                _addDataFormScript = new AddDataBreakpointFormScript();
                _removeData = new RemoveDataBreakPointFromAPI();
            }
            catch (Exception fail)
            {
                System.Diagnostics.Debug.WriteLine(fail.Message);
            }
        }

        protected override void _initContextMenu()
        {
            _menus = new BreakpointMenu();
            Properties.ContextMenuStrip = _menus.ListMenu;
        }

        private bool _data_breakpoint()
        {
            BreakpointItem item = BreakpointsManagement.getInstance().GetItem(_breakpoint.Name);
            if (!item.Breakpoint.FirstBreak(_breakpoint))
            {
                DebugThread thread = (Debugger.getInstance().CurrentThread as DebugThread);
                string name = item.Breakpoint.Name;
                string id = thread.ID;
                _write_log(thread, name, id);
            }
            return true;
        }

        private void _write_log(DebugThread thread, string name, string id)
        {
            ThreadLog log = new ThreadLog();
            log.Name = name;
            log.Key = id;
            LogManagement.getInstance().AddItem(log);
            BreakpointItem target = BreakpointsManagement.getInstance().GetItem(log.Name);
            target.HitLocations.BreakpointHit(thread);
        }

        private bool _code_breakpoint()
        {
            //FileInfo file = new FileInfo(_breakpoint.FileName);
            Func<bool> mode;
            if (_modes.TryGetValue( _breakpoints.Mode, out mode))
            {
                mode();
            }

            return true;
        }

        private void _addDataFormScript_RuleCompleted(object sender, RuleEventArgs e)
        {
            _breakpoints.ConcernedTarget = null;
        }

        private void _breakpoints_PropertyChanged(object sender, PropertyItem.EventArgs.PropertiesEventArgs<BreakpointItem> e)
        {
            SetCurrentProperty(e.Item);
        }
        private void _breakpoints_PropertyRemoved(object sender, PropertyItem.EventArgs.PropertiesEventArgs<BreakpointItem> e)
        {
            RemoveProprty(e.Item);
        }
        private void _breakpoints_PropertyAdded(object sender, PropertyItem.EventArgs.PropertiesEventArgs<BreakpointItem> e)
        {
            AddProprty(e.Item);
        }
        private void removemode_RuleCompleted(object sender, RuleEventArgs e)
        {
            RemoveDataBreakPointFromAPI rule = sender as RemoveDataBreakPointFromAPI;
            RemoveProprtyByName(rule.Data.Variable);
        }

        private void BreakPoints_Disposed(object sender, EventArgs e)
        {
            BreakpointsManagement.Destroy();
        }

    }
}
