using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoSchoolStudent 
    {

        public int Bst_Id { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string Bst_No
        {
            get; set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Bst_Name
        {
            get; set;
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Bst_NickName
        {
            get; set;
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string Bst_Avatar
        {
            get; set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public int Bst_Sex
        {
            get; set;
        }
        public string SexStr => CustomEnumHelper.Parse(typeof(SexEnum), Bst_Sex);
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Grade
        {
            get; set;
        }
        public string GradeStr => CustomEnumHelper.Parse(typeof(GradeEnum), Bst_Grade);
        public DateTime Bst_Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Phone
        {
            get; set;
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string StudentAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Bst_SchoolId
        {
            get; set;
        }
        /// <summary>
        /// 学生系统学校名称
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 学生线下学校
        /// </summary>
        public string Bst_StudySchool
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Address
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Status
        {
            get; set;
        }
        public string StatusStr => CustomEnumHelper.Parse(typeof(StudentStatusEnum), Bst_Status);
         
        /// <summary>
        /// 已购课程数
        /// </summary>
        public int Sst_OwnCourseCount { get; set; }
        
        public string AccountType => Sst_OwnCourseCount > 0 ? StudentAccountTypeEnum.客户.ToString() : StudentAccountTypeEnum.用户.ToString();
    }
}
