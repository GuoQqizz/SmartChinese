using AbhsChinese.Bll;
using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.Dto.Request.StudentWrong;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentWrongSubjectBllTest : BaseArrert
    {

        public StudentWrongBookBll bll = new StudentWrongBookBll();

        public SubjectBll subjectBll = new SubjectBll();

        [TestMethod]
        public void TestInsert()
        {
            List<StudentAnswerCard> answerCard = new List<StudentAnswerCard>();
            DtoStudentWrongBook wrongBook = new DtoStudentWrongBook();
            wrongBook.CourseId = 1;
            wrongBook.LessonId = 1;
            wrongBook.LessonProgressId = 1;
            wrongBook.Source = StudyWrongSourceEnum.课后任务;
            wrongBook.StudentId = 10;
            wrongBook.StudyTaskId = 1;

            StudentAnswerCard model = new StudentAnswerCard();
            model.AnswerCollection = new List<StudentAnswerBase>() { };
            for (int i = 7; i < 19; i++)
            {
                StuFreeAnswer tmp = new StuFreeAnswer();
                tmp.ResultStars = 0;
                tmp.SubjectId = i;
                tmp.KnowledgeId = i;
                tmp.Answer = "123456";
                model.AnswerCollection.Add(tmp);
            }
            answerCard.Add(model);
            var result = bll.SaveWrongBook(answerCard, wrongBook);
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestUpdateStatus()
        {
            List<int> list = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                list.Add(i);
            }
            //var res = bll.DeleteBySubjectIds(list, 2);
            //Assert.IsTrue(res);
        }
        [TestMethod]
        public void TestLinq()
        {
            List<Yw_StudentWrongSubject> old = new List<Yw_StudentWrongSubject>();
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 1
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.已消除,
                Yws_KnowledgeId = 2
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.已消除,
                Yws_KnowledgeId = 0
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.已消除,
                Yws_KnowledgeId = 3
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 2
            });

            var old1 = bll.GetSubjectByBookId(0);
            List<Yw_StudentWrongSubject> list = new List<Yw_StudentWrongSubject>();
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 0
            });
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 2
            });
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 3
            });
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 4
            });


            list = list.ExceptExt(old, l => l.Yws_KnowledgeId)
                        .Select(s => s)
                        .ToList();
            var WrongKnowledgeCount = (from o in old
                                       where o.Yws_Status == (int)StudyWrongStatusEnum.未消除
                                       select o.Yws_KnowledgeId)
                                       .Concat(
                                        from n in list select n.Yws_KnowledgeId
                                       ).Distinct().Count();

            int i = 0;
            Assert.AreEqual(WrongKnowledgeCount, 2);
        }

        [TestMethod]
        public void TestClearWrong()
        {

            int bookId = 10000;
            int subjectId = 10016;
            //for (int i = 0; i < 16; i++)
            //{
            //    var tmp = subjectId + i;

            //}
            bll.ClearWrongSubject(bookId, subjectId, StudyWrongStatusEnum.已做对);
        }
        [TestMethod]
        public void TestAnswer()
        {
            var ans = "{\"Sanw\":\"123456\",\"Type\":6,\"SbId\":15,\"Rtar\":0,\"SCon\":0,\"RCon\":0,\"Kdge\":15,\"IsRt\":false}";

            StudentAnswerBase baseModel = JsonConvert.DeserializeObject<StuFreeAnswer>(ans); //
            BaseAssertObj(baseModel);

            List<int> li = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var li2 = li.RandomSortList();

            int i = 0;

        }
        [TestMethod]
        public void TestRelationSubject()
        {
            int subjectId = 10043;
            //var list = subjectBll.GetSubjectForWrongClear(subjectId);
            var source = new List<Domain.Entity.Subject.Yw_Subject>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                source.Add(new Domain.Entity.Subject.Yw_Subject()
                {
                    Ysj_SubjectType = i % 5,
                    Ysj_MainKnowledgeId = i,
                    Ysj_Difficulty = r.Next(1, 5),
                    Ysj_Id = i,
                });
            }
            var list = subjectBll.SelectSubjectRealation(source, subjectId);

            BaseAssertList(list);
        }

        [TestMethod]
        public void ListExcept()
        {
            List<Yw_StudentWrongSubject> old = new List<Yw_StudentWrongSubject>();
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 1
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.已消除,
                Yws_KnowledgeId = 2
            });
            old.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.已消除,
                Yws_KnowledgeId = 0
            });

            List<Yw_StudentWrongSubject> list = new List<Yw_StudentWrongSubject>();
            var tmp = new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 0
            };
            list.Add(tmp);
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 2
            });
            list.Add(new Yw_StudentWrongSubject()
            {
                Yws_Status = (int)StudyWrongStatusEnum.未消除,
                Yws_KnowledgeId = 3
            });
            var s = list.ExceptExt(old, s1 => s1.Yws_KnowledgeId).ToList();

            int i = 0;
        }
    }

}
