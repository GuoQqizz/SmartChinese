using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    public class DtoSelectFillInBlankSubjectContent : DtoSubjectContent
    {
        /// <summary>
        /// 选项
        /// </summary>
        public IList<SubjectOption> Options { get; set; }

        /// <summary>
        /// 选项是文字还是图片
        /// </summary>
        public UeditorType OptionType { get; set; }
    }
}