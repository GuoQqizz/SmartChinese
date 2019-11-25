using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Code.Common;
using Dapper;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Dto.Response.StudentPractice;

namespace AbhsChinese.Repository.Repository
{
    public class StudentTaskRepository : RepositoryBase<Yw_StudentTask>, IStudentTaskRepository
    {
        public StudentTaskRepository() : base(AppSetting.ConnString)
        {

        }

        public void Add(Yw_StudentTask stuTask)
        {
            InsertEntity(stuTask);
        }

        public void Update(Yw_StudentTask stuTask)
        {
            UpdateEntity(stuTask);
        }

        public Yw_StudentTask GetByStudentTask(int student, int taskId)
        {
            string sql = "select * from Yw_StudentTask where Yuk_StudentId=@StudentId and Yuk_TaskId=@TaskId";
            Yw_StudentTask entity = Query(sql, new { StudentId = student, TaskId = taskId }).FirstOrDefault();
            if (entity != null)
            {
                entity.EnableAudit();
            }

            return entity;
        }

        public IList<DtoStudyTaskListItem> GetEntities(DtoStudyTaskSearch search)
        {
            StringBuilder fieldBuilder = new StringBuilder();
            fieldBuilder.Append("Ycs_Name AS CourseName,");
            fieldBuilder.Append("Yuk_FinishTime AS FinishTime,");
            fieldBuilder.Append("Yuk_CreateTime AS StartTime,");
            fieldBuilder.Append("Yuk_LessonIndex AS LessonIndex,");
            fieldBuilder.Append("Yuk_Status AS Status,");
            fieldBuilder.Append("Yuk_SubjectCount AS SubjectCount");
            fieldBuilder.Append(",Yuk_TaskId AS StudyTaskId");
            fieldBuilder.Append(",Yuk_Id AS StudentTaskId");

            StringBuilder whereBuilder = new StringBuilder();
            whereBuilder.Append("Yw_StudentTask");
            whereBuilder.Append(" INNER JOIN Yw_Course ON Yuk_CourseId = Ycs_Id");
            //whereBuilder.Append(" INNER JOIN Yw_CourseLesson ON Ycl_Id = Yuk_LessonId");
            whereBuilder.Append(" WHERE Yuk_StudentId = @studentId");
            whereBuilder.Append(" AND Yuk_TaskType = @taskType");

            if (search.TaskStatusParameter != null && search.TaskStatusParameter.Any())
            {
                whereBuilder.Append(" AND Yuk_Status in @taskStatuses");
            }

            string orderby = "Yuk_CreateTime DESC";
            var parameters = new
            {
                studentId = search.StudentId,
                taskType = (int)search.TaskType,
                taskStatuses = search.TaskStatusParameter
            };

            return QueryPaging<DtoStudyTaskListItem>(
                fieldBuilder.ToString(),
                 whereBuilder.ToString(),
                orderby,
                search.Pagination,
                parameters).ToList();
        }

        public List<DtoStudentReportList> GetStuTaskReport(PagingObject paging, int studentId, int taskType)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append(@"Yw_StudentTask 
                            inner join Yw_Course on Yuk_CourseId = Ycs_Id 
                            left join Yw_CourseLesson on Yuk_LessonId = Ycl_Id where Yuk_StudentId = @StudentId and Yuk_Status = 3");
            parameters.Add("StudentId", studentId);
            if (taskType == (int)CourseReportEnum.任务报告)
            {
                strSql.Append(" and (Yuk_TaskType=1 or Yuk_TaskType=2)");
            }
            else if (taskType == (int)CourseReportEnum.练习报告)
            {
                strSql.Append(" and Yuk_TaskType=3");
            }
            return QueryPaging<DtoStudentReportList>("Yuk_Id as Id,case Yuk_TaskType when 1 then 2 when 2 then 2 when 3 then 3 end as ReportType,Yuk_LessonIndex as LessonIndex,Yuk_FinishTime as FinishStudyTime,Ycs_Name,Ycl_Name", strSql.ToString(), "Yuk_FinishTime DESC", paging, parameters).ToList();
        }

        public List<Yw_StudentTask> StuTaskReportCount(int studentId)
        {
            string sql = "select *from Yw_StudentTask where Yuk_StudentId = @StudentId and Yuk_Status = 3";
            return Query(sql, new { StudentId = studentId }).ToList();
        }

        public DtoStudentReportList GetStuTaskReportById(int taskId)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append(@"select Yuk_Id as Id,Yuk_StudentId as StudentId,case Yuk_TaskType when 1 then 2 when 2 then 2 when 3 then 3 end as ReportType,Yuk_CourseId as CourseId,Yuk_LessonIndex as LessonIndex,Yuk_FinishTime as FinishStudyTime,Ycs_Name,Ycl_Name               from Yw_StudentTask 
                            inner join Yw_Course on Yuk_CourseId = Ycs_Id 
                            left join Yw_CourseLesson on Yuk_LessonId = Ycl_Id where Yuk_Id = @Id and Yuk_Status = 3");
            return QueryObject<DtoStudentReportList>(strSql.ToString(), new { Id = taskId }).FirstOrDefault();
        }

        public int GetPracticeReportCount(int studentId, int courseId, int id)
        {
            var sql = "select count(1) from Yw_StudentTask where Yuk_StudentId=@StudentId and Yuk_CourseId=@CourseId and Yuk_TaskType=3 and Yuk_TaskId<=(select Yuk_TaskId from Yw_StudentTask where Yuk_Id=@Id)";
            return ExecuteScalar<int>(sql, new { StudentId = studentId, CourseId = courseId, Id = id });
        }

        public IList<DtoStatusTotalRecord> GetTotalRecordByStatus(DtoStudyTaskSearch search)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT COUNT(1) AS TotalRecord,Yuk_Status AS Status 
                                FROM Yw_StudentTask WHERE 1 = 1");
            sqlBuilder.Append(" AND Yuk_TaskType = @TaskType");
            sqlBuilder.Append(" AND Yuk_StudentId = @StudentId");
            sqlBuilder.Append(" GROUP BY Yuk_Status");

            return Query<DtoStatusTotalRecord>(
                sqlBuilder.ToString(),
                new { TaskType = search.TaskType, StudentId = search.StudentId }).ToList();
        }

        public bool IsHasTaskReport(int studentId, int id)
        {
            string sql = "select count(1) from Yw_StudentTask where Yuk_StudentId=@StudentId and Yuk_Id=@Id";
            return ExecuteScalar<int>(sql, new { StudentId = studentId, Id = id }) > 0;
        }
    }
}
