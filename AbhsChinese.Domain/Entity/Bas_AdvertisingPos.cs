using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
	[Table("Bas_AdvertisingPos")]
    public partial class Bas_AdvertisingPos:EntityBase
    {
        [Key]
        public int Bap_Id { get; set; }
		private string bap_Code;
		/// <summary>
        /// 
        /// </summary>
        public string Bap_Code 
		{ 
			get
			{
				return bap_Code;
			} 
			set 
			{
				AuditCheck("Bap_Code", value);
				bap_Code=value; 
			}
		}
      
		private int bap_Type;
        /// <summary>
        /// 推荐类型 目前只有一个课程1
        /// <see cref="AbhsChinese.Domain.Enum.AdvertisingTypeEnum"/>
        /// </summary>
        public int Bap_Type 
		{ 
			get
			{
				return bap_Type;
			} 
			set 
			{
				AuditCheck("Bap_Type", value);
				bap_Type=value; 
			}
		}
      
		private int bap_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Bap_Status 
		{ 
			get
			{
				return bap_Status;
			} 
			set 
			{
				AuditCheck("Bap_Status", value);
				bap_Status=value; 
			}
		}
      
		private DateTime bap_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bap_CreateTime 
		{ 
			get
			{
				return bap_CreateTime;
			} 
			set 
			{
				AuditCheck("Bap_CreateTime", value);
				bap_CreateTime=value; 
			}
		}
      
		private int bap_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Bap_Creator 
		{ 
			get
			{
				return bap_Creator;
			} 
			set 
			{
				AuditCheck("Bap_Creator", value);
				bap_Creator=value; 
			}
		}
      
		private DateTime bap_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bap_UpdateTime 
		{ 
			get
			{
				return bap_UpdateTime;
			} 
			set 
			{
				AuditCheck("Bap_UpdateTime", value);
				bap_UpdateTime=value; 
			}
		}
      
		private int bap_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Bap_Editor 
		{ 
			get
			{
				return bap_Editor;
			} 
			set 
			{
				AuditCheck("Bap_Editor", value);
				bap_Editor=value; 
			}
		}
      
    }

}

