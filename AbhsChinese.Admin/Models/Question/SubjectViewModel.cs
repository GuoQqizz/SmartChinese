using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Admin.Models.Question
{
    public class SubjectViewModel
    {
        public string Difficulty { get; set; }
        public string Grade { get; set; }
        public int Id { get; set; }
        public string Keywords { get; set; }
        public string Name { get; set; }
        public int QuestionState { get; set; }
        public string QuestionStateText
        {
            get
            {
                return CustomEnumHelper.Parse(typeof(SubjectStatusEnum), QuestionState);
            }
        }
        public int RelevancyQuestions { get; set; }
        public int SubjectType { get; set; }
        public string SubjectTypeText { get; set; }

        public static SubjectViewModel Create(Yw_Subject subject)
        {
            return new SubjectViewModel
            {
                Name = subject.Ysj_Name,
                SubjectType = subject.Ysj_SubjectType,
                SubjectTypeText = CustomEnumHelper.Parse(
                    typeof(SubjectTypeEnum),
                    subject.Ysj_SubjectType),
                Difficulty = CustomEnumHelper.Parse(
                    typeof(DifficultyEnum),
                    subject.Ysj_Difficulty),
                Grade = CustomEnumHelper.Parse(typeof(GradeEnum), subject.Ysj_Grade),
                Id = subject.Ysj_Id,
                Keywords = subject.Ysj_Keywords,
                QuestionState = subject.Ysj_Status,
                RelevancyQuestions = subject.Ysj_GroupItemCount
            };
        }
    }
}