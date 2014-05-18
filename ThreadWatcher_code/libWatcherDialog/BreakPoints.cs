﻿using libUtilities;
using libWatcher.Constants;
using libWatcherDialog.CombineRules;
using libWatcherDialog.GeneralRules.Mode.BreakPoint;
using libWatcherDialog.GeneralRules.Mode.Debugger;
using libWatcherDialog.List;
using libWatcherDialog.PropertyItem;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatherDebugger.Breakpoint;
using libWatherDebugger.Memory;
using libWatherDebugger.Stack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watcher.Debugger;

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
        public void BreakPointTriggered(DebugBreakpoint breakpoint)
        {
            _breakpoint = breakpoint;
            Func<bool> mode;
            if (_modify_mode.TryGetValue(breakpoint.BreakpointKind, out mode))
            {
                mode();
            }
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
                AddProprtyViewItems((Properties.SelectedItem as BreakpointItem).Children);
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
    public class BreakPointsRef : PropertyForm<BreakpointItem, BreakPointProperty>
    {
        protected BreakpointsManagement _breakpoints = BreakpointsManagement.getInstance();
        protected Dictionary<string, CombineRules.CombineRules> _breakpoint_mode;
        protected Dictionary<string, Func<bool>> _modify_mode;
        protected DebugBreakpoint _breakpoint;
        protected BreakpointMenu _menus;
        protected AddDataBreakpointToList _addDataToList;
        protected RemoveDataBreakPointFromAPI _removeData;
        public BreakPointsRef()
            : base()
        {
            _create_modes();
            _init_events();
            _init_modes();
        }
        private void _init_modes()
        {
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
            Disposed += BreakPoints_Disposed;
        }
        private void _create_modes()
        {
            _addDataToList = new AddDataBreakpointToList();
            _removeData = new RemoveDataBreakPointFromAPI();
        }


        protected override void _initContextMenu()
        {
            _menus = new BreakpointMenu();
            Properties.ContextMenuStrip = _menus.ListMenu;
        }

        private bool _data_breakpoint()
        {
            Continue cnt = new Continue();
            cnt.RunRules();
            return true;
        }

        private bool _code_breakpoint()
        {
            //FileInfo file = new FileInfo(_breakpoint.FileName);
            CombineRules.CombineRules mode;
            string name = (Debugger.getInstance().CurrentStackFrame as DebugStackFrame).FunctionName;
            if (_breakpoint_mode.TryGetValue(name, out mode))
            {
                IBreakPoint bp = mode as IBreakPoint;
                bp.Breakpoint = _breakpoint;
                mode.Run();
            }
            return true;
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
        private void removemode_RuleCompleted(object sender, GeneralRules.EventArgs.RuleEventArgs e)
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