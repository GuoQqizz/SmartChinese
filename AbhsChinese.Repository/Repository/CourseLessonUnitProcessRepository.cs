using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AbhsChinese.Repository.Repository
{
    /// <summary>
    /// 课时单元(讲义)处理流程表操作仓储
    /// </summary>
    public class CourseLessonUnitProcessRepository : RepositoryBase<Yw_CourseLessonUnitProcess>, ICourseLessonUnitProcessRepository
    {
        public CourseLessonUnitProcessRepository() : base(AppSetting.ConnString)
        {
        }

        /// <summary>
        /// 添加课时单元处理流程
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public int Insert(Yw_CourseLessonUnitProcess process)
        {
            process.Yup_CreateTime = DateTime.Now;
            return base.InsertEntity(process);
        }
        /// <summary>
        /// 根据课程id获取最后一次的单元审批信息
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public List<Yw_CourseLessonUnitProcess> SelectByLessonID(int lessonid)
        {
            return Query("select * from Yw_CourseLessonUnitProcess where [Yup_LessonProcessId] = (select top(1) Ylp_Id from Yw_CourseLessonProcess where [Ylp_LessonId]= @lessonid and Ylp_Status=@status order by [Ylp_CreateTime] desc)", new { lessonid = lessonid, status = (int)LessonStatusEnum.审批中 }).ToList();
        }

        /// <summary>
        /// 通过审批流程id查找所有的操作处理流程数据
        /// </summary>
        /// <param name="processid">审批流程id</param>
        public List<Yw_CourseLessonUnitProcess> SelectByProcessID(int processid)
        {
            return Query("select * from Yw_CourseLessonUnitProcess where [Yup_LessonProcessId] = @processid", new { processid = processid }).ToList();
        }

        /// <summary>
        /// 通过单元id查找最后一个操作处理流程数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public List<Yw_CourseLessonUnitProcess> SelectByUnitID(int unitid)
        {
            return Query("select * from Yw_CourseLessonUnitProcess where Yup_UnitId = @unitid order by Ylp_CreateTime asc ", new { unitid = unitid }).ToList();
        }

        /// <summary>
        /// 通过单元id和审批id查找操作处理流程数据
        /// </summary>
        /// <param name="unitid">单元id</param>
        /// <param name="processid">审批id</param>
        /// <returns></returns>
        public Yw_CourseLessonUnitProcess SelectByUnitIdAndProcessID(int unitid, int processid)
        {
            var process = Query("select * from Yw_CourseLessonUnitProcess where Yup_UnitId = @unitid and Yup_LessonProcessId = @processid ", new { unitid = unitid, processid = processid }).FirstOrDefault();
            if (process != null)
            {
                process.EnableAudit();
            }
            return process;
        }
        /// <summary>
        /// 修改审批信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public int Update(Yw_CourseLessonUnitProcess process)
        {
            process.Yup_CreateTime = DateTime.Now;
            return base.UpdateEntity(process) ? 1 : 0;
        }
    }
}
