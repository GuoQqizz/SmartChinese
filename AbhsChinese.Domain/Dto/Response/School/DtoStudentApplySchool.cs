using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoStudentApplySchool
    {
        /// <summary>
        /// 申请ID
        /// </summary>
        public int Yay_Id { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int Yay_StudentId
        {
            get; set;
        }

        /// <summary>
        /// 学号
        /// </summary>
        public string Bst_No { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        public int Yay_SchoolId
        {
            get; set;
        }
        [JsonIgnore]
        public string Bst_Phone { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Bst_PhoneShow { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Bst_NickName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Bst_Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Bst_Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SexStr => CustomEnumHelper.Parse(typeof(SexEnum), Bst_Sex);

        public int Bst_Grade { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public string GradeStr => CustomEnumHelper.Parse(typeof(GradeEnum), Bst_Grade);

        public DateTime Yay_ApplyTime { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyDate => Yay_ApplyTime.ToString("yyyy-MM-dd");
        /// <summary>
        /// 来源老师ID
        /// </summary>
        public int Yay_TeacherId
        {
            get; set;
        }
        /// <summary>
        /// 来源老师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 来源老师电话
        /// </summary>
        public string TeacherPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Yay_Status
        {
            get; set;
        }

        public string SchoolName { get; set; }
    }
}
