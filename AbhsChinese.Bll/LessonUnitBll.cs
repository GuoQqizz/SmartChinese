using AbhsChinese.Code.Cache;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.UnitStep;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.UnitStep;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class LessonUnitBll : BllBase
    {
        private ICourseLessonUnitRepository lessonServer;
        private ILessonUnitStepRepository unitServer;
        private ICourseLessonUnitProcessRepository approveServer;

        public ICourseLessonUnitRepository LessonServer
        {
            get
            {
                if (lessonServer == null)
                {
                    lessonServer = new CourseLessonUnitRepository();
                }

                lessonServer.TranId = tranId;
                return lessonServer;
            }
        }

        public ILessonUnitStepRepository UnitServer
        {
            get
            {
                if (unitServer == null)
                {
                    unitServer = new LessonUnitStepRepository();
                }

                unitServer.TranId = tranId;
                return unitServer;
            }
        }

        public ICourseLessonUnitProcessRepository ApproveServer
        {
            get
            {
                if (approveServer == null)
                {
                    approveServer = new CourseLessonUnitProcessRepository();
                }

                approveServer.TranId = tranId;
                return approveServer;
            }
        }
        /// <summary>
        /// 添加单元(讲义)数据
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>讲义id</returns>
        public int Add(DtoLessonUnit unit)
        {
            var lessonUnit = new Yw_CourseLessonUnit()
            {
                Ycu_CourseId = unit.CourseId,
                Ycu_LessonId = unit.LessonId,
                Ycu_Index = unit.Index,
                Ycu_Name = unit.Name,
                Ycu_Screenshot = unit.Screenshot,
                Ycu_Status = 1,
                Ycu_Creator = unit.Creator
            };
            LessonServer.InsertUnits(lessonUnit);
            UnitServer.InsertSteps(new Yw_LessonUnitStepActions()
            {
                Yls_CourseId = unit.CourseId,
                Yls_LessonId = unit.LessonId,
                Yls_UnitId = lessonUnit.Ycu_Id,
                Yls_Status = 1,
                Yls_UnitIndex = unit.Index,
                Yls_SubjectIds = GetLessonQuestionIdStr(unit.Steps),
                Steps = unit.Steps
            });
            return lessonUnit.Ycu_Id;
        }

        /// <summary>
        /// 修改单元讲义数据
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public void Update(DtoLessonUnit unit)
        {
            var lessonUnit = LessonServer.SelectUnit(unit.Id);
            var unitSteps = (Yw_LessonUnitStepActions)UnitStepFactory.Create(UnitServer.SelectSteps(unit.Id));
            lessonUnit.Ycu_Name = unit.Name;
            lessonUnit.Ycu_Screenshot = unit.Screenshot;
            lessonUnit.Ycu_Editor = unit.Editor;
            lessonUnit.Ycu_Index = unit.Index;
            if (unit.Steps != null)
            {
                unitSteps.Yls_UnitId = lessonUnit.Ycu_Id;
                unitSteps.Yls_UnitIndex = unit.Index;
                unitSteps.Steps = unit.Steps;
                unitSteps.Yls_Coins = unit.Coins;
                unitSteps.Yls_SubjectIds = GetLessonQuestionIdStr(unit.Steps);
            }
            LessonServer.UpdateUnits(lessonUnit);
            UnitServer.UpdateSteps(unitSteps);
        }
        /// <summary>
        /// 添加radis中存储的单元对象
        /// </summary>
        /// <param name="unit"></param>
        public void SaveUnitToRadis(DtoLessonUnit unit)
        {
            var unitSteps = new Yw_LessonUnitStepActions();
            unitSteps.Yls_UnitId = unit.Id;
            unitSteps.Yls_UnitIndex = unit.Index;
            unitSteps.Steps = unit.Steps;
            unitSteps.Yls_Coins = unit.Coins;
            unitSteps.Yls_SubjectIds = GetLessonQuestionIdStr(unit.Steps);
            unitSteps.ResetJsonValue();
            Yw_LessonUnitStep u = unitSteps;
            CacheHelper.SetCache(u, CacheKeyEnum.UnitStepEdit_Cache, DateTime.Now, unit.Id);
        }
        /// <summary>
        /// 读取radis中存储的单元对象
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public DtoLessonUnit GetUnitFromRadis(int unitid)
        {
            DtoLessonUnit unit = new DtoLessonUnit();
            var data =  CacheHelper.GetCache<Yw_LessonUnitStep>(CacheKeyEnum.UnitStepEdit_Cache, unitid);
            var unitSteps = (Yw_LessonUnitStepActions)UnitStepFactory.Create(data);
            unit.Id = unitSteps.Yls_UnitId;
            unit.Index = unitSteps.Yls_UnitIndex;
            unit.Coins = unitSteps.Yls_Coins;
            unit.Steps = unitSteps.Steps;
            unit.Status = unitSteps.Yls_Status;
            return unit;
        }
        /// <summary>
        /// 删除单元讲义数据
        /// </summary>
        /// <param name="unitId"></param>
        public void Delete(int unitId)
        {
            LessonServer.DeleteUnits(unitId);
        }

        /// <summary>
        /// 移动单元(讲义)位置
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="toIndex"></param>
        public void MoveUnit(int unitid, int toIndex)
        {
            LessonServer.MoveUnits(unitid, toIndex);
        }

        /// <summary>
        /// 根据单元(讲义)id查询单元(讲义)信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DtoLessonUnit SelectUnit(int unitId, int processId)
        {
            DtoLessonUnit unit = new DtoLessonUnit();
            var unitInfo = LessonServer.SelectUnit(unitId);
            var unitSteps = (Yw_LessonUnitStepActions)UnitStepFactory.Create(UnitServer.SelectSteps(unitId));
            if (unitInfo != null)
            {
                unit.Id = unitInfo.Ycu_Id;
                unit.CourseId = unitInfo.Ycu_CourseId;
                unit.LessonId = unitInfo.Ycu_LessonId;
                unit.Index = unitInfo.Ycu_Index;
                unit.Name = unitInfo.Ycu_Name;
                unit.Screenshot = unitInfo.Ycu_Screenshot;
                unit.Status = unitInfo.Ycu_Status;
                unit.Creator = unitInfo.Ycu_Creator;
                unit.CreateTime = unitInfo.Ycu_CreateTime;
                unit.Editor = unitInfo.Ycu_Editor;
                unit.UpdateTime = unitInfo.Ycu_UpdateTime;
                if (unitSteps != null)
                {
                    unit.Steps = unitSteps.Steps;
                }
                if (processId != 0)//如果有审批id
                {
                    var approve = ApproveServer.SelectByUnitIdAndProcessID(unitId, processId);//获取最后一个审批意见
                    if (approve != null)//如果审批意见不为空,添加审批内容
                    {
                        unit.Approve = approve.Yup_Remark;
                        unit.ApproveStatus = approve.Yup_Status;
                    }
                }
            }
            return unit;
        }

        /// <summary>
        /// 根据学生id课程id及课时id,分页查询课时单元信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoLessonUnit> SelectUnitByNext(DtoLessonUnitSearch search)
        {
            StudentStudyBll bll = new StudentStudyBll();
            //如果学生有这个课程进度
            if (bll.GetProgressByStudentCourse(search.StudentID, search.CourseID) != null)
            {
                //获取课时单元信息数据并转换成动作对象
                var list = UnitServer.SelectUnitsByNext(search).Select(u => (Yw_LessonUnitStepActions)UnitStepFactory.Create(u)).ToList();
                //将数据转换成为dto模型
                return list.Select(u => new DtoLessonUnit()
                {
                    CourseId = u.Yls_CourseId,
                    LessonId = u.Yls_LessonId,
                    Id = u.Yls_UnitId,
                    Index = u.Yls_UnitIndex,
                    Coins = u.Yls_Coins,
                    Steps = u.Steps,
                    Status = u.Yls_Status,
                    CreateTime = u.Yls_CreateTime,
                    UpdateTime = u.Yls_UpdateTime
                }).ToList();
            }
            return new List<DtoLessonUnit>();
        }

        /// <summary>
        /// 根据课时id获取单元(讲义)信息
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public List<DtoLessonUnit> SelectUnitsByLesson(int lessonid, int processid)
        {
            var unitInfo = LessonServer.SelectUnits(lessonid);//查询课时的所有单元
            var approve = ApproveServer.SelectByProcessID(processid);//查询课时最后一次审批数据
            return unitInfo.Select(unit =>
            {
                var a = approve.Where(s => s.Yup_UnitId == unit.Ycu_Id).FirstOrDefault();//获取审批数据
                return new DtoLessonUnit()
                {
                    Id = unit.Ycu_Id,
                    CourseId = unit.Ycu_CourseId,
                    LessonId = unit.Ycu_LessonId,
                    Index = unit.Ycu_Index,
                    Name = unit.Ycu_Name,
                    Screenshot = unit.Ycu_Screenshot,
                    Status = unit.Ycu_Status,
                    Creator = unit.Ycu_Creator,
                    ApproveStatus = a == null ? 0 : a.Yup_Status
                };
            }).ToList();
        }
        /// <summary>
        /// 设置课程审批信息
        /// </summary>
        /// <param name="unitProcess"></param>
        /// <returns></returns>
        public int SetUnitApprove(Yw_CourseLessonUnitProcess unitProcess)
        {
            var up = ApproveServer.SelectByUnitIdAndProcessID(unitProcess.Yup_UnitId, unitProcess.Yup_LessonProcessId);
            if (up != null)
            {
                up.Yup_Status = unitProcess.Yup_Status;
                up.Yup_Remark = unitProcess.Yup_Remark;
                up.Yup_Operator = unitProcess.Yup_Operator;
                ApproveServer.Update(up);
            }
            else
            {
                ApproveServer.Insert(unitProcess);
            }
            return 1;
        }

        /// <summary>
        /// 返回课时中所有的题目
        /// </summary>
        /// <param name="lessonid">课时id</param>
        /// <returns>返回课时中的所有题目数据</returns>
        public List<int> SelectLessonQuestions(int lessonid)
        {
            return UnitServer.SelectLessonQuestions(lessonid);
        }

        /// <summary>
        /// 获取课时的问题id字符串
        /// </summary>
        /// <param name="steps">课时的步骤数据</param>
        /// <returns></returns>
        private string GetLessonQuestionIdStr(List<Step> steps)
        {
            List<string> questionIds = new List<string>();
            if (steps != null)
            {
                steps.ForEach(s =>
                {
                    s.Actions.ForEach(a =>
                    {
                        if (a is IQuestion)
                        {
                            questionIds.Add(((IQuestion)a).QuestionId);
                        }
                    });
                });
                return string.Join(",", questionIds.Distinct().ToArray());
            }
            else
            {
                return "";
            }
        }
    }
}
