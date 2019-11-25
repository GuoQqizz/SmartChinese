using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StudentInfoViewModel
    {
        public int Bst_Id { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string Bst_No { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Bst_Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Bst_NickName { get; set; }

        /// <summary>
        /// 称号
        /// </summary>
        public string Salutation => SalutationEnum.书童.ToString();

        /// <summary>
        /// 头像
        /// </summary>
        public string Bst_Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Bst_Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Bst_Birthday { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public int Bst_Grade { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Bst_Phone { get; set; }

        /// <summary>
        /// 校区
        /// </summary>
        public int Bst_SchoolId { get; set; }
        
        /// <summary>
        /// 在读学校
        /// </summary>
        public string Bst_StudySchool { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public int Bst_Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public int Bst_City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public int Bst_County { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Bst_Address { get; set; }

        public string SchoolName { get; set; }

        public string Avatar => string.IsNullOrEmpty(Bst_Avatar)?"":Bst_Avatar.ToOssPath();
        public string Sex => CustomEnumHelper.Parse(typeof(SexEnum), Bst_Sex);
        public string Birthday => Bst_Birthday.ToString("yyyy年MM月dd日");
        public string BirthdayEdit => Bst_Birthday.CompareTo(Convert.ToDateTime("1900-01-01")) == 0 ? DateTime.Now.ToString("yyyy-MM-dd") : Bst_Birthday.ToString("yyyy-MM-dd");
        public string Grade => CustomEnumHelper.Parse(typeof(GradeEnum), Bst_Grade);

        public int ApplyStatus { get; set; }

        public string ApplySchoolName { get; set; }
    }
}