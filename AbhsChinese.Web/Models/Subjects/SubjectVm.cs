using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Web.Models.Subjects
{
    public abstract class SubjectVm
    {
        public int KnowledgeId { get; set; }
        public string Stem { get; set; }
        public int StemType { get; set; }
        public object Answer { get; set; }
        public string Analysis { get; set; }
        public int SubjectType { get; set; }
    }
}