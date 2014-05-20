using System;
namespace libWatcherDialog.DebugScriptEngine.Property
{
    interface IPropertyAttribute
    {
        string Key { get; }
    }
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class PropertyAttribute : Attribute, libWatcherDialog.DebugScriptEngine.Property.IPropertyAttribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string _key;
        public PropertyAttribute():this("") { }
        // This is a positional argument
        public PropertyAttribute(string key = "")
        {
            _key = key;
        }
        public string Key { get { return _key; } }
        
    }
}
