using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ConvertTo<T>(
            this object o,
            PropertyNamePrefixAction action)
        {
            Check.IfNull(o, nameof(o));

            JsonSerializerSettings settings = null;

            Type type =
                action == PropertyNamePrefixAction.Add ? o.GetType() : typeof(T);
            if (type.IsDefined(typeof(JsonSerializeWithPrefixAttribute), true))
            {
                JsonSerializeWithPrefixAttribute attribute =
                    type.GetCustomAttribute<JsonSerializeWithPrefixAttribute>();
                string prefix = attribute.GetPrefix();

                DefaultContractResolver contractResolver = null;
                if (action == PropertyNamePrefixAction.Add)
                {
                    contractResolver = new AddPrefixContractResolver(prefix);
                }
                else
                {
                    contractResolver = new RemovePrefixContractResolver(prefix);
                }
                settings = new JsonSerializerSettings();
                //在序列化时,指定目标属性名字的格式
                settings.ContractResolver = contractResolver;
            }

            string jsonString = JsonConvert.SerializeObject(o, settings);
            T result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }

        public static string ToJson(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T ConvertTo<T>(this object obj, T target) where T : new()
        {
            if (target == null)
            {
                target = new T();
            }
            var type1 = typeof(T);
            var type2 = obj.GetType();
            if (type1.IsArray)
            {
                throw new Exception("不支持Array数据类型");
            }
            var props1 = type1.GetProperties().Where(p => p.CanWrite).ToArray();
            foreach (var p in props1)
            {
                try
                {
                    var val = type2.GetProperty(p.Name).GetValue(obj, null);
                    if (val != null)
                    {
                        p.SetValue(target, val);
                    }
                }
                catch (Exception)
                {
                }
            }
            return target;
        }
    }
}