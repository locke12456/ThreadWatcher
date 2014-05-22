using libWatcherDialog.PropertyItem.BreakPoint.Property.BreakpointThread;
using libWatcherDialog.PropertyItem.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.PropertyItem.BreakPoint
{
    public class BreakpointHitLocation
    {
        private static readonly int max_filename = 14;

        public string FileName { get; set; }
        public string Code { get { return CodeMenagement.getInstance().GetItem(FileName)[(int)LineNumber - 1]; } }
        public uint LineNumber { get; set; }
        public uint HitCount { get; private set; }
        public BreakpointCondition Condition { get; private set; }
        public BreakpointHitLocation()
        {
            HitCount = 0;
        }
        public List<BreakPointProperty> Members
        {
            get
            {
                List<BreakPointProperty> members = new List<BreakPointProperty>();
                members.Add(new BreakPointProperty("File Name"  , FileName));
                members.Add(new BreakPointProperty("Code"       , Code));
                members.Add(new BreakPointProperty("Line Number", LineNumber.ToString()));
                members.Add(new BreakPointProperty("Hit Count"  , HitCount.ToString()));
                members.Add(new BreakPointProperty("Condition", HitCount.ToString()));
                return members;
            }
        }
        public void AddHitCount()
        {
            HitCount++;
        }
        public bool IsEquals(BreakpointHitLocation target)
        {
            return _filename_equal(target.FileName) && _lin_number_equal(target.LineNumber);
        }
        public override string ToString()
        {
            string msg = _get_location_info();
            msg += ",Line " + LineNumber.ToString();
            return msg;
        }

        private string _get_location_info()
        {
            string msg = "";
            FileInfo file = new FileInfo(FileName);
            foreach (char chip in file.Name)
            {
                if (msg.Length <= max_filename)
                    msg += chip;
                else { msg += "..."; break; }
            }
            return msg;
        }

        private bool _lin_number_equal(uint target)
        {
            return LineNumber == target;
        }

        private bool _filename_equal(string target)
        {
            return FileName == target;
        }

    }
}
