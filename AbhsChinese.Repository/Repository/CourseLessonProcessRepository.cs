using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    /// <summary>
    /// 课时处理流程表操作仓储
    /// </summary>
    public class CourseLessonProcessRepository : RepositoryBase<Yw_CourseLessonProcess>, ICourseLessonProcessRepository
    {
        public CourseLessonProcessRepository() : base(AppSetting.ConnString)
        {
        }

        /// <summary>
        /// 添加课时审批数据
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public int Insert(Yw_CourseLessonProcess process)
        {
            process.Ylp_CreateTime = DateTime.Now;
            base.Execute($"update Yw_Course set [Ycs_ApprovalLesCount] = (select count(1) from [Yw_CourseLesson] where [Ycl_CourseId] = @courseid and [Ycl_Status] = {(int)LessonStatusEnum.合格}) where  [Ycs_Id] = @courseid", new {
                courseid = process.Ylp_CourseId
            });//更新课程表中合格课时数的值
            return base.InsertEntity(process);
        }

        /// <summary>
        /// 通过课时id查看最后一个审批历史数据
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public Yw_CourseLessonProcess SelectLastProcessByLesson(int lessonId)
        {
            return base.Query("select top(1) * from Yw_CourseLessonProcess where Ylp_LessonId = @lessonid order by Ylp_CreateTime desc", new { lessonid = lessonId }).FirstOrDefault();
        }
        /// <summary>
        /// 根据课程id查找历史数据
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Yw_CourseLessonProcess> SelectProcessByCourseByPage(int courseId, DtoSearch search)
        {
            return base.QueryPaging<Yw_CourseLessonProcess>("*", "Yw_CourseLessonProcess where [Ylp_CourseId] = @courseid", "Ylp_CreateTime desc", search.Pagination, new { courseid = courseId }).ToList();
        }

        /// <summary>
        /// 通过课时id查询审批历史数据
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<Yw_CourseLessonProcess> SelectProcessByLesson(int lessonId)
        {
            return base.Query("select * from Yw_CourseLessonProcess where Ylp_LessonId = @lessonid order by Ylp_CreateTime desc", new { lessonid = lessonId }).ToList();
        }

    }
}
