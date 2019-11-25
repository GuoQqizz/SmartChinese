using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsResource;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class QuestionInputModel
    {
        public IList<string> Mark { get; set; }
        public SubjectStatusEnum NextStatus { get; set; }
        public FormSubmitButton Button { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// 该字段用于下拉列表默认值
        /// </summary>
        public string DifficultyText
        {
            get
            {
                return CustomEnumHelper.Parse(typeof(DifficultyEnum), Difficulty);
            }
        }

        public string Explain { get; set; }
        public int Grade { get; set; }

        public string GradeText
        {
            get
            {
                return CustomEnumHelper.Parse(typeof(GradeEnum), Grade);
            }
        }

        public int Id { get; set; }
        public string Keywords { get; set; }

        /// <summary>
        /// 主知识点
        /// </summary>
        public int Knowledge { get; set; }

        ///// <summary>
        ///// 次级知识点
        ///// </summary>
        //public IList<int> Knowledges { get; set; }
        /// <summary>
        /// 次级知识点
        public string Knowledges { get; set; }

        public string KnowledgeText { get; set; }

        public IList<string> KnowledgeTexts { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 题干的纯文本
        /// </summary>
        public string PlainName { get; set; }

        /// <summary>
        /// 题型
        /// </summary>
        public int SubjectType { get { return GetSubjectType(); } }

        public SubjectStatusEnum SubjectStatus { get; set; }

        protected virtual int GetSubjectType()
        {
            throw new AbhsException(
                ErrorCodeEnum.NotImplementingVirtualMethod,
                AbhsErrorMsg.NotImplementingVirtualMethod);
        }
    }
}