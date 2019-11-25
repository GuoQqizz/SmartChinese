using Newtonsoft.Json.Serialization;

namespace AbhsChinese.Code.Json
{
    public class AddPrefixContractResolver : DefaultContractResolver
    {
        public string Prefix { get; set; }

        public AddPrefixContractResolver(string prefix)
        {
            Prefix = prefix;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string pName = propertyName;
            if (!string.IsNullOrWhiteSpace(Prefix))
            {
                pName = Prefix + "_" + propertyName;
            }
            return pName;
        }
    }
}