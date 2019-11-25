using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response.StudentPractice
{
    public class DtoStatusTotalRecord
    {
        public StudentTaskStatusEnum Status { get; set; }
        public int TotalRecord { get; set; }
    }
}