using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Web.Models.Subjects
{
    public class MarkSubjectVm : SubjectVm
    {
        public SubjectColorEnum Color { get; set; }
        public string Content { get; set; }
    }
}