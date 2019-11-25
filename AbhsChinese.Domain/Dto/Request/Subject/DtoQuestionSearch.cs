using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request.Subject
{
    public class DtoQuestionSearch : DtoSearch
    {
        public DifficultyEnum? Difficulty { get; set; }
        public int Id { get; set; }
        public string Keyword { get; set; }
        public SubjectStatusEnum? SubjectStatus { get; set; }
        public IList<int> SubjectIds { get; set; }
        public SubjectTypeEnum? SubjectType { get; set; }
    }
}