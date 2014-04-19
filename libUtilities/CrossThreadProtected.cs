using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Dialog
{
    public sealed class Modify {
        public static string Add { get { return "Add"; } }
        public static string AddRange { get { return "AddRange"; } }
        public static string Remove { get { return "Remove"; } }
        public static string Clear { get { return "Clear"; } }
        public static string Enabled { get { return "Enabled"; } }
        public static string Disable { get { return "Disable"; } }
    }
    public class CrossThreadProtected
    {
        private ContainerControl _that;
        public delegate void UpdateGUI_BySetValueCallback(string setter, Object parmeter, Object control);
        public delegate void UpdateGUI_ByCallMethodCallback(string method, Object parmeter, Object control);
        public CrossThreadProtected(ContainerControl that)
        {
            _that = that;
        }
        public void UpdateGUI_BySetValue ( string setter , Object parmeter ,  Object control ) 
        {
            //MethodInfo , new Type[]{ parmeter.GetType() }
            if (_that.InvokeRequired)
            {
                UpdateGUI_BySetValueCallback ui = new UpdateGUI_BySetValueCallback(UpdateGUI_BySetValue);
                _that.Invoke(ui, setter, parmeter, control);
            }
            else
            {
                PropertyInfo methodInfo = control.GetType().GetProperty(setter);
                methodInfo.SetValue(control, parmeter, null);
            }
        }
        public void UpdateGUI_ByCallMethod (string method, Object parmeter, Object control)
        {
            if (_that.InvokeRequired)
            {
                UpdateGUI_ByCallMethodCallback ui = new UpdateGUI_ByCallMethodCallback(UpdateGUI_ByCallMethod);
                _that.Invoke(ui, method, parmeter, control);
            }
            else
            {
                MethodInfo methodInfo = control.GetType().GetMethod(method,new Type[] { parmeter.GetType() });
                methodInfo.Invoke(control, new object[]{parmeter});
            }
        }
        public void UpdateGUI_ByCallMethod(string method, Object control)
        {
            if (_that.InvokeRequired)
            {
                UpdateGUI_ByCallMethodCallback ui = new UpdateGUI_ByCallMethodCallback(UpdateGUI_ByCallMethod);
                _that.Invoke(ui, method, null, control);
            }
            else
            {
                MethodInfo methodInfo = control.GetType().GetMethod(method);
                methodInfo.Invoke(control, new object[] {  });
            }
        }
        //public void UpdateGUI_ByCallMethod(string method, Object[] parmeters, Object control)
        //{
        //    if (_that.InvokeRequired)
        //    {
        //        UpdateGUI_ByCallMethodCallback ui = new UpdateGUI_ByCallMethodCallback(UpdateGUI_ByCallMethod);
        //        _that.Invoke(ui, method, parmeters, control);
        //    }
        //    else
        //    {
        //        Type[] types = {};
        //        foreach (Object parmeter in parmeters)
        //            types[types.Length] = parmeter.GetType();
        //        MethodInfo methodInfo = control.GetType().GetMethod(method, types );
        //        methodInfo.Invoke(control, parmeters);
        //    }
        //}
    }
}
