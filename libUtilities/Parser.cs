using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace libUtilities
{
    public partial class Parser
    {
        public static string[] ParseEnvironmentVariable(string cmd)
        {
            string[] cmds = _parseEnvironmentVariable(cmd);
            List<string> list = cmds.ToList();
            list = _recursive_remove(list);

            return list.Count > 1 ? list.ToArray() : null;
        }
        public static string[] ParseCommand(string cmd)
        {
            string[] cmds = _isIncludeSpace(cmd) ? _parseIncludeSpace(cmd) : _parse(cmd);
            List<string> list = cmds.ToList();
            list = _recursive_remove(list);

            return list.ToArray();
        }
        private static List<string> _recursive_remove(List<string> cmds)
        {
            foreach (string command in cmds)
            {
                if (command == "")
                {
                    cmds.Remove(command);
                    return _recursive_remove(cmds);
                }
            }
            return cmds;
        }
        private static bool _isIncludeSpace(string cmd)
        {
            return cmd.IndexOf('"') != -1;
        }
        private static string[] _parseIncludeSpace(string cmd)
        {
            string[] lcmd = Regex.Split(cmd, "[\"*\"]+");
            List<string> list = lcmd.Where(val => val != " ").ToList();
            lcmd = _parse(list[0]);
            list.RemoveAt(0);
            list.InsertRange(0, lcmd);
            return list.ToArray();
        }
        private static string[] _parseEnvironmentVariable(string variable)
        {
            string[] lcmd = Regex.Split(variable, "[%*%]+");
            List<string> list = lcmd.Where(val => val != " ").ToList();
            lcmd = _parse(list[0]);
            list.RemoveAt(0);
            list.InsertRange(0, lcmd);
            return list.ToArray();
        }
        private static string[] _parse(string cmd)
        {
            return cmd.Split(new char[] { ' ', '"' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
    public class JSONObject : Dictionary<string, Object>
    {
        public static Object Query(string json, string Key)
        {
            JSONObject dict = new JSONObject(json);
            return recursiveQuery(dict, Key);
        }
        public static Object Query(Object json, string Key)
        {
            if (json is Dictionary<string, Object>)
            {
                Dictionary<string, Object> dict = json as Dictionary<string, Object>;
                return recursiveQuery(dict, Key);
            }
            else return null;
        }
        public JSONObject() { }
        public JSONObject(string json)
        {
            try
            {
                recursive(JsonMapper.ToObject(json), this);
            }
            catch (Exception fail)
            {
                System.Console.WriteLine(fail.Message);
            }
        }
        public string ToJSONString()
        {
            JsonMapper mapper = new JsonMapper();
            string json = "";
            json = JsonMapper.ToJson(this);
            return json;
        }
        private void recursive(JsonData json, Dictionary<string, Object> item)
        {
            foreach (var data in json)
            {
                if (data is Object)
                {
                    KeyValuePair<string, JsonData> json_obj = (KeyValuePair<string, JsonData>)data;
                    switch (json_obj.Value.GetJsonType())
                    {
                        case JsonType.Array:
                            List<Object> obj = new List<Object>();
                            item.Add(json_obj.Key, obj);
                            try
                            {
                                recurarr(json_obj.Value, obj);
                            }
                            catch (Exception fail)
                            {
                                System.Console.WriteLine(fail.Message);
                            }
                            break;
                        case JsonType.Object:
                            Dictionary<string, Object> temp = new Dictionary<string, object>();
                            item.Add(json_obj.Key, temp);
                            try
                            {
                                recursive(json_obj.Value, temp);
                            }
                            catch (Exception fail)
                            {
                                System.Console.WriteLine(fail.Message);
                            }
                            break;
                        default:
                            item.Add(json_obj.Key, json_obj.Value);
                            break;
                    }
                }
            }
        }
        private void recurarr(JsonData arr, List<Object> item)
        {
            foreach (JsonData data in arr)
            {
                switch (data.GetJsonType())
                {

                    case JsonType.Array:
                        List<Object> obj = new List<Object>();
                        item.Add(obj);
                        try
                        {
                            recurarr(data, obj);
                        }
                        catch (Exception fail)
                        {
                            System.Console.WriteLine(fail.Message);
                        }
                        break;
                    case JsonType.Object:
                        Dictionary<string, Object> temp = new Dictionary<string, object>();
                        item.Add(temp); try
                        {
                            recursive(data, temp);
                        }
                        catch (Exception fail)
                        {
                            System.Console.WriteLine(fail.Message);
                        }
                        break;
                    default:
                        item.Add(data);
                        break;
                }

            }
        }
        private static Object recursiveQuery(Dictionary<string, Object> item, string key)
        {
            Object value = null;
            foreach (var data in item)
            {
                if (data.Key == key) return data.Value;
                if (data.Value is Dictionary<string, Object>)
                {
                    value = recursiveQuery(data.Value as Dictionary<string, Object>, key);
                    if (value != null) break;
                }
                if (data.Value is List<Object>)
                {
                    value = recursiveQuery(data.Value as List<Object>, key);
                    if (value != null) break;
                }
            }
            return value;
        }
        private static Object recursiveQuery(List<Object> items, string key)
        {
            Object value = null;
            foreach (var item in items)
            {
                if (item is Dictionary<string, Object>)
                {
                    value = recursiveQuery(item as Dictionary<string, Object>, key);
                    if (value != null) break;
                }
                if (item is List<Object>)
                {
                    value = recursiveQuery(item as List<Object>, key);
                    if (value != null) break;
                }
            }
            return value;
        }
    }
}
