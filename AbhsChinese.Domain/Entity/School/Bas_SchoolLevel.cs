using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.School
{           
	[Table("Bas_SchoolLevel")]
    public partial class Bas_SchoolLevel:EntityBase
    {
        [Key]
        public int Bhl_Id { get; set; }
		private string bhl_Name;
		/// <summary>
        /// 
        /// </summary>
        public string Bhl_Name 
		{ 
			get
			{
				return bhl_Name;
			} 
			set 
			{
				AuditCheck("Bhl_Name", value);
				bhl_Name=value; 
			}
		}
      
		private int bhl_DividePercent;
		/// <summary>
        /// 
        /// </summary>
        public int Bhl_DividePercent 
		{ 
			get
			{
				return bhl_DividePercent;
			} 
			set 
			{
				AuditCheck("Bhl_DividePercent", value);
				bhl_DividePercent=value; 
			}
		}
      
		private string bhl_Remark;
		/// <summary>
        /// 
        /// </summary>
        public string Bhl_Remark 
		{ 
			get
			{
				return bhl_Remark;
			} 
			set 
			{
				AuditCheck("Bhl_Remark", value);
				bhl_Remark=value; 
			}
		}
      
		private int bhl_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Bhl_Status 
		{ 
			get
			{
				return bhl_Status;
			} 
			set 
			{
				AuditCheck("Bhl_Status", value);
				bhl_Status=value; 
			}
		}
      
		private DateTime bhl_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bhl_CreateTime 
		{ 
			get
			{
				return bhl_CreateTime;
			} 
			set 
			{
				AuditCheck("Bhl_CreateTime", value);
				bhl_CreateTime=value; 
			}
		}
      
		private int bhl_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Bhl_Creator 
		{ 
			get
			{
				return bhl_Creator;
			} 
			set 
			{
				AuditCheck("Bhl_Creator", value);
				bhl_Creator=value; 
			}
		}
      
		private DateTime bhl_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bhl_UpdateTime 
		{ 
			get
			{
				return bhl_UpdateTime;
			} 
			set 
			{
				AuditCheck("Bhl_UpdateTime", value);
				bhl_UpdateTime=value; 
			}
		}
      
		private int bhl_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Bhl_Editor 
		{ 
			get
			{
				return bhl_Editor;
			} 
			set 
			{
				AuditCheck("Bhl_Editor", value);
				bhl_Editor=value; 
			}
		}
      
    }

}

