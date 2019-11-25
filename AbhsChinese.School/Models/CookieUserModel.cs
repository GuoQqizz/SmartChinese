using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models
{
    public class CookieUserModel
    {
        public CookieTeacher Teacher { get; set; }
        public CookieSchool School { get; set; }
    }



    public class CookieSchool
    {
        /// <summary>
        /// 学校id
        /// </summary>
        public int Bsl_Id { get; set; }
        /// <summary>
        /// 学校等级id
        /// </summary>
        public int Bsl_Level { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; }

        public decimal Bhl_DividePercent { get; set; }

        public string Bsl_SchoolName { get; set; }
    }
    public class CookieTeacher
    {
        public int Yoh_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Yoh_Name
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Yoh_Phone
        {
            get; set;
        }
        public int Yoh_SchoolId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Yoh_Grade
        {
            get; set;
        }
        public string Grades => string.Join(",", CustomEnumHelper.ParseBinaryAnd(typeof(GradeEnum), Yoh_Grade).Values);


        public string Bsl_SchoolName { get; set; }

        public int Yoh_Sex
        {
            get; set;
        }
        public string SexStr => CustomEnumHelper.Parse(typeof(SexEnum), Yoh_Sex);
        /// <summary>
        /// 头像
        /// </summary>
        public string Yoh_Avatar
        {
            get; set;
        }

        public string Yoh_AvatarShow => Yoh_Avatar.ToOssPath();
    }
}