using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoSchoolTeacher
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
        [ScriptIgnore]
        [JsonIgnore]
        public string Yoh_Password
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
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
            get;set;
        }

        public string Yoh_AvatarShow => Yoh_Avatar.ToOssPath();
        /// <summary>
        /// 
        /// </summary>
        public string Yoh_Email
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Yoh_Status
        {
            get; set;
        }
        public string StatusStr => CustomEnumHelper.Parse(typeof(StatusEnum), Yoh_Status);
        /// <summary>
        /// 
        /// </summary>
        public int Yoh_Grade
        {
            get; set;
        }
        public string Grades => string.Join(",", CustomEnumHelper.ParseBinaryAnd(typeof(GradeEnum), Yoh_Grade).Values);


        public string Bsl_SchoolName { get; set; }

        public int Yoh_SchoolId { get; set; }
    }
}
