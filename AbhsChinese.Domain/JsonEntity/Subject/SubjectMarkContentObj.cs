using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class SubjectMarkContentObj
    {
        public SubjectColorEnum Color { get; set; }
        public string Content { get; set; }
        public string Stem { get; set; }
        public int StemType { get; set; }
        /// <summary>
        /// 内容的对齐方式
        /// </summary>
        public AlignmentEnum Alignment { get; set; }
}
}