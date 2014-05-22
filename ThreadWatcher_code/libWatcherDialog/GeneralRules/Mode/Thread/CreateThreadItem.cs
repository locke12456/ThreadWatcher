using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watcher.Debugger;

namespace libWatcherDialog.GeneralRules.Mode.Thread
{
    public class CreateThreadItem : WatcherRule
    {

        public DebugThread Thread { get; set; }
        public string Name { get; set; }
        private ThreadItem _item;
        public CreateThreadItem()
        {
            _create_thread_item();
            _script_list = new List<Func<bool>>() {
               _init_thread_item , null
            };
        }

        private void _create_thread_item()
        {
            _item = new ThreadItem();
        }

        private bool _init_thread_item()
        {
            bool result = true;
            DebugThread thread = Thread;
            thread.Name = Name;
            _item.Thread = thread;
            ThreadsManagement.getInstance().AddItem(_item);
            return result;
        }
    }
}
