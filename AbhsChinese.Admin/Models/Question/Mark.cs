using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Admin.Models.Question
{
    public class Mark : QuestionInputModel
    {
        /// <summary>
        /// 内容的对齐方式
        /// </summary>
        public AlignmentEnum Alignment { get; set; }

        /// <summary>
        /// 答案显示的颜色
        /// </summary>
        public SubjectColorEnum Color { get; set; }

        public string Content { get; set; }
        public UeditorType StemType { get; set; }

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.圈点批注标色;
        }
    }
}