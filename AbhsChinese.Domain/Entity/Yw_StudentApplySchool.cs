using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
	[Table("Yw_StudentApplySchool")]
    public partial class Yw_StudentApplySchool:EntityBase
    {
        [Key]
        public int Yay_Id { get; set; }
		private int yay_StudentId;
		/// <summary>
        /// 
        /// </summary>
        public int Yay_StudentId 
		{ 
			get
			{
				return yay_StudentId;
			} 
			set 
			{
				AuditCheck("Yay_StudentId", value);
				yay_StudentId=value; 
			}
		}
      
		private int yay_SchoolId;
		/// <summary>
        /// 
        /// </summary>
        public int Yay_SchoolId 
		{ 
			get
			{
				return yay_SchoolId;
			} 
			set 
			{
				AuditCheck("Yay_SchoolId", value);
				yay_SchoolId=value; 
			}
		}
      
		private int yay_TeacherId;
		/// <summary>
        /// 
        /// </summary>
        public int Yay_TeacherId 
		{ 
			get
			{
				return yay_TeacherId;
			} 
			set 
			{
				AuditCheck("Yay_TeacherId", value);
				yay_TeacherId=value; 
			}
		}
      
		private int yay_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Yay_Status 
		{ 
			get
			{
				return yay_Status;
			} 
			set 
			{
				AuditCheck("Yay_Status", value);
				yay_Status=value; 
			}
		}
      
		private DateTime yay_ApplyTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yay_ApplyTime 
		{ 
			get
			{
				return yay_ApplyTime;
			} 
			set 
			{
				AuditCheck("Yay_ApplyTime", value);
				yay_ApplyTime=value; 
			}
		}
      
		private DateTime yay_OperateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yay_OperateTime 
		{ 
			get
			{
				return yay_OperateTime;
			} 
			set 
			{
				AuditCheck("Yay_OperateTime", value);
				yay_OperateTime=value; 
			}
		}
      
		private int yay_Operator;
		/// <summary>
        /// 
        /// </summary>
        public int Yay_Operator 
		{ 
			get
			{
				return yay_Operator;
			} 
			set 
			{
				AuditCheck("Yay_Operator", value);
				yay_Operator=value; 
			}
		}
      
    }

}

