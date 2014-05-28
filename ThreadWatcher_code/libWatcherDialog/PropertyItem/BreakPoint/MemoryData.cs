using libWatcherDialog.List;
using libWatherDebugger.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class MemoryData
    {
        private List<DataBreakpointListItem> _datas;
        public MemoryData()
        {
            _datas = new List<DataBreakpointListItem>();
        }
        public List<DataBreakpointListItem> GetDisplayList() 
        {
            return _datas.Where(item => !item.MemoryAddressInfo.InWatchList).ToList();
        }
        public bool Find( string address) 
        {
            return _dataInList(address) != null;
        }
        public bool Find(HeapMemory data)
        {
            return _dataInList(data) != null;
        }
        public DataBreakpointListItem GetData(string address) 
        {
            return _dataInList(address);
        }
        public void Add(DataBreakpointListItem data)
        {
            if (!Find((data as DataBreakpointListItem).MemoryAddressInfo)) _datas.Add(data);
        }
        public void Remove(DataBreakpointListItem data)
        {
            if (Find((data as DataBreakpointListItem).MemoryAddressInfo)) _datas.Remove(data);
        }
        private bool _addressEquals(HeapMemory item, object data)
        {
            return item.Variable == data as string;
        }
        private bool _areEquals(HeapMemory item, object data)
        {
            return item == data as HeapMemory;
        }
        private DataBreakpointListItem _dataInList(object data)
        {
            Dictionary<Type, Func<HeapMemory, object, bool>> modes = new Dictionary<Type, Func<HeapMemory, object, bool>>() 
            {
                {typeof(HeapMemory) , _areEquals},{typeof(string) , _addressEquals}
            };
            foreach (DataBreakpointListItem item in _datas)
            {
                Func<HeapMemory, object, bool> mode;
                if (modes.TryGetValue(data.GetType(), out mode))
                {
                    if (mode(item.MemoryAddressInfo, data)) return item;
                }
            }
            return null;
        }
    }
}
