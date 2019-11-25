using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Repository.Repository
{
    /// <summary>
    /// 课时表操作仓储
    /// </summary>
    public class CourseLessonRepository : RepositoryBase<Yw_CourseLesson>, ICourseLessonRepository
    {
        public CourseLessonRepository() : base(AppSetting.ConnString)
        {
        }

        /// <summary>
        /// 添加课时数据
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public int Insert(Yw_CourseLesson lesson)
        {
            lesson.Ycl_CreateTime = DateTime.Now;
            return base.InsertEntity(lesson);
        }

        /// <summary>
        /// 批量添加课时数据
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <param name="names">课时名称</param>
        /// <param name="fromIndex">开始序号</param>
        /// <param name="creator">创建人id</param>
        /// <returns></returns>
        public int Insert(int courseId, List<string> names, int fromIndex, int creator)
        {
            int i = 0;
            foreach (string name in names)
            {
                var lesson = new Yw_CourseLesson()
                {
                    Ycl_CourseId = courseId,
                    Ycl_Index = fromIndex++,
                    Ycl_Name = name,
                    Ycl_Status = (int)LessonStatusEnum.未编辑,
                    Ycl_UnitCount = 0,
                    Ycl_Creator = creator,
                    Ycl_CreateTime = DateTime.Now,
                    Ycl_Editor = creator,
                    Ycl_UpdateTime = DateTime.Now
                };
                i += base.InsertEntity(lesson);
                string sql = "insert into Yw_CourseLessonProcess ([Ylp_CourseId],[Ylp_LessonId],[Ylp_Action],[Ylp_Status],[Ylp_Operator],[Ylp_Remark],[Ylp_CreateTime]) values (@courseId,@lessonId,@action,@status,@creator,@remark,@createTime)";
                base.Execute(sql, new
                {
                    courseId = lesson.Ycl_CourseId,
                    lessonId = lesson.Ycl_Id,
                    action = (int)LessonStatusActionEnum.创建,
                    status = (int)LessonStatusEnum.未编辑,
                    creator = lesson.Ycl_Creator,
                    remark = "创建课时“" + lesson.Ycl_Name + "”",
                    createTime = lesson.Ycl_CreateTime
                });
            }
            return i;
        }

        /// <summary>
        /// 查询课程下的所有课时
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        public List<Yw_CourseLesson> SelectByCourse(int courseId)
        {
            return base.Query("select * from Yw_CourseLesson where  Ycl_CourseId = @courseid order by Ycl_Index asc", new { courseid = courseId }).ToList();
        }

        /// <summary>
        /// 查询课程下的所有课时(包含处理流程)
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        public IList<DtoLesson> GetLessonsWithProcessInfo(int courseId)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT [Ycl_Id] as [ID]");
            builder.Append("      ,[Ycl_CourseId] as [CourseId]");
            builder.Append("      ,[Ycl_Name] as [Name]");
            builder.Append("      ,[Ycl_Index] as [Index]");
            builder.Append("      ,[Ycl_Status] as [Status]");
            //builder.Append("	  ,(select top(1) [Ylp_Id] from[Yw_CourseLessonProcess] where[Ylp_LessonId] = [Yw_CourseLesson].[Ycl_Id]  and Ylp_Status=@status order by[Ylp_CreateTime] desc ) as [ProcessId]");
            builder.Append("      ,[Ycl_UnitCount] as [UnitCount]");
            builder.Append("      ,[Ycl_Producer] as [Producer]");
            builder.Append("      ,[Ycl_Approver] as [Approver]");
            builder.Append("      ,[Ycl_CreateTime] as [CreateTime]");
            builder.Append("      ,[Ycl_Creator] as [Creator]");
            builder.Append("      ,[Ycl_UpdateTime] as [UpdateTime]");
            builder.Append("      ,[Ycl_Editor] as [Editor]");
            builder.Append("FROM [Yw_CourseLesson] where [Ycl_CourseId] = @courseId order by [Index] asc");
            string sql = builder.ToString();
            return base.Query<DtoLesson>(sql, new { courseId = courseId }).ToList();
        }

        /// <summary>
        /// 根据课程id获取课程信息(包含审批id)
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public DtoLesson SelectByLessonId(int lessonId)
        {
            return base.Query<DtoLesson>(@"SELECT [Ycl_Id] as [ID]
      ,[Ycl_CourseId] as [CourseId]
      ,[Ycl_Name] as [Name]
      ,[Ycl_Index] as [Index]
      ,[Ycl_Status] as [Status]
      ,(select top(1) Ylp_Id from Yw_CourseLessonProcess where [Ylp_LessonId]= [Ycl_Id] and Ylp_Status=@status order by [Ylp_CreateTime] desc) as [ProcessId]
      ,[Ycl_UnitCount] as [UnitCount]
      ,[Ycl_Producer] as [Producer]
      ,[Ycl_Approver] as [Approver]
      ,[Ycl_CreateTime] as [CreateTime]
      ,[Ycl_Creator] as [Creator]
      ,[Ycl_UpdateTime] as [UpdateTime]
      ,[Ycl_Editor] as [Editor]
  FROM[SmartChinese].[dbo].[Yw_CourseLesson] where [Ycl_Id] = @lessonId", new { lessonId = lessonId, status = (int)LessonStatusEnum.审批中 }).FirstOrDefault();
        }
        /// <summary>
        /// 根据课程id获取课程信息
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public Yw_CourseLesson Select(int lessonid)
        {
            var lesson = base.GetEntity(lessonid);
            if (lesson != null)
            {
                lesson.EnableAudit();
            }
            return lesson;
        }

        /// <summary>
        /// 更新课时数据
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public int Update(Yw_CourseLesson lesson)
        {
            return base.UpdateEntity(lesson) ? 1 : 0;
        }

        /// <summary>
        /// 根据查询条件获取审批分页数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<DtoLessonApprove> GetApproveLessonByPage(DtoApproveLessonSearch search)
        {
            Check.IfNull(search, nameof(search));
            string fields = @" course.Ycs_Id as [CourseID]
,course.Ycs_Name as [CourseName]
,course.Ycs_Grade as [Grade]
,course.Ycs_CourseType as [CourseType]
,lesson.Ycl_Id as [LessonID]
,lesson.Ycl_Index as [LessonIndex]
,lesson.Ycl_Name as [LessonName]
,lesson.Ycl_Producer as [LessonProducer]
,employee.Bem_Name as [LessonProducerName]
,lesson.Ycl_Status as [LessonStatus]
,lesson.Ycl_UpdateTime as [UpdateDate] ";
            string from = $@" Yw_CourseLesson as lesson
inner join Yw_Course  as course on lesson.Ycl_CourseId = course.Ycs_Id
left join Bas_Employee as employee on lesson.Ycl_Producer = employee.Bem_Id
where (lesson.Ycl_Status = {(int)LessonStatusEnum.待审批} or lesson.Ycl_Status = {(int)LessonStatusEnum.审批中}) ";
            string where = from;
            if (search.CourseID != 0)
            {
                where += " and course.Ycs_Id=@courseId ";
            }
            if (search.CourseType != 0)
            {
                where += " and course.Ycs_CourseType = @courseType ";
            }
            if (search.Grade != 0)
            {
                where += " and course.Ycs_Grade = @grade ";
            }
            if (search.LessonIndex != 0)
            {
                where += " and lesson.Ycl_Index = @index  ";
            }
            if (!string.IsNullOrEmpty(search.LessonOrProducerName))
            {
                where += " and (lesson.Ycl_Name like @name or employee.Bem_Name like @name) ";
            }

            string orderby = " lesson.Ycl_UpdateTime desc ";
            object param = new
            {
                courseId = search.CourseID,
                courseType = search.CourseType,
                grade = search.Grade,
                index = search.LessonIndex,
                name =  $"{search.LessonOrProducerName}%"
            };
            return base.QueryPaging<DtoLessonApprove>(
                fields,
                where,
                orderby,
                search.Pagination,
                param).ToList();
        }

        /// <summary>
        /// 查询课程下的课时数
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        public int SelectCountByCourse(int courseId)
        {
            return Query<int>("select count(1) from Yw_CourseLesson where [Ycl_CourseId] = @courseId",new { courseId = courseId }).FirstOrDefault();
        }
    }
}












