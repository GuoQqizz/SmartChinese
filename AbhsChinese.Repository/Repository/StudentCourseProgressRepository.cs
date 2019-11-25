using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Dto.Request;
using StudentPractice = AbhsChinese.Domain.Dto.Response.StudentPractice;

namespace AbhsChinese.Repository.Repository
{
    public class StudentCourseProgressRepository : RepositoryBase<Yw_StudentCourseProgress>, IStudentCourseProgressRepository
    {
        public StudentCourseProgressRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_StudentCourseProgress Get(int id)
        {
            var result = GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }

        public Yw_StudentCourseProgress GetByStudentCourse(int studentId, int courseId)
        {
            string sql = "select * from Yw_StudentCourseProgress where yps_studentId = @StudentId and yps_courseId = @CourseId and Yps_Status=1";
            var result = Query(sql, new { StudentId = studentId, CourseId = courseId }).FirstOrDefault();
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }

        /// <summary>
        /// 获取学生课程详情
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        public DtoStudentCourseInfo GetByStudentCourseId(int studentId, int courseId)
        {
            string sql = $@"select c.Ycs_CourseType  as [CourseType]
 ,cp.Yps_CourseId as [CourseID]
 ,c.Ycs_Name as [CourseName]
 ,c.Ycs_LessonCount as [LessonsNum]
 ,cp.Yps_LessonFinishedCount as [LessonsFinishedNum]
 ,cp.Yps_NextLessonIndex as [NextLessonNum]
 ,cp.Yps_StartStudyTime as [CourseStartTime]
 ,cp.Yps_IsFinished as [CourseFinshed]
 ,c.Ycs_Grade as [Grade]
 ,cp.Yps_SchoolId as [SchoolID]
 --,s.Bsl_SchoolName as [SchoolName]
 ,cp.Yps_ClassId as [ClassID]
 ,sc.Ycc_Name as [ClassName]
 ,sc.Ycc_ClassMaster as [TeacherID]
 ,st.Yoh_Name as [TeacherName]
 ,st.Yoh_Avatar as [TeacherImg] from 
 Yw_StudentCourseProgress as cp
 left join Yw_Course as c on cp.Yps_CourseId = c.Ycs_Id 
 --left join Bas_School as s on cp.Yps_SchoolId = s.Bsl_Id
 left join Yw_SchoolClass as sc on cp.Yps_ClassId = sc.Ycc_Id
 left join Yw_SchoolTeacher as st on sc.Ycc_ClassMaster = st.Yoh_Id
where cp.yps_studentId = @StudentId and cp.Yps_CourseId = @courseId and cp.Yps_Status=1
";
            return Query<DtoStudentCourseInfo>(sql, new { StudentId = studentId, courseId = courseId }).FirstOrDefault();
        }
        /// <summary>
        /// 根据条件获取学生的课程分页数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoStudentCourseInfo> GetByStudentCourseSearch(DtoStudentCourseSearch search)
        {
            string fields = @" c.Ycs_CourseType  as [CourseType]
 ,cp.Yps_CourseId as [CourseID]
 ,c.Ycs_Name as [CourseName]
 ,c.Ycs_LessonCount as [LessonsNum]
 ,cp.Yps_LessonFinishedCount as [LessonsFinishedNum] 
 ,cp.Yps_NextLessonIndex as [NextLessonNum]
 ,cp.Yps_StartStudyTime as [CourseStartTime]
 ,cp.Yps_IsFinished as [CourseFinshed] 
 ,c.Ycs_Grade as [Grade]
 ,cp.Yps_SchoolId as [SchoolID]
 --,s.Bsl_SchoolName as [SchoolName]
 ,cp.Yps_ClassId as [ClassID]
 ,sc.Ycc_Name as [ClassName]
 ,sc.Ycc_ClassMaster as [TeacherID]
 ,st.Yoh_Name as [TeacherName]
 ,st.Yoh_Avatar as [TeacherImg] ";
            string where = @"Yw_StudentCourseProgress as cp
 left join Yw_Course as c on cp.Yps_CourseId = c.Ycs_Id 
 --left join Bas_School as s on cp.Yps_SchoolId = s.Bsl_Id
 left join Yw_SchoolClass as sc on cp.Yps_ClassId = sc.Ycc_Id
 left join Yw_SchoolTeacher as st on sc.Ycc_ClassMaster = st.Yoh_Id
where cp.yps_studentId = @StudentId and cp.Yps_IsFinished = @IsFinished and cp.Yps_Status=1";
            string orderby = " cp.Yps_CreateTime desc";
            return QueryPaging<DtoStudentCourseInfo>(fields, where, orderby, search.Pagination, new { StudentId = search.StudentId, IsFinished = search.IsFinished }).ToList();
        }

        public List<DtoSchoolNoClassStudent> GetNoClassStudent(DtoSchoolNoClassStudentSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "scp.Yps_Id,scp.Yps_StudentId,scp.Yps_OrderId,scp.Yps_CourseId,s.Bst_No,s.Bst_Name,s.Bst_Grade,s.Bst_Phone,c.Ycs_Name,c.Ycs_CourseType,so.Yod_OrderNo,so.Yod_OrderTime ";
            var orderBy = "scp.Yps_Id ";
            var parameters = new DynamicParameters();
            //todo  Yw_StudentOrder表 现在测试用LEFT  正式用 JOIN

            strWhere.Append($@"dbo.Yw_StudentCourseProgress scp
                                    JOIN dbo.Bas_Student s ON scp.Yps_StudentId = s.Bst_Id
                                     JOIN dbo.Yw_StudentOrder so ON scp.Yps_OrderId=so.Yod_Id
                                    JOIN dbo.Yw_Course c ON scp.Yps_CourseId=c.Ycs_Id
                                    WHERE 1=1   AND scp.Yps_ClassId=0");

            if (search.Grade > 0)
            {
                strWhere.Append(" AND s.Bst_Grade=@Bst_Grade ");
                parameters.Add("Bst_Grade", search.Grade);
            }
            if (search.SchoolId > 0)
            {
                strWhere.Append(" AND scp.Yps_SchoolId=@Yps_SchoolId ");
                parameters.Add("Yps_SchoolId", search.SchoolId);
            }
            if (search.CourseType > 0)
            {
                strWhere.Append(" AND c.Ycs_CourseType=@CourseType ");
                parameters.Add("CourseType", search.CourseType);
            }
            if (search.StudentNo.HasValue())
            {
                strWhere.Append(" AND s.Bst_No=@Bst_No ");
                parameters.Add("Bst_No", search.StudentNo);
            }
            if (search.SearchStr.HasValue())
            {
                strWhere.Append(" AND (s.Bst_Name LIKE @Bst_Name OR s.Bst_Phone LIKE @Bst_Phone) ");
                parameters.Add("Bst_Name", $"%{search.SearchStr}%");
                parameters.Add("Bst_Phone", $"%{search.SearchStr}%");
            }
            return base.QueryPaging<DtoSchoolNoClassStudent>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();
        }


        public bool UpdateClassId(int ypsId, int classId)
        {
            string sql = "UPDATE dbo.Yw_StudentCourseProgress SET Yps_ClassId=@Yps_ClassId ,Yps_UpdateTime=GETDATE() WHERE Yps_Id=@Yps_Id";

            return Execute(sql, new
            {
                Yps_ClassId = classId,
                Yps_Id = ypsId
            }) > 0;
        }

        /// <summary>
        /// 更新最后学习时间
        /// </summary>
        /// <param name="ypsId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>

        public bool UpdateStudyTime(int ypsId, DateTime dt)
        {
            var p = GetEntity(ypsId);
            if (p != null)
            {
                p.EnableAudit();
                if (p.Yps_StartStudyTime == new DateTime(1900, 1, 1))
                {
                    p.Yps_StartStudyTime = dt;
                }
                p.Yps_UpdateTime = dt;
                p.Yps_LastStudyTime = dt;
                return UpdateEntity(p);
            }
            return false;
        }

        /// <summary>
        /// 获取上过课的课程
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<StudentPractice.DtoCourse> GetCoursesAttended(DtoCoursesSearch search)
        {
            StringBuilder fieldBuilder = new StringBuilder();
            fieldBuilder.Append("Ycs_Name AS CourseName");
            fieldBuilder.Append(", Ycs_CourseType AS CourseType");
            fieldBuilder.Append(", Yps_CourseId AS CourseId");
            fieldBuilder.Append(", Yps_NextLessonIndex AS NextLessonIndex");
            fieldBuilder.Append(", Ycs_Grade AS Grade");
            fieldBuilder.Append(", Ycc_Name AS ClassName");
            fieldBuilder.Append(", Bsl_SchoolName AS SchoolName");
            fieldBuilder.Append(", Ycs_LessonCount AS LessonCount");

            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("Yw_StudentCourseProgress");
            sqlBuilder.Append(" INNER JOIN Yw_Course ON Ycs_Id = Yps_CourseId");
            sqlBuilder.Append(" LEFT JOIN Yw_SchoolClass ON Ycc_Id = Yps_ClassId");
            sqlBuilder.Append(" LEFT JOIN Bas_School ON Bsl_Id = Yps_SchoolId");

            sqlBuilder.Append(" WHERE Yps_LessonFinishedCount > 0");//过滤上过课的课程
            if (search.StudentId > 0)
            {
                sqlBuilder.Append(" AND Yps_StudentId = @studentId");
            }

            string orderby = "Yps_CreateTime DESC";
            var parameters = new
            {
                studentId = search.StudentId
            };
            //return Query<StudentPractice.DtoCourse>(
            //    sqlBuilder.ToString(),
            //    new { studentId = search.StudentId }).ToList();

            return QueryPaging<StudentPractice.DtoCourse>(
                fieldBuilder.ToString(),
                 sqlBuilder.ToString(),
                orderby,
                search.Pagination,
                parameters).ToList();
        }

        /// <summary>
        /// 更近课时进度信息
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public bool Update(Yw_StudentCourseProgress progress)
        {
            return UpdateEntity(progress);
        }

        /// <summary>
        /// 根据学生id获取学生拥有的课时数量
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <returns>第一个值:总课程数,第二个值:未完成课程数,第三个值:完成课程数</returns>
        public Tuple<int, int, int> GetCourseCountByStudnet(int studentId)
        {
            string sql = "select Yps_IsFinished as Finish,count(1) as Count from [Yw_StudentCourseProgress] where Yps_StudentId = @studentId group by Yps_IsFinished";
            var data = QueryObject<FinishData>(sql,new { studentId = studentId}).ToList();
            int not = data.Where(s=>!s.Finish).Sum(s=>s.Count), finish = data.Where(s => s.Finish).Sum(s => s.Count);
            int total = not + finish;
            return new Tuple<int, int, int>(total, not, finish);
        }
        private class FinishData
        {
            public bool Finish { get; set; }
            public int Count { get; set; }
        }
    }
}
