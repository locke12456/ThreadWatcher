using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
using libWatcherDialog.PropertyItem.Log;
using libWatcherDialog.PropertyItem.Logger;
using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Memory;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules.Mode.MemoryData
{
    public class FreeHeapMemoryData : ItemManagementRule
    {
        public static readonly string Name = "MemoryFreed";
        public static readonly string Created = "a data address has freed .";
        public FreeHeapMemoryData()
            : base()
        {
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() { _remove, null };
        }
        private bool _remove()
        {
            DataBreakpointListItem data = BreakpointsManagement.getInstance().Datas.GetData(Data.Variable);
            if (data != null)
            {
                BreakpointsManagement.getInstance().RemoveMemoryData(data);
               // ThreadItem thread = ThreadsManagement.getInstance().GetItem();
                //thread.WriteLog(Log.Create(Name, Created));
                LogItem log = Log.Create(Name, Created + "(" + Data.Variable + ")");
                log.Key = (_dbg.CurrentThread as DebugThread).ID;
                //log.
                //log.Name = msg;
                LogManagement.getInstance().AddItem(log);
            }
            return true;
        }
    }
}
