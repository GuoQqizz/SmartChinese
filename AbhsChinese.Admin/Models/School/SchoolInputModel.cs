using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.School
{
    public class SchoolInputModel
    {


        public int Bsl_Id { get; set; }

        public string Bsl_SchoolName
        {
            get; set;
        }


        public int Bsl_Level
        {
            get; set;
        }


        public int Bsl_SchoolMasterId
        {
            get; set;
        }


        public string Bsl_MasterName
        {
            get; set;
        }


        public string Bsl_MasterPhone
        {
            get; set;
        }


        public int Bsl_Province
        {
            get; set;
        }

        public int Bsl_City
        {
            get; set;
        }

        public int Bsl_County
        {
            get; set;
        }


        public string Bsl_Address
        {
            get; set;
        }

        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime Bsl_ContractDate
        {
            get; set;
        }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime Bsl_ExpiredDate
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Bsl_Status
        {
            get; set;
        }


        public bool Bsl_IsValid
        {
            get; set;
        }
        public string IsValidStr
        {
            get; set;
        }

        public string Bsl_Remark
        {
            get; set;
        }





        public string LoginPhone { get; set; }

        public string LoginPwd { get; set; }

        public int Sex { get; set; }




        public Bas_School ToSchoolDbModel(Bas_School model)
        {
            //Bas_School model
            //Bas_School model = new Bas_School();

            //model.Bsl_Address = this.Bsl_Address;
            //model.Bsl_City = this.Bsl_City;
            //model.Bsl_ContractDate = this.Bsl_ContractDate;
            //model.Bsl_County = this.Bsl_County;
            //model.Bsl_Creator = this.Bsl_Creator;
            //model.Bsl_Editor = this.Bsl_Editor;
            //model.Bsl_ExpiredDate = this.Bsl_ExpiredDate;
            //model.Bsl_Id = this.Bsl_Id;
            //model.Bsl_IsValid = this.Bsl_IsValid;
            //model.Bsl_Level = this.Bsl_Level;
            //model.Bsl_MasterName = this.Bsl_MasterName;
            //model.Bsl_MasterPhone = this.Bsl_MasterPhone;
            //model.Bsl_Province = this.Bsl_Province;
            //model.Bsl_Remark = this.Bsl_Remark;
            //model.Bsl_SchoolMasterId = this.Bsl_SchoolMasterId;
            //model.Bsl_SchoolName = this.Bsl_SchoolName;
            //model.Bsl_Status = this.Bsl_Status;


            model = this.ConvertTo<Bas_School>(model);
            model.Bsl_IsValid = this.IsValidStr == "on";
            return model;
        }

        public Yw_SchoolTeacher ToSchoolTeacherDbModel()
        {
            Yw_SchoolTeacher model = new Yw_SchoolTeacher();
            if (model == null)
            {
                model = new Yw_SchoolTeacher();
            }
            model.Yoh_Id = this.Bsl_SchoolMasterId;
            model.Yoh_Name = this.Bsl_MasterName;
            model.Yoh_Phone = this.LoginPhone;
            model.Yoh_Password = this.LoginPwd;
            model.Yoh_IsSchoolMaster = true;
            model.Yoh_Sex = this.Sex;
            model.Yoh_Status = (int)StatusEnum.有效;

            return model;
        }

        public SchoolInputModel()
        {
            this.Sex = (int)SexEnum.男;
        }

        public SchoolInputModel FromDbModel(Bas_School school, Yw_SchoolTeacher schoolTeacher)
        {
            SchoolInputModel res = new SchoolInputModel();
            if (school != null)
            {
                res = school.ConvertTo<SchoolInputModel>();
                res.IsValidStr = school.Bsl_IsValid ? "on" : "";
                //this.Bsl_Address = school.Bsl_Address;
                //this.Bsl_City = school.Bsl_City;
                //this.Bsl_ContractDate = school.Bsl_ContractDate;
                //this.Bsl_County = school.Bsl_County;
                //this.Bsl_Creator = school.Bsl_Creator;
                //this.Bsl_Editor = school.Bsl_Editor;
                //this.Bsl_ExpiredDate = school.Bsl_ExpiredDate;
                //this.Bsl_Id = school.Bsl_Id;
                //this.Bsl_IsValid = school.Bsl_IsValid;
                //this.Bsl_Level = school.Bsl_Level;
                //this.Bsl_MasterName = school.Bsl_MasterName;
                //this.Bsl_MasterPhone = school.Bsl_MasterPhone;
                //this.Bsl_Province = school.Bsl_Province;
                //this.Bsl_Remark = school.Bsl_Remark;
                //this.Bsl_SchoolMasterId = school.Bsl_SchoolMasterId;
                //this.Bsl_SchoolName = school.Bsl_SchoolName;
                //this.Bsl_Status = school.Bsl_Status;
            }
            if (schoolTeacher != null)
            {
                res.LoginPhone = schoolTeacher.Yoh_Phone;
            }
            return res;
        }
    }

}