using ComponentOwl.BetterListView;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointHitLoactions : BreakPointProperty
    {
        public delegate void HitEventHandler(object sender, DebugThread thread);
        public event HitEventHandler BreakPointHitEvent;
        private static readonly string Name = "HitLoactions";
        private static readonly string Value = "( ... )";
        public string TargetThreadId { get; set; }
        private BetterListViewComboBoxEmbeddedControl _combobox;
        public Dictionary<string, TargetThread> Targets { get; private set; }
        public override IBetterListViewEmbeddedControl Control
        {
            get
            {
                List<BreakpointHitLocation> loactions = _try_get_location(TargetThreadId);
                if (loactions == null)
                {
                    return null;
                }
                _combobox = new BetterListViewComboBoxEmbeddedControl();
                _combobox.Items.AddRange(loactions.ToArray());
                return _combobox;
            }
        }
        public BreakpointHitLoactions()
            : base(BreakpointHitLoactions.Name, BreakpointHitLoactions.Value)
        {
            TargetThreadId = "";
            Targets = new Dictionary<string, TargetThread>();
        }

        public void BreakpointHit(DebugThread thread)
        {
            if (!_try_add_hit_count(thread))
            {
                _add_hit_location(thread);
            }
        }

        private void _add_hit_location(DebugThread thread)
        {
            TargetThread target = new TargetThread();
            _update_hit_count(thread, target);
            Targets.Add(thread.ID, target);
        }

        private void _update_hit_count(DebugThread thread, TargetThread target)
        {
            target.Thread = thread;
            target.UpdateHitCount();
            if (BreakPointHitEvent != null) BreakPointHitEvent(this, thread);
        }

        private bool _try_add_hit_count(DebugThread thread)
        {
            TargetThread target;
            if (Targets.TryGetValue(thread.ID, out target))
            {
                _update_hit_count(thread, target);
                return true;
            }
            return false;
        }
        private List<BreakpointHitLocation> _try_get_location(string thread_id)
        {
            TargetThread target = null;
            if (Targets.TryGetValue(thread_id, out target))
            {
                return target.Locations;
            }
            return null;
        }
        public override void AfterLabelEdit(object sender, BetterListViewLabelEditEventArgs eventArgs)
        {
            if (_combobox.SelectedItem.ToString() == Value) return;
            BreakpointHitLocation bhl = _combobox.SelectedItem as BreakpointHitLocation;
            BreakPoints bps = Parent as BreakPoints;
            bps.ClearDetails();
            bps.AddDetialItems(bhl.Members);
        }
    }
}
