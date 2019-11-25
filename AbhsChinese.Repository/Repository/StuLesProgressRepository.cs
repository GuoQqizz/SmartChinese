using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StuLesProgressRepository : RepositoryBase<Yw_StudentLessonProgress>, IStuLesProgressRepository
    {
        public StuLesProgressRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_StudentLessonProgress Get(int id)
        {
            Yw_StudentLessonProgress entity = GetEntity(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        /// <summary>
        /// 获取学生完成的课时id数据
        /// </summary>
        /// <param name="studnetId">学生id</param>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        public List<int> GetFinishedByLesson(int studnetId, int courseId)
        {
            return Query<int>("select distinct [Yle_LessonId] from Yw_StudentLessonProgress where [Yle_StudentId] = @studentid and [Yle_CourseId] = @courseid and [Yle_IsFinished] = 1", new { studentid = studnetId, courseid = courseId }).ToList();
        }

        /// <summary>
        /// 获取最后一次的学习数据
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public Yw_StudentLessonProgress GetLastProgress(int studentId, int lessonid)
        {
            var p = Query("select top(1) * from Yw_StudentLessonProgress where  [Yle_StudentId] = @studentid and [Yle_LessonId] = @lessonid order by [Yle_CreateTime] desc", new { studentid = studentId, lessonid = lessonid }).FirstOrDefault();
            if (p != null)
            {
                p.EnableAudit();
            }
            return p;
        }

        /// <summary>
        /// 根据课时进度id获取学习进度数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Yw_StudentLessonProgress GetProgressByID(int id)
        {
            var p = GetEntity(id);
            if (p != null)
            {
                p.EnableAudit();
            }
            return p;
        }

        /// <summary>
        /// 添加课时学习进度
        /// </summary>
        /// <param name="slp"></param>
        public void Insert(Yw_StudentLessonProgress slp)
        {
            if (slp.Yle_FinishStudyTime == new DateTime())
            {
                slp.Yle_FinishStudyTime = new DateTime(1900, 1, 1);
            }
            if (slp.Yle_LastStudyTime == new DateTime())
            {
                slp.Yle_LastStudyTime = new DateTime(1900, 1, 1);
            }
            if (slp.Yle_StartStudyTime == new DateTime())
            {
                slp.Yle_StartStudyTime = new DateTime(1900, 1, 1);
            }
            slp.Yle_CreateTime = slp.Yle_UpdateTime = DateTime.Now;
            InsertEntity(slp);
            slp.EnableAudit();
        }

        /// <summary>
        /// 更新学生课程
        /// </summary>
        /// <param name="progress"></param>
        public void Update(Yw_StudentLessonProgress progress)
        {
            if (progress.Yle_FinishStudyTime == new DateTime())
            {
                progress.Yle_FinishStudyTime = new DateTime(1900, 1, 1);
            }
            if (progress.Yle_LastStudyTime == new DateTime())
            {
                progress.Yle_LastStudyTime = new DateTime(1900, 1, 1);
            }
            if (progress.Yle_StartStudyTime == new DateTime())
            {
                progress.Yle_StartStudyTime = new DateTime(1900, 1, 1);
            }
            progress.Yle_UpdateTime = DateTime.Now;
            UpdateEntity(progress);
        }

        /// <summary>
        /// 修改课时学习进度秘钥
        /// </summary>
        /// <param name="id">学习进度id</param>
        public string UpdateKey(int id)
        {
            var p = GetEntity(id);
            p.Yle_key = Guid.NewGuid().ToString("N").ToLower();
            UpdateEntity(p);
            return p.Yle_key;
        }

        public List<DtoStudentReportList> GetStudentReport(PagingObject paging,int studentId)
        {
            var sql = @"(select Yle_Id as Id,1 as ReportType,Yle_LessonIndex as LessonIndex,Yle_LastStudyTime as FinishStudyTime,Ycs_Name,Ycl_Name from Yw_StudentLessonProgress 
                            inner join Yw_Course on Yle_CourseId=Ycs_Id 
                            inner join Yw_CourseLesson on Yle_LessonId=Ycl_Id where Yle_StudentId=@StudentId and Yle_IsFinished=1
                        union all
                        select Yuk_Id as Id,case Yuk_TaskType when 1 then 2 when 2 then 2 when 3 then 3 end as ReportType,Yuk_LessonIndex as LessonIndex,Yuk_FinishTime as FinishStudyTime,Ycs_Name,Ycl_Name from Yw_StudentTask 
                            inner join Yw_Course on Yuk_CourseId = Ycs_Id 
                            left join Yw_CourseLesson on Yuk_LessonId = Ycl_Id where Yuk_StudentId = @StudentId and Yuk_Status = 3)newTable";
            return QueryPaging<DtoStudentReportList>("*",sql,"FinishStudyTime DESC",paging, new { StudentId = studentId }).ToList();
        }

        public List<DtoStudentReportList> GetStuCourseReport(PagingObject paging, int studentId)
        {
            var sql = @"Yw_StudentLessonProgress 
                            inner join Yw_Course on Yle_CourseId = Ycs_Id
                            inner join Yw_CourseLesson on Yle_LessonId = Ycl_Id where Yle_StudentId = @StudentId and Yle_IsFinished = 1";
            return QueryPaging<DtoStudentReportList>("Yle_Id as Id,1 as ReportType,Yle_LessonIndex as LessonIndex,Yle_LastStudyTime as FinishStudyTime,Ycs_Name,Ycl_Name", sql, "Yle_LastStudyTime DESC", paging, new { StudentId = studentId }).ToList();
        }

        public DtoStudentReportList GetStuCourseReportById(int lessonProcessId)
        {
            var sql = @"select Yle_Id as Id,1 as ReportType,Yle_LessonIndex as LessonIndex,Yle_LastStudyTime as FinishStudyTime,Ycs_Name,Ycl_Name from Yw_StudentLessonProgress 
                            inner join Yw_Course on Yle_CourseId=Ycs_Id 
                            inner join Yw_CourseLesson on Yle_LessonId=Ycl_Id where Yle_Id=@Id";// and Yle_IsFinished=1
            return QueryObject<DtoStudentReportList>(sql, new { Id = lessonProcessId }).FirstOrDefault();
        }

        public int StuCourseReportCount(int studentId)
        {
            var sql = @"select count(1) from Yw_StudentLessonProgress where Yle_StudentId = @StudentId and Yle_IsFinished = 1";
            return ExecuteScalar<int>(sql, new { StudentId = studentId });
        }

        public bool IsHasStudy(int id, int studentId)
        {
            string sql = "select count(1) from Yw_StudentLessonProgress where Yle_Id=@Id and Yle_StudentId=@StudentId ";//and Yle_IsFinished=1
            return ExecuteScalar<int>(sql, new { Id = id, StudentId = studentId }) > 0;
        }

        /// <summary>
        /// 演示使用，正式上线删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Yw_StudentLessonProgress IsHasStudys(int id, int studentId)
        {
            string sql = "select * from Yw_StudentLessonProgress where Yle_Id=@Id and Yle_StudentId=@StudentId ";//and Yle_IsFinished=1
            return Query(sql, new { Id = id, StudentId = studentId }).FirstOrDefault();
        }
    }
}
