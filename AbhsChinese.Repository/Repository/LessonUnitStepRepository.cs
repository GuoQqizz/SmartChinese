using AbhsChinese.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity.UnitStep;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;

namespace AbhsChinese.Repository.Repository
{
    public class LessonUnitStepRepository : RepositoryBase<Yw_LessonUnitStep>, ILessonUnitStepRepository
    {
        public LessonUnitStepRepository() : base(AppSetting.ConnString) { }

        /// <summary>
        /// 根据课时id获取全部的单元(讲义)
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns></returns>
        public List<Yw_LessonUnitStep> SelectUnits(int lessonid)
        {
            List<Yw_LessonUnitStep> units = base.Query("select * from [Yw_LessonUnitStep] where [Yls_LessonId]=@lessonid and Yls_Status = 1 order by [Yls_UnitIndex] asc ", new { lessonid = lessonid }).ToList();

            return units;
        }

        /// <summary>
        /// 根据课时id获取后几条的单元(讲义)
        /// </summary>
        /// <param name="search">条件</param>
        /// <returns></returns>
        public List<Yw_LessonUnitStep> SelectUnitsByNext(DtoLessonUnitSearch search)
        {
            string countSql = "select max([Yls_UnitIndex]) from [Yw_LessonUnitStep] where [Yls_LessonId]=@lessonid and [Yls_CourseId]=@courseid and Yls_Status = 1";
            string sql = $"select top({search.Pagination.PageSize})* from [Yw_LessonUnitStep] where [Yls_LessonId]=@lessonid  and [Yls_CourseId]=@courseid and Yls_Status = 1 and Yls_UnitIndex >= {search.Pagination.PageIndex} order by [Yls_UnitIndex] asc ";
            var paras = new { courseid = search.CourseID, lessonid = search.LessonID };
            search.Pagination.TotalCount = Query<int>(countSql, paras).FirstOrDefault();
            return Query(sql, paras).ToList();



        }

        /// <summary>
        /// 插入步骤数据
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public bool InsertSteps(Yw_LessonUnitStep steps)
        {
            steps.Yls_CreateTime = steps.Yls_UpdateTime = DateTime.Now;
            return base.InsertEntity(steps) == 1;
        }

        /// <summary>
        /// 根据单元(讲义页)id查询步骤信息
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public Yw_LessonUnitStep SelectSteps(int pageid)
        {
            Yw_LessonUnitStep page = base.GetEntity(pageid);
            if (page != null)
            {
                page.EnableAudit();
            }
            return page;
        }

        /// <summary>
        /// 更新步骤数据
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public bool UpdateSteps(Yw_LessonUnitStep steps)
        {
            steps.Yls_UpdateTime = DateTime.Now;
            return base.UpdateEntity(steps);
        }

        /// <summary>
        /// 返回课时中所有的题目
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns>返回课时中的所有题目数据</returns>
        public List<int> SelectLessonQuestions(int lessonid)
        {
            string sql = "select Yls_SubjectIds from Yw_LessonUnitStep where Yls_LessonId = @lessonid";
            List<string> idStrList = Query<string>(sql, new { lessonid = lessonid }).ToList();
            List<int> ids = new List<int>();
            if (idStrList != null)
            {
                idStrList.ForEach(s =>
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        var idStr = s.Split(',');
                        foreach (var i in idStr)
                        {
                            int id = 0;
                            if (int.TryParse(i, out id))
                            {
                                ids.Add(id);
                            }
                        }
                    }
                });
            }
            return ids.Distinct().ToList();
        }
    }
}
