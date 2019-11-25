using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class MultipleChoiceFillInBlankContentObj
    {
        public int ContentType { get; set; }
        public string Stem { get; set; }
        public List<SubjectOption> SubjectGOptions { get; set; }
        public List<SubjectOption> SubjectOptions { get; set; }
    }
}