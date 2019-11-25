using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.School
{
	[Table("Yw_SchoolTeacher")]
    public partial class Yw_SchoolTeacher:EntityBase
    {
        [Key]
        public int Yoh_Id { get; set; }
		private int yoh_SchoolId;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_SchoolId 
		{ 
			get
			{
				return yoh_SchoolId;
			} 
			set 
			{
				AuditCheck("Yoh_SchoolId", value);
				yoh_SchoolId=value; 
			}
		}
      
		private string yoh_Name;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Name 
		{ 
			get
			{
				return yoh_Name;
			} 
			set 
			{
				AuditCheck("Yoh_Name", value);
				yoh_Name=value; 
			}
		}
      
		private string yoh_Phone;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Phone 
		{ 
			get
			{
				return yoh_Phone;
			} 
			set 
			{
				AuditCheck("Yoh_Phone", value);
				yoh_Phone=value; 
			}
		}
      
		private string yoh_Password;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Password 
		{ 
			get
			{
				return yoh_Password;
			} 
			set 
			{
				AuditCheck("Yoh_Password", value);
				yoh_Password=value; 
			}
		}
      
		private int yoh_Sex;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_Sex 
		{ 
			get
			{
				return yoh_Sex;
			} 
			set 
			{
				AuditCheck("Yoh_Sex", value);
				yoh_Sex=value; 
			}
		}
      
		private string yoh_Avatar;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Avatar 
		{ 
			get
			{
				return yoh_Avatar;
			} 
			set 
			{
				AuditCheck("Yoh_Avatar", value);
				yoh_Avatar=value; 
			}
		}
      
		private string yoh_Email;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Email 
		{ 
			get
			{
				return yoh_Email;
			} 
			set 
			{
				AuditCheck("Yoh_Email", value);
				yoh_Email=value; 
			}
		}
      
		private int yoh_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_Status 
		{ 
			get
			{
				return yoh_Status;
			} 
			set 
			{
				AuditCheck("Yoh_Status", value);
				yoh_Status=value; 
			}
		}
      
		private int yoh_Grade;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_Grade 
		{ 
			get
			{
				return yoh_Grade;
			} 
			set 
			{
				AuditCheck("Yoh_Grade", value);
				yoh_Grade=value; 
			}
		}
      
		private string yoh_Remark;
		/// <summary>
        /// 
        /// </summary>
        public string Yoh_Remark 
		{ 
			get
			{
				return yoh_Remark;
			} 
			set 
			{
				AuditCheck("Yoh_Remark", value);
				yoh_Remark=value; 
			}
		}
      
		private bool yoh_IsSchoolMaster;
		/// <summary>
        /// 
        /// </summary>
        public bool Yoh_IsSchoolMaster 
		{ 
			get
			{
				return yoh_IsSchoolMaster;
			} 
			set 
			{
				AuditCheck("Yoh_IsSchoolMaster", value);
				yoh_IsSchoolMaster=value; 
			}
		}
      
		private DateTime yoh_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yoh_CreateTime 
		{ 
			get
			{
				return yoh_CreateTime;
			} 
			set 
			{
				AuditCheck("Yoh_CreateTime", value);
				yoh_CreateTime=value; 
			}
		}
      
		private int yoh_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_Creator 
		{ 
			get
			{
				return yoh_Creator;
			} 
			set 
			{
				AuditCheck("Yoh_Creator", value);
				yoh_Creator=value; 
			}
		}
      
		private DateTime yoh_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yoh_UpdateTime 
		{ 
			get
			{
				return yoh_UpdateTime;
			} 
			set 
			{
				AuditCheck("Yoh_UpdateTime", value);
				yoh_UpdateTime=value; 
			}
		}
      
		private int yoh_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Yoh_Editor 
		{ 
			get
			{
				return yoh_Editor;
			} 
			set 
			{
				AuditCheck("Yoh_Editor", value);
				yoh_Editor=value; 
			}
		}
      
    }

}

