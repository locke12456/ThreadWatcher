using System;
using System.Collections.Generic;
using System.Reflection;
namespace libWatcherDialog.DebugScriptEngine.Property
{
    public class BasicProperties
    {
        public const string Name = "name";
    }
    interface IPropertyInfo
    {
        string name { get; set; }
    }
    public class PropertyInfo : libWatcherDialog.DebugScriptEngine.Property.IPropertyInfo
    {
        [Property(BasicProperties.Name)]
        public string name { get; set; }
        public PropertyInfo() { }
        public PropertyInfo(Dictionary<string, object> json)
        {
            _init_form_json_object(json);
        }

        private void _init_form_json_object(Dictionary<string, object> json)
        {
            foreach (var json_object in json)
            {
                _set_value_by_key(json_object.Key, json_object.Value);
            }
        }
        protected virtual void _set_value_by_key(string key , object value)
        {
            System.Reflection.PropertyInfo info = this.GetType().GetProperty(key);
            Attribute[] attr = PropertyAttribute.GetCustomAttributes(info);
            PropertyAttribute key_info = attr[0] as PropertyAttribute;
            info.SetValue(this, value, null);
        }
    }

}
