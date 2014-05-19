using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class SourceFileInfoProperties
    {
        public const string Name  = "fileName"  ;
        public const string Line      = "line"      ;
        public const string Code      = "code"      ;
        public const string Function  = "function"  ;
        public const string Type      = "type";
    }

   
    public class SourceFileInfo : PropertyInfo
    {
        [Property(SourceFileInfoProperties.Name)]
        public string name  { get; set; }
        [Property(SourceFileInfoProperties.Line)]
        public int    line      { get; set; }
        [Property(SourceFileInfoProperties.Code)]
        public string code      { get; set; }
        [Property(SourceFileInfoProperties.Function)]
        public string function  { get; set; }
        [Property(SourceFileInfoProperties.Type)]
        public string type      { get; set; }
        public SourceFileInfo() { }
        public SourceFileInfo(Dictionary<string, object> json)
            : base(json)
        {

        }
        //protected override void _set_value_by_key(string key, object value)
        //{
        //    System.Reflection.PropertyInfo info = GetType().GetProperty(key);
        //    Attribute[] attr = PropertyAttribute.GetCustomAttributes(info);
        //    PropertyAttribute key_info = attr[0] as PropertyAttribute;
        //    info.SetValue(this, value, null);
        //}
    }
}
