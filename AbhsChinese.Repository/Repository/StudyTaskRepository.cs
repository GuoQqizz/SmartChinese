using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Dto.Request.StudyTask;

namespace AbhsChinese.Repository.Repository
{
    public class StudyTaskRepository : RepositoryBase<Yw_StudyTask>, IStudyTaskRepository
    {
        public StudyTaskRepository() : base(AppSetting.ConnString)
        {

        }

        public void Add(Yw_StudyTask task)
        {
            InsertEntity(task);
        }

        public Yw_StudyTask Get(int id)
        {
            return GetEntity(id);
        }

        public void Update(Yw_StudyTask task)
        {
            UpdateEntity(task);
        }

        public IList<DtoSubjectCountToPractice> GetTotalSubjectsPracticed(
            IList<int> courseIds,
            int studentId)
        {
            string sql = @"SELECT t.Yuk_CourseId AS CourseId,
                                  t.Yuk_SubjectCount  AS SubjectCount
	                        FROM Yw_StudentTask t
	                        WHERE t.Yuk_TaskType = 3 AND t.Yuk_Status =3
                            AND t.Yuk_StudentId = @studentId
                            AND t.Yuk_CourseId IN @courseIds";

            return Query<DtoSubjectCountToPractice>(
                sql,
                new { courseIds = courseIds, studentId = studentId }).ToList();
        }

        public DtoStudyTaskListItem GetCourseLessonByStudyTask(int studentTaskId)
        {
            string sql = @"SELECT c.Ycs_Name as CourseName, Yuk_LessonIndex as LessonIndex
	                        FROM Yw_StudentTask as st
                            INNER JOIN Yw_Course AS c ON Yuk_CourseId = Ycs_Id
                            WHERE st.Yuk_Id = @StudentTaskId";

            return Query<DtoStudyTaskListItem>(sql, new { StudentTaskId = studentTaskId }).FirstOrDefault();
        }
    }
}
