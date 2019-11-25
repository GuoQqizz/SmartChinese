using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    /// <summary>
    /// 课时处理流程表操作仓储接口
    /// </summary>
    public interface ICourseLessonProcessRepository : IRepositoryBase<Yw_CourseLessonProcess>
    {
        /// <summary>
        /// 添加课时审批数据
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        int Insert(Yw_CourseLessonProcess process);

        /// <summary>
        /// 通过课时id查看最后一个审批数据
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        Yw_CourseLessonProcess SelectLastProcessByLesson(int lessonId);

        /// <summary>
        /// 通过课程id查看审批记录数据
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        List<Yw_CourseLessonProcess> SelectProcessByCourseByPage(int courseId, DtoSearch search);
        /// <summary>
        /// 通过课时id查看审批记录数据
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        List<Yw_CourseLessonProcess> SelectProcessByLesson(int lessonId);
    }
}
