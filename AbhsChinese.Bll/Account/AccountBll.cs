using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.Account
{
    public class AccountBll
    {

        #region bll
        private StudentBll studentBll;
        public StudentBll StudentBll
        {
            get
            {
                if (studentBll == null)
                {
                    studentBll = new StudentBll();
                }
                return studentBll;
            }
        }

        private EmployeeBll employeeBll;
        public EmployeeBll EmployeeBll
        {
            get
            {
                if (employeeBll == null)
                {
                    employeeBll = new EmployeeBll();
                }
                return employeeBll;
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


        public bool CheckPhone(string phone, AccountVerifyTypeEnum type)
        {
            bool result = false;
            switch (type)
            {
                case AccountVerifyTypeEnum.学生未注册:
                    result = StudentBll.CheckUniqueAccount(phone) == 0;
                    break;
                case AccountVerifyTypeEnum.学生已注册:
                    result = StudentBll.CheckUniqueAccount(phone) > 0;
                    break;
                case AccountVerifyTypeEnum.校区教师未注册:
                    result = SchoolTeacherBll.GetSchoolTeacherCountByPhone(phone) == 0;
                    break;
                case AccountVerifyTypeEnum.校区教师已注册:
                case AccountVerifyTypeEnum.校区找回密码:
                    result = SchoolTeacherBll.GetSchoolTeacherCountByPhone(phone) > 0;
                    break;
                case AccountVerifyTypeEnum.教研教师未注册:
                    result = EmployeeBll.CheckUniqueAccount(phone) == 0;
                    break;
                case AccountVerifyTypeEnum.教研教师已注册:
                    result = EmployeeBll.CheckUniqueAccount(phone) > 0;
                    break;
                default:
                    result = true;
                    break;
            }
            return result;
        }
    }
}
