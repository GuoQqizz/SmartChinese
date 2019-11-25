using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StuLessonAnsRepository : RepositoryBase<Yw_StudentLessonAnswer>, IStuLessonAnsRepository
    {
        public StuLessonAnsRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_StudentLessonAnswer GetByProgressId(int lessonProgressId)
        {
            string sql = "select * from Yw_StudentLessonAnswer where Yla_LessonProgressId=@ProgressId";
            Yw_StudentLessonAnswer obj = Query(sql, new { ProgressId = lessonProgressId }).FirstOrDefault();
            if (obj != null)
            {
                Yw_StudentLessonAnswerExt realObj = TranslatorFactory.Translator<Yw_StudentLessonAnswer, Yw_StudentLessonAnswerExt>(obj);
                realObj.EnableAudit();
                return realObj;
            }
            return null;
        }

        public void Insert(Yw_StudentLessonAnswer lessonAnswer)
        {
            lessonAnswer.Yla_CreateTime = lessonAnswer.Yla_UpdateTime = DateTime.Now;
            InsertEntity(lessonAnswer);
        }

        public void Update(Yw_StudentLessonAnswer lessonAnswer)
        {
            lessonAnswer.Yla_UpdateTime = DateTime.Now;
            UpdateEntity(lessonAnswer);
        }

        public void Update(int id, string answer, string coins)
        {
            string sql = "update Yw_StudentLessonAnswer set Yla_StudentAnswer = Yla_StudentAnswer + @answer , Yla_StudentCoin = Yla_StudentCoin + @coins,Yla_UpdateTime = GETDATE() where Yla_LessonProgressId = @id";
            Execute(sql, new { id = id, answer = answer, coins = coins });
        }

        //public Yw_StudentLessonAnswer GetByStudentLesson(int lessonId, int studentId)
        //{
        //    string sql = @"select * from Yw_StudentLessonAnswer 
        //                   where Yla_LessonProgressId = 
        //                   (select Yle_Id from Yw_StudentLessonProgress where Yle_StudentId = @StudentId and Yle_LessonId = @LessonId and Yle_IsLastest=1)";

        //    Yw_StudentLessonAnswer obj = Query(sql, new { StudentId = studentId, LessonId = lessonId }).FirstOrDefault();
        //    Yw_StudentLessonAnswerExt realObj = TranslatorFactory.Translator<Yw_StudentLessonAnswer, Yw_StudentLessonAnswerExt>(obj);
        //    realObj.EnableAudit();

        //    return realObj;
        //}

        public Yw_StudentLessonAnswer GetStuLesAnswer(int studentId, int lessonProgressId)
        {
            string sql = "select * from Yw_StudentLessonAnswer where Yla_StudentId=@StudentId and Yla_LessonProgressId=@ProgressId";
            Yw_StudentLessonAnswer obj = Query(sql, new { StudentId = studentId, ProgressId = lessonProgressId }).FirstOrDefault();
            Yw_StudentLessonAnswerExt realObj = TranslatorFactory.Translator<Yw_StudentLessonAnswer, Yw_StudentLessonAnswerExt>(obj);
            realObj.EnableAudit();

            return realObj;
        }
    }
}
