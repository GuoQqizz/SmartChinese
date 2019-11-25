namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoSimpleCourse
    {
        public int CourseId
        {
            set; get;
        }

        public int Grade
        {
            set; get;
        }

        public int CourseType
        {
            set; get;
        }

        public decimal Amount
        {
            set; get;
        }

        public string Row
        {
            get
            {
                return $"{CourseId},{Grade},{CourseType},{Amount}";
            }
        }
    }
}
