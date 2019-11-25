using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    /// <summary>
    /// 圈点批注-断句
    /// </summary>
    public class DtoMark2SubjectContent : DtoSubjectContent
    {
        /// <summary>
        /// 标记的文本在学生端用什么颜色显示
        /// </summary>
        public SubjectColorEnum Color { get; set; }
        /// <summary>
        /// 圈点批注内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 内容的对齐方式
        /// </summary>
        public AlignmentEnum Alignment { get; set; }
    }
}