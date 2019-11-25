using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class CourseLessonUnitRepository : RepositoryBase<Yw_CourseLessonUnit>, ICourseLessonUnitRepository
    {
        public CourseLessonUnitRepository() : base(AppSetting.ConnString)
        {
        }
        /// <summary>
        /// 根据id删除单元
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public bool DeleteUnits(int unitid)
        {
            Yw_CourseLessonUnit u = GetEntity(unitid);
            if (u != null && u.Ycu_Status == 1)
            {
                string sql = @"
update [Yw_CourseLessonUnit] set Ycu_Index = Ycu_Index - 1 where Ycu_LessonId = @lessonid and [Ycu_Status] = 1 and Ycu_Index > @index
update [Yw_CourseLessonUnit] set Ycu_Index = -1 ,Ycu_Status =3 where Ycu_Id = @unitid 
update [Yw_LessonUnitStep] set Yls_UnitIndex = Yls_UnitIndex + @type where Yls_LessonId = @lessonid and Yls_Status =1 and Yls_UnitIndex >@index
update [Yw_LessonUnitStep] set Yls_UnitIndex = -1 , Yls_Status=3 where Yls_unitId = @unitid
update [Yw_CourseLesson] set [Ycl_UnitCount] =(select count(1) from [Yw_CourseLessonUnit] where [Ycu_LessonId] = @lessonid and [Ycu_Status] = 1) where [Ycl_Id] = @lessonid
";

                return base.Execute(sql, new
                {
                    type = 1,//区间内的序号+1
                    lessonid = u.Ycu_LessonId,
                    unitid = u.Ycu_Id,
                    index = u.Ycu_Index
                }) > 0;

            }
            return false;
        }

        /// <summary>
        /// 插入课时单元信息
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool InsertUnits(Yw_CourseLessonUnit unit)
        {
            unit.Ycu_CreateTime = unit.Ycu_UpdateTime = DateTime.Now;
            var i = InsertEntity(unit);
            string sql = "update [Yw_CourseLesson] set [Ycl_UnitCount] = (select count(1) from [Yw_CourseLessonUnit] where [Ycu_LessonId] = @lessonid and [Ycu_Status] = 1) where [Ycl_Id] = @lessonid";
            base.Execute(sql, new { lessonid = unit.Ycu_LessonId });//在lesson中添加一个计数
            return i == 1;
        }
        /// <summary>
        /// 移动课时单元位置
        /// </summary>
        /// <param name="unitid">单元id</param>
        /// <param name="toIndex">移动到位置</param>
        /// <returns></returns>
        public bool MoveUnits(int unitid, int toIndex)
        {
            Yw_CourseLessonUnit u = GetEntity(unitid);
            if (u != null)
            {
                string sql = @"
update [Yw_CourseLessonUnit] set Ycu_Index = Ycu_Index + @type where Ycu_LessonId = @lessonid and [Ycu_Status] = 1 and Ycu_Index between @begin and @end
update [Yw_CourseLessonUnit] set Ycu_Index = @index where Ycu_Id = @unitid and  [Ycu_Status] = 1
update [Yw_LessonUnitStep] set Yls_UnitIndex = Yls_UnitIndex + @type where Yls_LessonId = @lessonid and Yls_Status =1 and Yls_UnitIndex between @begin and @end
update [Yw_LessonUnitStep] set Yls_UnitIndex = @index where Yls_unitId = @unitid and Yls_Status =1
";
                if (u.Ycu_Index > toIndex)//如果是前移
                {
                    return base.Execute(sql, new
                    {
                        type = 1,//区间内的序号+1
                        lessonid = u.Ycu_LessonId,
                        begin = toIndex,
                        end = u.Ycu_Index,
                        index = toIndex,
                        unitid = u.Ycu_Id
                    }) > 0;
                }
                else
                {
                    return base.Execute(sql, new
                    {
                        type = -1,
                        lessonid = u.Ycu_LessonId,
                        begin = u.Ycu_Index,
                        end = toIndex,
                        index = toIndex,
                        unitid = u.Ycu_Id
                    }) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 查找课时单元
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public Yw_CourseLessonUnit SelectUnit(int unitid)
        {
            Yw_CourseLessonUnit u = GetEntity(unitid);
            if (u != null)
            {
                u.EnableAudit();
            }
            return u;
        }

        /// <summary>
        /// 查找课时单元集合
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public List<Yw_CourseLessonUnit> SelectUnits(int lessonid)
        {
            return Query("select [Ycu_Id],[Ycu_CourseId],[Ycu_LessonId],[Ycu_Index],[Ycu_Name],[Ycu_Screenshot],[Ycu_Status],[Ycu_CreateTime],[Ycu_Creator],[Ycu_UpdateTime],[Ycu_Editor] from [Yw_CourseLessonUnit] where [Ycu_LessonId]=@lessonid and [Ycu_Status] = 1 order by [Ycu_Index]", new { lessonid = lessonid }).ToList();
        }

        /// <summary>
        /// 修改课时单元
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool UpdateUnits(Yw_CourseLessonUnit unit)
        {
            unit.Ycu_UpdateTime = DateTime.Now;
            return UpdateEntity(unit);
        }
    }
}
