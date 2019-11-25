using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    public class DtoMatchSubjectContent : DtoSubjectContent
    {
        /// <summary>
        /// 连线题左侧部分
        /// </summary>
        public IList<SubjectOption> LeftOptions { get; set; }

        /// <summary>
        /// 连线题左侧部分是图片还是文本
        /// </summary>
        public UeditorType LeftOptionType { get; set; }

        /// <summary>
        /// 连线题右侧部分
        /// </summary>
        public IList<SubjectOption> RightOptions { get; set; }

        /// <summary>
        /// 连线题右侧部分是图片还是文本
        /// </summary>
        public UeditorType RightOptionType { get; set; }
    }
}