namespace AbhsChinese.Domain.Dto.Request
{
    /// <summary>
    /// 课后练习搜索条件
    /// </summary>
    public class DtoCoursesSearch : DtoSearch
    {
        public int StudentId { get; set; }
    }
}