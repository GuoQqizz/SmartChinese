using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response.Subject
{
    public abstract class DtoSubjectContent
    {
        public int SubjectId { get; set; }
        /// <summary>
        /// 主知识的id
        /// </summary>
        public int KnowledgeId { get; set; }
        /// <summary>
        /// 题干
        /// </summary>
        public string Stem { get; set; }
        /// <summary>
        /// 题干的类型(文字/图片)
        /// </summary>
        public UeditorType StemType { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public object Answer { get; set; }
        /// <summary>
        /// 解析
        /// </summary>
        public string Analysis { get; set; }
        /// <summary>
        /// 题的种类(选择题、判断题等)
        /// </summary>
        public SubjectTypeEnum SubjectType { get; set; }
    }
}