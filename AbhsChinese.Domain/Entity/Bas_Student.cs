using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_Student")]
    public partial class Bas_Student : EntityBase
    {
        [Key]
        public int Bst_Id { get; set; }
        private string bst_No;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_No
        {
            get
            {
                return bst_No;
            }
            set
            {
                AuditCheck("Bst_No", value);
                bst_No = value;
            }
        }

        private string bst_Name;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Name
        {
            get
            {
                return bst_Name;
            }
            set
            {
                AuditCheck("Bst_Name", value);
                bst_Name = value;
            }
        }

        private string bst_NickName;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_NickName
        {
            get
            {
                return bst_NickName;
            }
            set
            {
                AuditCheck("Bst_NickName", value);
                bst_NickName = value;
            }
        }

        private string bst_Avatar;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Avatar
        {
            get
            {
                return bst_Avatar;
            }
            set
            {
                AuditCheck("Bst_Avatar", value);
                bst_Avatar = value;
            }
        }

        private int bst_Sex;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Sex
        {
            get
            {
                return bst_Sex;
            }
            set
            {
                AuditCheck("Bst_Sex", value);
                bst_Sex = value;
            }
        }

        private DateTime bst_Birthday;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_Birthday
        {
            get
            {
                return bst_Birthday;
            }
            set
            {
                AuditCheck("Bst_Birthday", value);
                bst_Birthday = value;
            }
        }

        private int bst_Grade;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Grade
        {
            get
            {
                return bst_Grade;
            }
            set
            {
                AuditCheck("Bst_Grade", value);
                bst_Grade = value;
            }
        }

        private string bst_Phone;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Phone
        {
            get
            {
                return bst_Phone;
            }
            set
            {
                AuditCheck("Bst_Phone", value);
                bst_Phone = value;
            }
        }

        private int bst_SchoolId;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_SchoolId
        {
            get
            {
                return bst_SchoolId;
            }
            set
            {
                AuditCheck("Bst_SchoolId", value);
                bst_SchoolId = value;
            }
        }

        private string bst_StudySchool;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_StudySchool
        {
            get
            {
                return bst_StudySchool;
            }
            set
            {
                AuditCheck("Bst_StudySchool", value);
                bst_StudySchool = value;
            }
        }

        private int bst_Province;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Province
        {
            get
            {
                return bst_Province;
            }
            set
            {
                AuditCheck("Bst_Province", value);
                bst_Province = value;
            }
        }

        private int bst_City;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_City
        {
            get
            {
                return bst_City;
            }
            set
            {
                AuditCheck("Bst_City", value);
                bst_City = value;
            }
        }

        private int bst_County;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_County
        {
            get
            {
                return bst_County;
            }
            set
            {
                AuditCheck("Bst_County", value);
                bst_County = value;
            }
        }

        private string bst_Address;
        /// <summary>
        /// 
        /// </summary>
        public string Bst_Address
        {
            get
            {
                return bst_Address;
            }
            set
            {
                AuditCheck("Bst_Address", value);
                bst_Address = value;
            }
        }

        private DateTime bst_RegTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_RegTime
        {
            get
            {
                return bst_RegTime;
            }
            set
            {
                AuditCheck("Bst_RegTime", value);
                bst_RegTime = value;
            }
        }

        private int bst_RegSource;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_RegSource
        {
            get
            {
                return bst_RegSource;
            }
            set
            {
                AuditCheck("Bst_RegSource", value);
                bst_RegSource = value;
            }
        }

        private int bst_RegOperator;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_RegOperator
        {
            get
            {
                return bst_RegOperator;
            }
            set
            {
                AuditCheck("Bst_RegOperator", value);
                bst_RegOperator = value;
            }
        }

        private int bst_Status;
        /// <summary>
        /// 
        /// </summary>
        public int Bst_Status
        {
            get
            {
                return bst_Status;
            }
            set
            {
                AuditCheck("Bst_Status", value);
                bst_Status = value;
            }
        }

        private DateTime bst_UpdateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_UpdateTime
        {
            get
            {
                return bst_UpdateTime;
            }
            set
            {
                AuditCheck("Bst_UpdateTime", value);
                bst_UpdateTime = value;
            }
        }
    }
}

