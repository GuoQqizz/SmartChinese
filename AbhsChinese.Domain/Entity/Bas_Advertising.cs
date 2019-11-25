using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
	[Table("Bas_Advertising")]
    public partial class Bas_Advertising:EntityBase
    {
        [Key]
        public int Bad_Id { get; set; }
		private int bad_PosId;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_PosId 
		{ 
			get
			{
				return bad_PosId;
			} 
			set 
			{
				AuditCheck("Bad_PosId", value);
				bad_PosId=value; 
			}
		}
      
		private int bad_ReferId;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_ReferId 
		{ 
			get
			{
				return bad_ReferId;
			} 
			set 
			{
				AuditCheck("Bad_ReferId", value);
				bad_ReferId=value; 
			}
		}
      
		private string bad_Url;
		/// <summary>
        /// 
        /// </summary>
        public string Bad_Url 
		{ 
			get
			{
				return bad_Url;
			} 
			set 
			{
				AuditCheck("Bad_Url", value);
				bad_Url=value; 
			}
		}
      
		private string bad_ImageUrl;
		/// <summary>
        /// 
        /// </summary>
        public string Bad_ImageUrl 
		{ 
			get
			{
				return bad_ImageUrl;
			} 
			set 
			{
				AuditCheck("Bad_ImageUrl", value);
				bad_ImageUrl=value; 
			}
		}
      
		private int bad_HitCount;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_HitCount 
		{ 
			get
			{
				return bad_HitCount;
			} 
			set 
			{
				AuditCheck("Bad_HitCount", value);
				bad_HitCount=value; 
			}
		}
      
		private int bad_ValidCount;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_ValidCount 
		{ 
			get
			{
				return bad_ValidCount;
			} 
			set 
			{
				AuditCheck("Bad_ValidCount", value);
				bad_ValidCount=value; 
			}
		}
      
		private int bad_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_Status 
		{ 
			get
			{
				return bad_Status;
			} 
			set 
			{
				AuditCheck("Bad_Status", value);
				bad_Status=value; 
			}
		}
      
		private DateTime bad_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bad_CreateTime 
		{ 
			get
			{
				return bad_CreateTime;
			} 
			set 
			{
				AuditCheck("Bad_CreateTime", value);
				bad_CreateTime=value; 
			}
		}
      
		private int bad_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Bad_Creator 
		{ 
			get
			{
				return bad_Creator;
			} 
			set 
			{
				AuditCheck("Bad_Creator", value);
				bad_Creator=value; 
			}
		}
      
		private DateTime bad_EndTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Bad_EndTime 
		{ 
			get
			{
				return bad_EndTime;
			} 
			set 
			{
				AuditCheck("Bad_EndTime", value);
				bad_EndTime=value; 
			}
		}
      
    }

}

