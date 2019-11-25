using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudyTaskAnswer;
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
    public class StuStudyTaskAnsRepository : RepositoryBase<Yw_StudentStudyTaskAnswer>, IStuStudyTaskAnsRepository
    {
        public StuStudyTaskAnsRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_StudentStudyTaskAnswer GetByStudentTask(int studentId, int taskId)
        {
            string sql = "select * from Yw_StudentStudyTaskAnswer where Yta_StudentId=@StudentId and Yta_StudentStudyTaskId=@TaskId";
            Yw_StudentStudyTaskAnswer obj = Query(sql, new { StudentId = studentId, TaskId = taskId }).FirstOrDefault();
            if (obj == null)
            {
                return null;
            }

            Yw_StudentStudyTaskAnswerExt realObj = TranslatorFactory.Translator<Yw_StudentStudyTaskAnswer, Yw_StudentStudyTaskAnswerExt>(obj);
            realObj.EnableAudit();
            return realObj;
        }

        public void Add(Yw_StudentStudyTaskAnswer entity)
        {
            InsertEntity(entity);
        }

        public void Update(Yw_StudentStudyTaskAnswer entity)
        {
            UpdateEntity(entity);
        }

        public Yw_StudentStudyTaskAnswer Get(int id)
        {
            Yw_StudentStudyTaskAnswer obj = GetEntity(id);
            if (obj == null)
            {
                return null;
            }
            Yw_StudentStudyTaskAnswerExt realObj = TranslatorFactory.Translator<Yw_StudentStudyTaskAnswer, Yw_StudentStudyTaskAnswerExt>(obj);
            realObj.EnableAudit();
            return realObj;
        }
    }
}