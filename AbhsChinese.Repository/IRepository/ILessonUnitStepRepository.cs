using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity.UnitStep;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ILessonUnitStepRepository : IRepositoryBase<Yw_LessonUnitStep>
    {
        /// <summary>
        /// 根据课时id获取全部的单元(讲义)
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns></returns>
        List<Yw_LessonUnitStep> SelectUnits(int lessonid);

        /// <summary>
        /// 根据课时id获取后几条的单元(讲义)
        /// </summary>
        /// <param name="search">条件</param>
        /// <returns></returns>
        /// <returns></returns>
        List<Yw_LessonUnitStep> SelectUnitsByNext(DtoLessonUnitSearch search);
        /// <summary>
        /// 获取课时信息
        /// </summary>
        /// <param name="pageid">单元(讲义)id</param>
        /// <returns></returns>
        Yw_LessonUnitStep SelectSteps(int pageid);
        /// <summary>
        /// 更新课时步骤信息
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        bool UpdateSteps(Yw_LessonUnitStep steps);
        /// <summary>
        /// 插入课时步骤信息
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        bool InsertSteps(Yw_LessonUnitStep steps);

        /// <summary>
        /// 返回课时中所有的题目
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns>返回课时中的所有题目数据</returns>
        List<int> SelectLessonQuestions(int lessonid);
    }
}
