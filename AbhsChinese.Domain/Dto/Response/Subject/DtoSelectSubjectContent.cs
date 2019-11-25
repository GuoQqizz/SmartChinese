using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    public class DtoSelectSubjectContent : DtoSubjectContent
    {
        public int Display { get; set; }

        /// <summary>
        /// 选择题的选项
        /// </summary>
        public IList<SubjectOption> Options { get; set; }

        /// <summary>
        /// 选项是文本还是图片
        /// </summary>
        public UeditorType OptionType { get; set; }
        public int Random { get; set; }
    }
}