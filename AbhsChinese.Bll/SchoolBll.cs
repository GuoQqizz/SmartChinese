using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Repository.Repository.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class SchoolBll : BllBase
    {
        public SchoolBll() : base()
        {
        }
        #region bll
        private StudentStudyBll studentStudyBll;
        public StudentStudyBll StudentStudyBll
        {
            get
            {
                if (studentStudyBll == null)
                {
                    studentStudyBll = new StudentStudyBll();
                }
                return studentStudyBll;
            }
        }

        private SchoolTeacherBll schoolTeacherBll;
        public SchoolTeacherBll SchoolTeacherBll
        {
            get
            {
                if (schoolTeacherBll == null)
                {
                    schoolTeacherBll = new SchoolTeacherBll();
                }
                return schoolTeacherBll;
            }
        }
        #endregion
        #region repository

        private ISchoolLevelRepository schoolLevelRepository;
        public ISchoolLevelRepository SchoolLevelRepository
        {
            get
            {
                if (schoolLevelRepository == null)
                {
                    schoolLevelRepository = new SchoolLevelRepository();
                }
                return schoolLevelRepository;
            }
        }

        private ISchoolRepository schoolRepository;
        public ISchoolRepository SchoolRepository
        {
            get
            {
                if (schoolRepository == null)
                {
                    schoolRepository = new SchoolRepository();
                }
                return schoolRepository;
            }
        }




        #endregion

        #region select
        /// <summary>
        /// 学校等级列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<Bas_SchoolLevel> GetSchoolLevelList(PagingObject page = null)
        {
            if (page == null)
            {
                page = new PagingObject(1, 100);
            }
            return SchoolLevelRepository.GetList(page);
        }
        public Bas_SchoolLevel GetSchoolLevel(int schoolLevelId)
        {
            return SchoolLevelRepository.Get(schoolLevelId);
        }
        /// <summary>
        /// 学校列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoSchool> GetSchoolList(DtoSchoolSearch search)
        {
            return SchoolRepository.GetSchoolList(search);
        }
        /// <summary>
        /// 学校实体
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public Bas_School GetSchool(int schoolId)
        {
            return SchoolRepository.Get(schoolId);
        }
        public DtoSchool GetSchoolDto(int schoolId)
        {
            DtoSchoolSearch search = new DtoSchoolSearch();
            search.SchoolId = schoolId;
            return SchoolRepository.GetSchoolList(search).FirstOrDefault();
        }

        /// <summary>
        /// 获取学生绑定的校区
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Bas_School GetSchoolByStudent(int studentId)
        {
            return SchoolRepository.GetSchoolByStudent(studentId);
        }

        public List<DtoKeyValue<int, string>> GetSchoolByIdOrName(PagingObject paging, string idOrName)
        {
            var list = SchoolRepository.GetSchoolByIdOrName(paging, idOrName);
            return list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Bsl_Id, value = s.Bsl_SchoolName }; }).ToList();
        }
        #endregion

        #region insert
        /// <summary>
        /// 插入学校等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertSchoolLevel(Bas_SchoolLevel model)
        {

            return SchoolLevelRepository.Insert(model);
        }
        #endregion

        #region update


        #endregion

        #region save
        /// <summary>
        /// 保存学校
        /// </summary>
        /// <param name="school"></param>
        /// <param name="schoolTeacher"></param>
        /// <returns></returns>
        public bool SaveSchool(Bas_School school, Yw_SchoolTeacher schoolTeacher)
        {
            bool result = false;
            #region 参数逻辑校验
            bool checkParam = true;
            if (school.Bsl_Id == 0 && schoolTeacher.Yoh_Id != 0)//学校不存在 校长已经存在
            {
                checkParam = false;
            }
            if (school.Bsl_Id != 0 && schoolTeacher.Yoh_Id == 0)//学校存在 校长不存在
            {
                checkParam = false;
            }
            int count = SchoolTeacherBll.GetSchoolTeacherCountByPhone(schoolTeacher.Yoh_Phone);
            if (schoolTeacher.Yoh_Id == 0 && count > 0)//校长不存在，登录手机已经存在
            {
                checkParam = false;
            }
            if (!checkParam)
            {
                //throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
                return false;
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (school.Bsl_Id == 0)
                    {
                        int schoolId = SchoolRepository.Insert(school);
                        int schoolTeacherId = 0;
                        if (schoolId > 0)
                        {
                            schoolTeacher.Yoh_SchoolId = schoolId;
                            schoolTeacherId = SchoolTeacherBll.InsertEntity(schoolTeacher);
                            if (schoolTeacherId > 0)
                            {
                                result = SchoolRepository.UpdateMasterId(schoolId, schoolTeacherId);
                            }
                        }
                    }
                    else
                    {
                        #region teacher
                        bool updateTeacher = true;
                        bool updateSchool = true;
                        var oldTeacher = SchoolTeacherBll.GetEntity(schoolTeacher.Yoh_Id);
                        int schoolTeacherId = schoolTeacher.Yoh_Id;
                        #region 更新校长
                        if (oldTeacher.Yoh_Phone != schoolTeacher.Yoh_Phone)
                        {
                            if (SchoolTeacherBll.GetSchoolTeacherCountByPhone(schoolTeacher.Yoh_Phone) == 0 && schoolTeacher.Yoh_Password.HasValue())
                            {
                                SchoolTeacherBll.UpdateStatus(schoolTeacher.Yoh_Id, (int)StatusEnum.删除, school.Bsl_Editor);
                                schoolTeacher.Yoh_Id = 0;
                                schoolTeacher.Yoh_SchoolId = school.Bsl_Id;
                                schoolTeacher.Yoh_CreateTime = DateTime.Now;
                                schoolTeacher.Yoh_UpdateTime = DateTime.Now;
                                schoolTeacherId = SchoolTeacherBll.InsertEntity(schoolTeacher);
                                updateTeacher = schoolTeacherId > 0;

                            }
                            else
                            {
                                updateTeacher = false;
                            }
                        }
                        #endregion
                        #region 更新密码
                        else
                        {
                            if (schoolTeacher.Yoh_Password.HasValue())
                            {
                                updateTeacher = SchoolTeacherBll.UpdatePwd(schoolTeacher.Yoh_Id, schoolTeacher.Yoh_Password, school.Bsl_Editor);
                            }
                        }
                        #endregion
                        #endregion
                        #region 更新学校
                        school.Bsl_SchoolMasterId = schoolTeacherId;
                        updateSchool = SchoolRepository.Update(school);
                        #endregion
                        result = updateTeacher && updateSchool;

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
                    throw ex;
                }
            }
            return result;
        }

        public bool SaveSchoolLevel(Bas_SchoolLevel model)
        {
            bool result = false;
            if (model.Bhl_Id > 0)
            {
                result = SchoolLevelRepository.Update(model);
            }
            else
            {
                result = SchoolLevelRepository.Insert(model) > 0;
            }
            return result;
        }
        #endregion
    }
}
