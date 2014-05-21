using libWatcherDialog.PropertyItem.Event;
using libWatcherDialog.PropertyItem.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem
{
    public abstract class ItemsManagement<T> : IPropertiesEvent<T>, IItemsManagement<T> where T : class
    {

        public event PropertiesEventHandler<T> PropertyChanged;
        public event PropertiesEventHandler<T> PropertyAdded;
        public event PropertiesEventHandler<T> PropertyRemoved;
        protected List<T> _items;
        public T CurrentItem { get; set; }
        public void SetCurrentItem(T target)
        {
            T item = _findItemRule(target);
            if (item != null) { 
                CurrentItem = item;
                if (PropertyChanged != null) PropertyChanged(this, new PropertiesEventArgs<T>(item));
            }
        }
        public virtual T GetItem(T target)
        {
            return _findItemRule(target);
        }
        public List<T> GetList()
        {
            return _items;
        }
        public virtual void AddItem(T target)
        {
            T item = _findItemRule(target);
            if (item == null)
            {
                _items.Add(target);
                if (PropertyAdded != null) PropertyAdded(this, new PropertiesEventArgs<T>(target));
            }
            else {
                
            }

        }
        public void RemoveItem(T target)
        {
            T item = _findItemRule(target);
            if (item != null)
            {
                _items.Remove(item);
                if (PropertyRemoved != null) PropertyRemoved(this, new PropertiesEventArgs<T>(item));
            }
        }
        protected virtual T _findItemRule(T item) 
        {
            return _items.IndexOf(item) == -1 ? null : item;
        }
    }
}
