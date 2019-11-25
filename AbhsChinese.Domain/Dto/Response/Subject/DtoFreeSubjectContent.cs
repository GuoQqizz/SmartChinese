using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    public class DtoFreeSubjectContent : DtoSubjectContent
    {
        /// <summary>
        /// 评分标准
        /// </summary>
        public string Standard { get; set; }
    }
}