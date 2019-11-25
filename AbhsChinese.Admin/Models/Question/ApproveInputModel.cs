using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class ApproveInputModel
    {
        public IList<int> Mark { get; set; }
        public SubjectStatusEnum NextStatus { get; set; }
        public int SubjectId { get; set; }
    }
}