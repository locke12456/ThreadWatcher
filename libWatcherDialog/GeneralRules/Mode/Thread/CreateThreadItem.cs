using libWatcherDialog.PropertyItem.Thread;
using libWatherDebugger.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.GeneralRules.Mode.Thread
{
    public class CreateThreadItem : WatcherRule
    {
        public DebugThread Thread { get; set; }
        public string Name { get; set; }
        public CreateThreadItem()
        {
            _script_list = new List<Func<bool>>() {
               _add_thread_item , null
            };
        }

        private bool _add_thread_item()
        {
            bool result = true;
            DebugThread thread = Thread;
            thread.Name = Name;
            ThreadItem item = new ThreadItem();
            item.Thread = thread;
            ThreadsManagement.getInstance().AddItem(item);
            return result;
        }
    }
}
