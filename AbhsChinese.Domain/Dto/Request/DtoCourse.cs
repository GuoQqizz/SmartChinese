using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request
{
    [JsonSerializeWithPrefix("Ycs")]
    public class DtoCourse
    {
        public int CourseType { get; set; }
        public int CurrentUser { get; set; }
        public CourseStatusEnum Status { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        public int LessonCount { get; set; }
        public IList<string> Lessons { get; set; } = new List<string>();
        public string Name { get; set; }
        public int Owner { get; set; }
        public int ResourceGroupId { get; set; }
        public string Employees { get; set; }
    }
}