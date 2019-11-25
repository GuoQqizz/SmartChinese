namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoCoursePriceListItem
    {
        public int Yce_CourseId { get; set; }
        public decimal Yce_Price { get; set; }
        public string Yce_SchoolLevelName { get; set; }
        public int Yce_SchoolLevelId { get; set; }
    }
}