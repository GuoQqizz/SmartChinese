using AbhsChinese.Domain.Dto.Request;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Course
{
    public class CoursePricingInputModel
    {
        public string Arrange { get; set; }
        public int CourseId { get; set; }
        public string Introduction { get; set; }
        public IList<DtoPricing> Pricings { get; set; } = new List<DtoPricing>();
        public int Sort { get; set; }
        public int NextStatus { get; set; }
        public string CoverIamge { get; set; }
    }
}