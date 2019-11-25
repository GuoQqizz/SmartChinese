using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Request.Teachers
{
    public class DtoEmployeeSearch
    {
        public int Grade { get; set; }
        public string RoleCode { get; set; }
        public StatusEnum Status { get; set; }
    }
}