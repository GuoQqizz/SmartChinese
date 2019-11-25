using System;

namespace AbhsChinese.Code.Json
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsonSerializeWithPrefixAttribute : Attribute
    {
        private readonly string prefix;

        public JsonSerializeWithPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }

        public string GetPrefix()
        {
            return prefix;
        }
    }
}