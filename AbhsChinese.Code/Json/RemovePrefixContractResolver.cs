using Newtonsoft.Json.Serialization;

namespace AbhsChinese.Code.Json
{
    public class RemovePrefixContractResolver : DefaultContractResolver
    {
        public string Prefix { get; set; }

        public RemovePrefixContractResolver()
        {
        }

        public RemovePrefixContractResolver(string prefix)
        {
            Prefix = prefix;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string pName = propertyName;
            if (!string.IsNullOrWhiteSpace(Prefix))
            {
                pName = propertyName.Replace(Prefix + "_", string.Empty);
            }
            return pName;
        }
    }
}