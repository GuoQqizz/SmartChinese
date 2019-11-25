using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.School
{
	[Table("Yw_SchoolClassSchedule")]
    public partial class Yw_SchoolClassSchedule:EntityBase
    {
        [Key]
        public int Ywd_Id { get; set; }
		private int ywd_SchoolId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywd_SchoolId 
		{ 
			get
			{
				return ywd_SchoolId;
			} 
			set 
			{
				AuditCheck("Ywd_SchoolId", value);
				ywd_SchoolId=value; 
			}
		}
      
		private int ywd_ClassId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywd_ClassId 
		{ 
			get
			{
				return ywd_ClassId;
			} 
			set 
			{
				AuditCheck("Ywd_ClassId", value);
				ywd_ClassId=value; 
			}
		}
      
		private int ywd_Day;
		/// <summary>
        /// 
        /// </summary>
        public int Ywd_Day 
		{ 
			get
			{
				return ywd_Day;
			} 
			set 
			{
				AuditCheck("Ywd_Day", value);
				ywd_Day=value; 
			}
		}
      
		private TimeSpan ywd_StartTime;
		/// <summary>
        /// 
        /// </summary>
        public TimeSpan Ywd_StartTime 
		{ 
			get
			{
				return ywd_StartTime;
			} 
			set 
			{
				AuditCheck("Ywd_StartTime", value);
				ywd_StartTime=value; 
			}
		}
      
		private TimeSpan ywd_EndTime;
		/// <summary>
        /// 
        /// </summary>
        public TimeSpan Ywd_EndTime 
		{ 
			get
			{
				return ywd_EndTime;
			} 
			set 
			{
				AuditCheck("Ywd_EndTime", value);
				ywd_EndTime=value; 
			}
		}
      
		private DateTime ywd_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Ywd_CreateTime 
		{ 
			get
			{
				return ywd_CreateTime;
			} 
			set 
			{
				AuditCheck("Ywd_CreateTime", value);
				ywd_CreateTime=value; 
			}
		}
      
		private int ywd_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Ywd_Creator 
		{ 
			get
			{
				return ywd_Creator;
			} 
			set 
			{
				AuditCheck("Ywd_Creator", value);
				ywd_Creator=value; 
			}
		}
      
		private DateTime ywd_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Ywd_UpdateTime 
		{ 
			get
			{
				return ywd_UpdateTime;
			} 
			set 
			{
				AuditCheck("Ywd_UpdateTime", value);
				ywd_UpdateTime=value; 
			}
		}
      
		private int ywd_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Ywd_Editor 
		{ 
			get
			{
				return ywd_Editor;
			} 
			set 
			{
				AuditCheck("Ywd_Editor", value);
				ywd_Editor=value; 
			}
		}
      
    }

}

