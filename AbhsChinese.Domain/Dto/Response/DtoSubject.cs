using AbhsChinese.Domain.Entity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoSubject
    {
        public Yw_SubjectContent Content { get; set; }
        public int Difficulty { get; set; }
        public int Grade { get; set; }
        public int Id { get; set; }
        public string Keywords { get; set; }
        public IList<int> Knowledges { get; set; }
        public IList<string> KnowledgeNames { get; set; }
        public int MainKnowledge { get; set; }
        public string MainKnowledgeName { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public IList<string> Mark { get; set; }
    }
}