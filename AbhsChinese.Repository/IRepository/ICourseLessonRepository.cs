using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
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
    /// 课时表操作仓储接口
    /// </summary>
    public interface ICourseLessonRepository : IRepositoryBase<Yw_CourseLesson>
    {
        /// <summary>
        /// 添加课时数据
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        int Insert(Yw_CourseLesson lesson);
        /// <summary>
        /// 批量添加课时数据
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <param name="names">课时名称</param>
        /// <param name="fromIndex">开始序号</param>
        /// <param name="creator">创建人id</param>
        /// <returns></returns>
        int Insert(int courseId, List<string> names,int fromIndex, int creator);
        /// <summary>
        /// 查询课程下的所有课时
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        List<Yw_CourseLesson> SelectByCourse(int courseId);

        /// <summary>
        /// 查找课程下的课时数量
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        int SelectCountByCourse(int courseId);
        /// <summary>
        /// 根据课程id获取课程信息(包含审批id)
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        DtoLesson SelectByLessonId(int lessonId);
        /// <summary>
        /// 根据课程id获取课程信息
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        Yw_CourseLesson Select(int lessonid);
        /// <summary>
        /// 更新课时数据
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        int Update(Yw_CourseLesson lesson);
        /// <summary>
        /// 通过课时id查询课时信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        IList<DtoLesson> GetLessonsWithProcessInfo(int courseId);
        /// <summary>
        /// 根据查询条件获取审批分页数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<DtoLessonApprove> GetApproveLessonByPage(DtoApproveLessonSearch search);
    }
}
