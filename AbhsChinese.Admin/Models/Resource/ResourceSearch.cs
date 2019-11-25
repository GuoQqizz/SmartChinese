using AbhsChinese.Admin.Models.Common;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceSearch:Search
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string NameOrKey { get; set; }
        public int TextType { get; set; }
        public int MediaType { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int GroupType { get; set; }
    }
}