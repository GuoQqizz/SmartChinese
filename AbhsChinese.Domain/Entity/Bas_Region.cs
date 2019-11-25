using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
	[Table("Bas_Region")]
    public partial class Bas_Region:EntityBase
    {
        [Key]
        public int Reg_ID { get; set; }
		private string reg_Name;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_Name 
		{ 
			get
			{
				return reg_Name;
			} 
			set 
			{
				AuditCheck("Reg_Name", value);
				reg_Name=value; 
			}
		}
      
		private int reg_ParentID;
		/// <summary>
        /// 
        /// </summary>
        public int Reg_ParentID 
		{ 
			get
			{
				return reg_ParentID;
			} 
			set 
			{
				AuditCheck("Reg_ParentID", value);
				reg_ParentID=value; 
			}
		}
      
		private string reg_Level;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_Level 
		{ 
			get
			{
				return reg_Level;
			} 
			set 
			{
				AuditCheck("Reg_Level", value);
				reg_Level=value; 
			}
		}
      
		private string reg_FullName;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_FullName 
		{ 
			get
			{
				return reg_FullName;
			} 
			set 
			{
				AuditCheck("Reg_FullName", value);
				reg_FullName=value; 
			}
		}
      
		private string reg_Pinyin;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_Pinyin 
		{ 
			get
			{
				return reg_Pinyin;
			} 
			set 
			{
				AuditCheck("Reg_Pinyin", value);
				reg_Pinyin=value; 
			}
		}
      
		private string reg_Alias;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_Alias 
		{ 
			get
			{
				return reg_Alias;
			} 
			set 
			{
				AuditCheck("Reg_Alias", value);
				reg_Alias=value; 
			}
		}
      
		private string reg_CPY;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_CPY 
		{ 
			get
			{
				return reg_CPY;
			} 
			set 
			{
				AuditCheck("Reg_CPY", value);
				reg_CPY=value; 
			}
		}
      
		private string reg_GBCode;
		/// <summary>
        /// 
        /// </summary>
        public string Reg_GBCode 
		{ 
			get
			{
				return reg_GBCode;
			} 
			set 
			{
				AuditCheck("Reg_GBCode", value);
				reg_GBCode=value; 
			}
		}
      
    }

}

