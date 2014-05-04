using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadWatcher
{
    public class GUIDs
    {
        public static readonly Guid GuidNativeOnlyEng           = new Guid("3B476D35-A401-11D2-AAD4-00C04F990171");      //NativeOnly
        public static readonly Guid GuidScriptEng               = new Guid("F200A7E7-DEA5-11D0-B854-00A0244A1DE2");      //Script
        public static readonly Guid GuidCOMPlusNativeEng        = new Guid("92EF0900-2251-11D2-B72E-0000F87572EF");      //CompPLusNative
        public static readonly Guid GuidCOMPlusSQLLocalEng      = new Guid("E04BDE58-45EC-48DB-9807-513F78865212");      //SqlClr
        public static readonly Guid CLSID_SqlDebugEngine3       = new Guid("3B476D3A-A401-11D2-AAD4-00C04F990171");      //Yukon
        public static readonly Guid CLSID_SqlDebugEngine2       = new Guid("3B476D30-A401-11D2-AAD4-00C04F990171");      //LegacyTSQL
        public static readonly Guid FilterRegistersGuid         = new Guid("223ae797-bd09-4f28-8241-2763bdc5f713");
        public static readonly Guid FilterLocalsGuid            = new Guid("b200f725-e725-4c53-b36a-1ec27aef12ef");
        public static readonly Guid FilterAllLocalsGuid         = new Guid("196db21f-5f22-45a9-b5a3-32cddb30db06");
        public static readonly Guid FilterArgsGuid              = new Guid("804bccea-0475-4ae7-8a46-1862688ab863");
        public static readonly Guid FilterLocalsPlusArgsGuid    = new Guid("e74721bb-10c0-40f5-807f-920d37f95419");
        public static readonly Guid FilterAllLocalsPlusArgsGuid = new Guid("939729a8-4cb0-4647-9831-7ff465240d5f");
        public static readonly Guid CppLanguageGuid             = new Guid("3a12d0b7-c26c-11d0-b442-00a0244a1dd2");
    }
}
