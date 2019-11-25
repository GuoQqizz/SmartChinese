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
    /// 课时单元(讲义)处理流程表操作仓储接口
    /// </summary>
    public interface ICourseLessonUnitProcessRepository : IRepositoryBase<Yw_CourseLessonUnitProcess>
    {
        /// <summary>
        /// 添加课时单元处理流程
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        int Insert(Yw_CourseLessonUnitProcess process);
        /// <summary>
        /// 修改课时单元流程数据
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        int Update(Yw_CourseLessonUnitProcess process);

        /// <summary>
        /// 通过单元id和审批id查找操作处理流程数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="processid"></param>
        /// <returns></returns>
        Yw_CourseLessonUnitProcess SelectByUnitIdAndProcessID(int unitid,int processid);

        /// <summary>
        /// 通过单元id查找所有的操作处理流程数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        List<Yw_CourseLessonUnitProcess> SelectByUnitID(int unitid);
        /// <summary>
        /// 通过审批流程id查找所有的操作处理流程数据
        /// </summary>
        /// <param name="processid">审批流程id</param>
        /// <returns></returns>
        List<Yw_CourseLessonUnitProcess> SelectByProcessID(int processid);

        /// <summary>
        /// 通过课程id查找最有一次审批的所有操作处理流程数据
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns></returns>
        List<Yw_CourseLessonUnitProcess> SelectByLessonID(int lessonid);
    }
}
