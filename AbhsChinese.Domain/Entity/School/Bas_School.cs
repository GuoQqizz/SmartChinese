using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.School
{
	[Table("Bas_School")]
    public partial class Bas_School:EntityBase
    {
        [Key]
        public int Bsl_Id { get; set; }
		private string bsl_SchoolName;
		/// <summary>
        /// 
        /// </summary>
        public string Bsl_SchoolName 
		{ 
			get
			{
				return bsl_SchoolName;
			} 
			set 
			{
				AuditCheck("Bsl_SchoolName", value);
				bsl_SchoolName=value; 
			}
		}
      
		private int bsl_Level;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_Level 
		{ 
			get
			{
				return bsl_Level;
			} 
			set 
			{
				AuditCheck("Bsl_Level", value);
				bsl_Level=value; 
			}
		}
      
		private int bsl_SchoolMasterId;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_SchoolMasterId 
		{ 
			get
			{
				return bsl_SchoolMasterId;
			} 
			set 
			{
				AuditCheck("Bsl_SchoolMasterId", value);
				bsl_SchoolMasterId=value; 
			}
		}
      
		private string bsl_MasterName;
		/// <summary>
        /// 
        /// </summary>
        public string Bsl_MasterName 
		{ 
			get
			{
				return bsl_MasterName;
			} 
			set 
			{
				AuditCheck("Bsl_MasterName", value);
				bsl_MasterName=value; 
			}
		}
      
		private string bsl_MasterPhone;
		/// <summary>
        /// 
        /// </summary>
        public string Bsl_MasterPhone 
		{ 
			get
			{
				return bsl_MasterPhone;
			} 
			set 
			{
				AuditCheck("Bsl_MasterPhone", value);
				bsl_MasterPhone=value; 
			}
		}
      
		private int bsl_Province;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_Province 
		{ 
			get
			{
				return bsl_Province;
			} 
			set 
			{
				AuditCheck("Bsl_Province", value);
				bsl_Province=value; 
			}
		}
      
		private int bsl_City;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_City 
		{ 
			get
			{
				return bsl_City;
			} 
			set 
			{
				AuditCheck("Bsl_City", value);
				bsl_City=value; 
			}
		}
      
		private int bsl_County;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_County 
		{ 
			get
			{
				return bsl_County;
			} 
			set 
			{
				AuditCheck("Bsl_County", value);
				bsl_County=value; 
			}
		}
      
		private string bsl_Address;
		/// <summary>
        /// 
        /// </summary>
        public string Bsl_Address 
		{ 
			get
			{
				return bsl_Address;
			} 
			set 
			{
				AuditCheck("Bsl_Address", value);
				bsl_Address=value; 
			}
		}
      
		private DateTime bsl_ContractDate;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bsl_ContractDate 
		{ 
			get
			{
				return bsl_ContractDate;
			} 
			set 
			{
				AuditCheck("Bsl_ContractDate", value);
				bsl_ContractDate=value; 
			}
		}
      
		private DateTime bsl_ExpiredDate;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bsl_ExpiredDate 
		{ 
			get
			{
				return bsl_ExpiredDate;
			} 
			set 
			{
				AuditCheck("Bsl_ExpiredDate", value);
				bsl_ExpiredDate=value; 
			}
		}
      
		private int bsl_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_Status 
		{ 
			get
			{
				return bsl_Status;
			} 
			set 
			{
				AuditCheck("Bsl_Status", value);
				bsl_Status=value; 
			}
		}
      
		private bool bsl_IsValid;
		/// <summary>
        /// 
        /// </summary>
        public bool Bsl_IsValid 
		{ 
			get
			{
				return bsl_IsValid;
			} 
			set 
			{
				AuditCheck("Bsl_IsValid", value);
				bsl_IsValid=value; 
			}
		}
      
		private string bsl_Remark;
		/// <summary>
        /// 
        /// </summary>
        public string Bsl_Remark 
		{ 
			get
			{
				return bsl_Remark;
			} 
			set 
			{
				AuditCheck("Bsl_Remark", value);
				bsl_Remark=value; 
			}
		}
      
		private DateTime bsl_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bsl_CreateTime 
		{ 
			get
			{
				return bsl_CreateTime;
			} 
			set 
			{
				AuditCheck("Bsl_CreateTime", value);
				bsl_CreateTime=value; 
			}
		}
      
		private int bsl_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_Creator 
		{ 
			get
			{
				return bsl_Creator;
			} 
			set 
			{
				AuditCheck("Bsl_Creator", value);
				bsl_Creator=value; 
			}
		}
      
		private DateTime bsl_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bsl_UpdateTime 
		{ 
			get
			{
				return bsl_UpdateTime;
			} 
			set 
			{
				AuditCheck("Bsl_UpdateTime", value);
				bsl_UpdateTime=value; 
			}
		}
      
		private int bsl_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Bsl_Editor 
		{ 
			get
			{
				return bsl_Editor;
			} 
			set 
			{
				AuditCheck("Bsl_Editor", value);
				bsl_Editor=value; 
			}
		}
      
    }

}

