using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models
{
    public class SchoolTeacherInputModel
    {
        public int Yoh_Id { get; set; }

        public int Yoh_SchoolId { get; set; }
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
        public string Yoh_Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Yoh_Sex
        {
            get; set;
        }

        /// <summary>
        /// 头像
        /// </summary>
        public string Yoh_Avatar
        {
            get; set;
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

        /// <summary>
        /// 
        /// </summary>
        public int Yoh_Grade { get; set; }
        public List<int> Grades { get; set; }

        public string Yoh_Remark { get; set; }

        public bool Yoh_IsSchoolMaster { get; set; }


        //public string SessionId { get; set; }
        public string VerificationCode { get; set; }

        public SchoolTeacherInputModel()
        {
            this.Yoh_IsSchoolMaster = false;
            this.Grades = new List<int>();
        }

        public SchoolTeacherInputModel FromDbModel(Yw_SchoolTeacher dbModel)
        {
            SchoolTeacherInputModel model = dbModel.ConvertTo<SchoolTeacherInputModel>();
            if (model == null)
            {
                model = new SchoolTeacherInputModel();
            }
            model.Yoh_Password = "";
            return model;
        }

        public Yw_SchoolTeacher ToDbModel(Yw_SchoolTeacher dbModel)
        {
            this.Yoh_Grade = this.Grades.Sum();
            return this.ConvertTo<Yw_SchoolTeacher>(dbModel);
        }
    }
}