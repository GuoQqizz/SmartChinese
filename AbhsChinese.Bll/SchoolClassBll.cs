using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.Repository.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class SchoolClassBll : BllBase
    {
        public SchoolClassBll() : base()
        {

        }

        #region repository
        private ISchoolClassRepository schoolClassRepository;
        public ISchoolClassRepository SchoolClassRepository
        {
            get
            {
                if (schoolClassRepository == null)
                {
                    schoolClassRepository = new SchoolClassRepository();
                }
                return schoolClassRepository;
            }
        }

        private ISchoolClassScheduleRepository schoolClassScheduleRepository;
        public ISchoolClassScheduleRepository SchoolClassScheduleRepository
        {
            get
            {
                if (schoolClassScheduleRepository == null)
                {
                    schoolClassScheduleRepository = new SchoolClassScheduleRepository();
                }
                return schoolClassScheduleRepository;
            }
        }
        #endregion

        #region select
        public Yw_SchoolClass GetSchoolClass(int classId)
        {
            return SchoolClassRepository.Get(classId);
        }

        /// <summary>
        /// 获取班级列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoSchoolClass> GetSchoolClassList(DtoSchoolClassSearch search)
        {
            var list = SchoolClassRepository.GetSchoolClassList(search);
            SetSchoolClassSchedule(list);
            return list;
        }
        private void SetSchoolClassSchedule(List<DtoSchoolClass> listClass)
        {
            var classIds = listClass.Select(s => s.Ycc_Id).ToList();
            var schedules = GetClassSchedule(classIds);
            if (listClass != null && listClass.Count > 0)
            {
                listClass.ForEach(c =>
                {
                    c.ClassSchedule = schedules.Where(s => s.Ywd_ClassId == c.Ycc_Id).Select(cs => cs.ConvertTo<DtoSchoolClassSchedule>()).ToList();
                });
            }
        }

        public List<Yw_SchoolClassSchedule> GetClassSchedule(List<int> classIds)
        {
            return SchoolClassScheduleRepository.GetByClassIds(classIds);
        }
        public List<Yw_SchoolClassSchedule> GetSchoolClassSchedule(int classId)
        {
            return SchoolClassScheduleRepository.GetByClassId(classId);
        }
        /// <summary>
        /// 根据课程id查班级
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<Yw_SchoolClass> GetSchoolClassByCourseId(int courseId, int schoolId)
        {
            return SchoolClassRepository.GetSchoolClassByCourseId(courseId, schoolId);
        }
        #endregion

        #region update

        public bool IncrementStudentCount(int classId, int count, int oper)
        {
            return SchoolClassRepository.IncrementStudentCount(classId, count, oper);
        }
        #endregion

        #region save
        private bool SaveSchoolClass(Yw_SchoolClass schoolClass)
        {
            bool result = false;
            if (schoolClass.Ycc_Id > 0)
            {
                //var old = SchoolClassRepository.Get(schoolClass.Ycc_Id);
                //if (old != null)
                //{
                //    schoolClass.Ycc_CreateTime = old.Ycc_CreateTime;
                //    schoolClass.Ycc_Creator = old.Ycc_Creator;
                //    result = SchoolClassRepository.Update(schoolClass);
                //}
                result = SchoolClassRepository.Update(schoolClass);
            }
            else
            {
                var id = SchoolClassRepository.Insert(schoolClass);
                schoolClass.Ycc_Id = id;
                result = id > 0;
            }
            return result;
        }
        public int InsertListSchoolClassSchedule(List<Yw_SchoolClassSchedule> listSchedule, bool deleteOld = true)
        {
            if (deleteOld)
            {
                var ids = listSchedule.Select(s => s.Ywd_ClassId).Distinct().ToList();
                SchoolClassScheduleRepository.DeleteByClassIds(ids);
            }
            var id = SchoolClassScheduleRepository.InsertList(listSchedule);
            return id;

        }
        public bool SaveSchoolClass(Yw_SchoolClass schoolClass, List<Yw_SchoolClassSchedule> listSchedule)
        {
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    bool insert = schoolClass.Ycc_Id == 0;
                    result = SaveSchoolClass(schoolClass);
                    if (result)
                    {
                        if (insert)
                        {
                            listSchedule.ForEach(s =>
                            {
                                s.Ywd_ClassId = schoolClass.Ycc_Id;
                            });
                        }
                        result = InsertListSchoolClassSchedule(listSchedule) > 0;
                    }
                    if (result)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        RollbackTran();
                    }
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw;
                }
            }
            return result;
        }
        #endregion
    }
}
