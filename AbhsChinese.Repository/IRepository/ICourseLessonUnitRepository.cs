using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICourseLessonUnitRepository : IRepositoryBase<Yw_CourseLessonUnit>
    {
        /// <summary>
        /// 根据课时获取单元(讲义)信息
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns></returns>
        List<Yw_CourseLessonUnit> SelectUnits(int lessonid);
        /// <summary>
        /// 查找单元(讲义)信息
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        Yw_CourseLessonUnit SelectUnit(int unitid);

        /// <summary>
        /// 添加单元(讲义)信息
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        bool InsertUnits(Yw_CourseLessonUnit unit);

        /// <summary>
        /// 修改单元(讲义)信息
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        bool UpdateUnits(Yw_CourseLessonUnit unit);

        bool MoveUnits(int unitid, int toIndex);
        /// <summary>
        /// 根据单元id删除数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        bool DeleteUnits(int unitid);
    }
}
