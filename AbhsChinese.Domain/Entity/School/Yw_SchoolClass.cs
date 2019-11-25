using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.School
{
	[Table("Yw_SchoolClass")]
    public partial class Yw_SchoolClass:EntityBase
    {
        [Key]
        public int Ycc_Id { get; set; }
		private int ycc_SchoolId;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_SchoolId 
		{ 
			get
			{
				return ycc_SchoolId;
			} 
			set 
			{
				AuditCheck("Ycc_SchoolId", value);
				ycc_SchoolId=value; 
			}
		}
      
		private string ycc_Name;
		/// <summary>
        /// 
        /// </summary>
        public string Ycc_Name 
		{ 
			get
			{
				return ycc_Name;
			} 
			set 
			{
				AuditCheck("Ycc_Name", value);
				ycc_Name=value; 
			}
		}
      
		private int ycc_Grade;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_Grade 
		{ 
			get
			{
				return ycc_Grade;
			} 
			set 
			{
				AuditCheck("Ycc_Grade", value);
				ycc_Grade=value; 
			}
		}
      
		private int ycc_CourseType;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_CourseType 
		{ 
			get
			{
				return ycc_CourseType;
			} 
			set 
			{
				AuditCheck("Ycc_CourseType", value);
				ycc_CourseType=value; 
			}
		}
      
		private int ycc_CourseId;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_CourseId 
		{ 
			get
			{
				return ycc_CourseId;
			} 
			set 
			{
				AuditCheck("Ycc_CourseId", value);
				ycc_CourseId=value; 
			}
		}
      
		private int ycc_ClassMaster;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_ClassMaster 
		{ 
			get
			{
				return ycc_ClassMaster;
			} 
			set 
			{
				AuditCheck("Ycc_ClassMaster", value);
				ycc_ClassMaster=value; 
			}
		}
      
		private int ycc_LimitStudentCount;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_LimitStudentCount 
		{ 
			get
			{
				return ycc_LimitStudentCount;
			} 
			set 
			{
				AuditCheck("Ycc_LimitStudentCount", value);
				ycc_LimitStudentCount=value; 
			}
		}
      
		private int ycc_StudentCount;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_StudentCount 
		{ 
			get
			{
				return ycc_StudentCount;
			} 
			set 
			{
				AuditCheck("Ycc_StudentCount", value);
				ycc_StudentCount=value; 
			}
		}
      
		private DateTime ycc_StartTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Ycc_StartTime 
		{ 
			get
			{
				return ycc_StartTime;
			} 
			set 
			{
				AuditCheck("Ycc_StartTime", value);
				ycc_StartTime=value; 
			}
		}
      
		private string ycc_Remark;
		/// <summary>
        /// 
        /// </summary>
        public string Ycc_Remark 
		{ 
			get
			{
				return ycc_Remark;
			} 
			set 
			{
				AuditCheck("Ycc_Remark", value);
				ycc_Remark=value; 
			}
		}
      
		private int ycc_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_Status 
		{ 
			get
			{
				return ycc_Status;
			} 
			set 
			{
				AuditCheck("Ycc_Status", value);
				ycc_Status=value; 
			}
		}
      
		private DateTime ycc_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Ycc_CreateTime 
		{ 
			get
			{
				return ycc_CreateTime;
			} 
			set 
			{
				AuditCheck("Ycc_CreateTime", value);
				ycc_CreateTime=value; 
			}
		}
      
		private int ycc_Creator;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_Creator 
		{ 
			get
			{
				return ycc_Creator;
			} 
			set 
			{
				AuditCheck("Ycc_Creator", value);
				ycc_Creator=value; 
			}
		}
      
		private DateTime ycc_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Ycc_UpdateTime 
		{ 
			get
			{
				return ycc_UpdateTime;
			} 
			set 
			{
				AuditCheck("Ycc_UpdateTime", value);
				ycc_UpdateTime=value; 
			}
		}
      
		private int ycc_Editor;
		/// <summary>
        /// 
        /// </summary>
        public int Ycc_Editor 
		{ 
			get
			{
				return ycc_Editor;
			} 
			set 
			{
				AuditCheck("Ycc_Editor", value);
				ycc_Editor=value; 
			}
		}
      
    }

}

