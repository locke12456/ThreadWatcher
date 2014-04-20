using ComponentOwl.BetterListView;
using libWatcherDialog.PropertyItem.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint.Property
{
    public class BreakpointHitLoactions : BreakPointProperty
    {
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
                if (loactions == null) { 
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

        public void BreakpointHit(ThreadItem thread)
        {
            if (!_try_add_hit_count(thread))
            {
                _add_hit_location(thread);
            }
        }

        private void _add_hit_location(ThreadItem thread)
        {
            TargetThread target = new TargetThread();
            target.Thread = thread;
            target.UpdateHitCount();
            Targets.Add(thread.Thread.ID, target);
        }

        private bool _try_add_hit_count(ThreadItem thread)
        {
            TargetThread target;
            if (Targets.TryGetValue(thread.Thread.ID, out target))
            {
                target.UpdateHitCount();
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
            BreakpointHitLocation bhl = _combobox.SelectedItem as BreakpointHitLocation;
            BreakPoints bps = Parent as BreakPoints;
            bps.AddDetialItems(bhl.Members);
        }
    }
}
