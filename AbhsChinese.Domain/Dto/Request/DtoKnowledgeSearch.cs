using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoKnowledgeSearch : DtoSearch
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public KnowledgeEnum? Level { get; set; }
        public IList<int> Ids { get; set; }
        public int ParentId { get; set; }
    }
}