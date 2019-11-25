using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.Repository.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public class SchoolTeacherBll
    {
        #region repository
        private ISchoolTeacherRepository schoolTeacherRepository;
        public ISchoolTeacherRepository SchoolTeacherRepository
        {
            get
            {
                if (schoolTeacherRepository == null)
                {
                    schoolTeacherRepository = new SchoolTeacherRepository();
                }
                return schoolTeacherRepository;
            }
        }


        #endregion
        #region select

        /// <summary>
        /// 学校教师列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoSchoolTeacher> GetSchoolTeacherList(DtoSchoolTeacherSearch search)
        {
            return SchoolTeacherRepository.GetSchoolTeacherList(search);
        }
        /// <summary>
        /// 学校教师实体
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public Yw_SchoolTeacher GetSchoolTeacher(int teacherId)
        {
            return SchoolTeacherRepository.Get(teacherId);
        }
        public List<DtoKeyValue<int, string>> GetTeacherByGrade(int grade, int schoolId)
        {
            var list = SchoolTeacherRepository.GetTeacherByGrade(grade, schoolId);
            return list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Yoh_Id, value = s.Yoh_Name }; }).ToList();

        }
        /// <summary>
        /// 校验教师手机唯一性
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public int GetSchoolTeacherCountByPhone(string phone)
        {
            return SchoolTeacherRepository.GetCountByPhone(phone);
        }
        public DtoSchoolTeacher GetSchoolTeacherByPhone(string phone)
        {
            return SchoolTeacherRepository.GetSchoolTeacherByPhone(phone);
        }

        public Yw_SchoolTeacher GetEntity(int id)
        {
            return SchoolTeacherRepository.Get(id);
        }

        public DtoSchoolTeacher Login(string account, string pwd)
        {
            DtoSchoolTeacher result = null;
            var teacher = SchoolTeacherRepository.GetSchoolTeacherByPhone(account);
            if (teacher != null)
            {
                result = teacher.Yoh_Password == Encrypt.GetMD5Pwd(pwd) ? teacher : null;
            }
            return result;


        }
        #endregion
        #region insert
        public int InsertEntity(Yw_SchoolTeacher entity)
        {
            return SchoolTeacherRepository.Insert(entity);
        }
        #endregion

        #region update
        public bool UpdateStatus(int id, int toStatus, int edit)
        {
            return SchoolTeacherRepository.UpdateStatus(id, toStatus, edit);
        }

        public bool UpdatePwd(int id, string pwd, int edit = 0)
        {
            return SchoolTeacherRepository.UpdatePwd(id, pwd, edit);
        }
        #endregion

        #region save
        /// <summary>
        /// 保存学校教师
        /// </summary>
        /// <param name="newModel"></param>
        /// <returns></returns>
        public bool SaveSchoolTeacher(Yw_SchoolTeacher newModel)
        {
            if (newModel.Yoh_Id > 0)
            {
                return SchoolTeacherRepository.Update(newModel);
            }
            else
            {
                newModel.Yoh_CreateTime = DateTime.Now;
                newModel.Yoh_UpdateTime = DateTime.Now;

                return SchoolTeacherRepository.Insert(newModel) > 0;
            }
        }


        #endregion
    }
}
