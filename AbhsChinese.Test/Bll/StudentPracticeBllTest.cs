using AbhsChinese.Bll;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbhsChinese.Bll.StudentPracticeBll;
using static AbhsChinese.Domain.JsonEntity.Answer.StuBlankAnswer;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentPracticeBllTest
    {
        [TestMethod]
        public void PayOrder()
        {
            StudentPracticeBll bll = new StudentPracticeBll();
            bll.GenerateTaskSubjectsAutoAfterStudy(10196, 10011);
        }

        [TestMethod]
        public void GeneratePracticeTest()
        {
            StudentPracticeBll bll = new StudentPracticeBll();
            bll.GeneratePractice(10003, 10001, 20);
        }

        [TestMethod]
        public void GenerateTaskSubjectsAutoAfterStudy()
        {
            Yw_StudentLessonAnswerExt answer = new Yw_StudentLessonAnswerExt();

            answer.Yla_Answer_Obj = new List<Domain.JsonEntity.Answer.StudentAnswerCard>();

            StudentAnswerCard card = new StudentAnswerCard();
            card.Part = 1;
            card.SubmitTime = DateTime.Now.ToString();
            card.AnswerCollection = new List<StudentAnswerBase>();

            BlankAnswerItem item = new BlankAnswerItem();
            item.Index = 0;
            item.Score = 0.8;
            item.Text = "ce shi";

            BlankAnswerItem item1 = new BlankAnswerItem();
            item1.Index = 1;
            item1.Score = 0.8;
            item1.Text = "ce shi1";

            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10000, Type = (int)SubjectTypeEnum.填空题 });
            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10001, Type = (int)SubjectTypeEnum.填空题 });
            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10002, Type = (int)SubjectTypeEnum.填空题 });
            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10003, Type = (int)SubjectTypeEnum.填空题 });
            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10004, Type = (int)SubjectTypeEnum.填空题 });
            card.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item, item1 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10005, Type = (int)SubjectTypeEnum.填空题 });
  
            StudentAnswerCard card1 = new StudentAnswerCard();
            card1.Part = 1;
            card1.SubmitTime = DateTime.Now.ToString();
            card1.AnswerCollection = new List<StudentAnswerBase>();

            BlankAnswerItem item2 = new BlankAnswerItem();
            item2.Index = 0;
            item2.Score = 0.8;
            item2.Text = "ce shi";

            BlankAnswerItem item3 = new BlankAnswerItem();
            item3.Index = 1;
            item3.Score = 0.8;
            item3.Text = "ce shi1";

            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10000, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10010, Type = (int)SubjectTypeEnum.填空题 });
            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10001, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10011, Type = (int)SubjectTypeEnum.填空题 });
            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10002, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10012, Type = (int)SubjectTypeEnum.填空题 });
            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10003, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10013, Type = (int)SubjectTypeEnum.填空题 });
            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10004, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10014, Type = (int)SubjectTypeEnum.填空题 });
            card1.AnswerCollection.Add(new StuBlankAnswer() { Answers = new List<StuBlankAnswer.BlankAnswerItem>() { item2, item3 }, KnowledgeId = 10005, ResultCoins = 3, ResultStars = 3, SubjectCoins = 5, SubjectId = 10015, Type = (int)SubjectTypeEnum.填空题 });

            answer.Yla_Answer_Obj.Add(card);
            answer.Yla_Answer_Obj.Add(card1);


            List<StudentAnswerBase> problemAnswers = answer.Yla_Answer_Obj.SelectMany(x => x.AnswerCollection).Where(x => x.ResultStars < 5).ToList();
            List<Tuple<int, int>> errorSubjects = problemAnswers.GroupBy(x => x.SubjectId).
                Select(x => new Tuple<int, int>(x.Key, x.Min(y => y.ResultStars))).ToList();

            List<DtoLesTaskSubject> subjects = new List<DtoLesTaskSubject>();

            subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10000, Number = 3, Score = 1, SubjectId = 11000, SubjectType = (int)SubjectTypeEnum.判断题 });
            subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10000, Number = 2, Score = 1, SubjectId = 11001, SubjectType = (int)SubjectTypeEnum.选择题 });
            subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10000, Number = 1, Score = 1, SubjectId = 11002, SubjectType = (int)SubjectTypeEnum.判断题 });
            subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10001, Number = 1, Score = 2, SubjectId = 11002, SubjectType = (int)SubjectTypeEnum.判断题 });
            subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10001, Number = 2, Score = 2, SubjectId = 11001, SubjectType = (int)SubjectTypeEnum.判断题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10001, Number = 3, Score = 2, SubjectId = 11003, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10001, Number = 3, Score = 2, SubjectId = 11004, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10001, Number = 3, Score = 2, SubjectId = 11005, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10002, Number = 3, Score = 2, SubjectId = 11202, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10003, Number = 3, Score = 2, SubjectId = 11203, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10004, Number = 3, Score = 2, SubjectId = 11204, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10005, Number = 3, Score = 2, SubjectId = 11205, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10006, Number = 3, Score = 2, SubjectId = 11006, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10007, Number = 3, Score = 2, SubjectId = 11007, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10008, Number = 3, Score = 2, SubjectId = 11008, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10009, Number = 3, Score = 2, SubjectId = 11009, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10010, Number = 3, Score = 2, SubjectId = 11010, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10011, Number = 3, Score = 2, SubjectId = 11011, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10012, Number = 3, Score = 2, SubjectId = 11012, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10013, Number = 3, Score = 2, SubjectId = 11013, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10014, Number = 3, Score = 2, SubjectId = 11014, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10015, Number = 3, Score = 2, SubjectId = 11015, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10016, Number = 3, Score = 2, SubjectId = 11016, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10017, Number = 3, Score = 2, SubjectId = 11017, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10018, Number = 3, Score = 2, SubjectId = 11018, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10019, Number = 3, Score = 2, SubjectId = 11019, SubjectType = (int)SubjectTypeEnum.选择题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10010, Number = 1, Score = 3, SubjectId = 12000, SubjectType = (int)SubjectTypeEnum.连线题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10011, Number = 1, Score = 3, SubjectId = 12001, SubjectType = (int)SubjectTypeEnum.连线题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10012, Number = 1, Score = 3, SubjectId = 12002, SubjectType = (int)SubjectTypeEnum.连线题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10013, Number = 1, Score = 3, SubjectId = 12003, SubjectType = (int)SubjectTypeEnum.连线题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10014, Number = 1, Score = 3, SubjectId = 12004, SubjectType = (int)SubjectTypeEnum.连线题 });
            //subjects.Add(new DtoLesTaskSubject() { ErrorSubjectId = 10015, Number = 1, Score = 3, SubjectId = 12005, SubjectType = (int)SubjectTypeEnum.连线题 });

            var groups = subjects.OrderBy(x => x.Number)
               .GroupBy(x => new { ErrorSubjectId = x.ErrorSubjectId, Score = x.Score })
               .OrderBy(x => x.Key.Score);

            Dictionary<int, DtoLesTaskSubject> subjectSelected = new Dictionary<int, DtoLesTaskSubject>();

            List<WrapSubjectGroup> wrapGroups = new List<WrapSubjectGroup>();
            foreach (IGrouping<dynamic, DtoLesTaskSubject> group in groups)
            {
                wrapGroups.Add(new WrapSubjectGroup(group.Key.Score, group.ToList()));
            }

            bool hasSubject = false;
            bool reachMax = false;
            int maxSubjectCount = groups.Count() > 50 ? groups.Count() : 50;//每个错题至少推一个关联题目
            int loopTime = 0;
            while (loopTime < maxSubjectCount)
            {
                hasSubject = false;
                foreach (WrapSubjectGroup group in wrapGroups)
                {
                    bool result = group.TakeSubject(subjectSelected);
                    if (result)
                    {
                        if (subjectSelected.Count >= maxSubjectCount)
                        {
                            reachMax = true;
                            break;
                        }
                        hasSubject = true;
                    }
                }
                if (!hasSubject || reachMax)
                {
                    break;
                }
                loopTime++;
            }

            if (subjectSelected.Count > 0)
            {
                string str = string.Join(",", subjectSelected.OrderBy(x => x.Value.SubjectType).Select(x => x.Value.SubjectId));
            }
        }

        [TestMethod]
        public void DistinctSubjectData()
        {
            List<DtoLesTaskSubject> subjects = new List<DtoLesTaskSubject>();
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, ErrorSubjectId = 9, Score = 9 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, Number = 100 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, ErrorSubjectId = 9, Score = 9 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, ErrorSubjectId = 19, Score = 19 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, ErrorSubjectId = 19, Score = 19 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10000, ErrorSubjectId = 29, Score = 29 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10001, ErrorSubjectId = 29, Score = 29 });
            subjects.Add(new DtoLesTaskSubject() { SubjectId = 10002, ErrorSubjectId = 29, Score = 29 });

            var u = subjects.GroupBy(x => new { ErrorSubjectId = x.ErrorSubjectId, Score = x.Score, TakenSubjectCount = 0 });
        }

        public class GroupItem
        {
            public int ErrorSubjectId
            {
                set; get;
            }

            public int Score
            {
                set; get;
            }

            public int TakenSubjectCount
            {
                set; get;
            }
        }
    }
}
