using AbhsChinese.Admin.Models.Common;

namespace AbhsChinese.Admin.Models.Question
{
    public class QuestionBasic
    {
        /// <summary>
        /// 难度
        /// </summary>
        public Select2 Difficulty { get; set; } = new Select2
        {
            Controller = "Select2",
            Action = "GetDifficulties"
        };

        public Select2 Grade { get; set; } = new Select2
        {
            Controller = "Select2",
            Action = "GetGrades"
        };

        public Select2 Keywords { get; set; } =
            new Select2
            {
                Multiple = "Multiple",
                Controller = "Select2",
                Action = "GetKeywords"
            };

        public Select2 Knowledges { get; set; } =
            new Select2
            {
                Multiple = "Multiple",
                Controller = "Select2",
                Action = "GetKnowledges"
            };

        public SubjectKnowledgeInputModel MainKnowledge { get; set; }
    }
}