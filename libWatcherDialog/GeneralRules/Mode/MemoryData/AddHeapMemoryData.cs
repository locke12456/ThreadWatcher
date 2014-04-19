﻿using libWatcherDialog.List;
using libWatcherDialog.PropertyItem.BreakPoint;
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
    public class AddHeapMemoryData : ItemManagementRule
    {
        public static readonly string Name = "MemoryAlloced";
        public static readonly string Created = "a data address has added to list.";
        public AddHeapMemoryData()
            : base()
        {
        }
        protected override void _init()
        {
            _script_list = new List<Func<bool>>() { _addToList, null };
        }
        private bool _addToList()
        {
            BreakpointsManagement bps = BreakpointsManagement.getInstance();
            DataBreakpointListItem item = new DataBreakpointListItem();
            item.MemoryAddressInfo = Data as HeapMemory;
            bps.AddMemoryData(item);
            ThreadItem thread = ThreadsManagement.getInstance().GetItem(_get_threadId());
            thread.WriteLog(Log.Create(Name, Created + "(" + Data.Variable + ")"));
            return true;
        }

        private string _get_threadId()
        {
            return (_dbg.CurrentThread as DebugThread).ID;
        }
    }
}
