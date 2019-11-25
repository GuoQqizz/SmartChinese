using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoPricing
    {
        public decimal Price { get; set; }
        public int SchoolLevelId { get; set; }
    }
    public class DtoCoursePricing
    {
        public int CourseId { get; set; }
        public string Arrange { get; set; }
        public string Introduction { get; set; }
        public IList<DtoPricing> Pricings { get; set; } = new List<DtoPricing>();
        public int Sort { get; set; }
        public CourseStatusEnum NextStatus { get; set; }
    }
}