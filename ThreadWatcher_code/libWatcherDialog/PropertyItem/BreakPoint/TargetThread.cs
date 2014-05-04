using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class TargetThread
    {
        public DebugThread Thread { get; set; }
        public List<BreakpointHitLocation> Locations { get; set; }
        public TargetThread()
        {
            Locations = new List<BreakpointHitLocation>();
        }
        public void UpdateHitCount() 
        {
            BreakpointHitLocationFactory factory = new BreakpointHitLocationFactory(Thread);
            factory.CreateProduct();
            BreakpointHitLocation loaction = _compare(factory.Product);
            loaction = _has_new_breakloaction(factory, loaction);
            _add_hit_count(loaction);
        }

        private BreakpointHitLocation _has_new_breakloaction(BreakpointHitLocationFactory factory, BreakpointHitLocation loaction)
        {
            if (loaction == null)
                loaction = _add_to_list(factory.Product);
            return loaction;
        }

        private void _add_hit_count(BreakpointHitLocation loaction)
        {
            loaction.AddHitCount();
        }

        private BreakpointHitLocation _add_to_list(BreakpointHitLocation loaction)
        {
            Locations.Add(loaction);
            return loaction;
        }

        private BreakpointHitLocation _compare(BreakpointHitLocation loaction)
        {
            foreach (BreakpointHitLocation bhl in Locations)
            {
                if (bhl.IsEquals(loaction))
                {
                    return bhl;
                }
            }
            return null;
        }

    }
}
