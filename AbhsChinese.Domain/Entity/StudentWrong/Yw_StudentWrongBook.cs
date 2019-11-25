using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentWrong
{
	[Table("Yw_StudentWrongBook")]
    public partial class Yw_StudentWrongBook:EntityBase
    {
        [Key]
        public int Ywb_Id { get; set; }
		private int ywb_StudentId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywb_StudentId 
		{ 
			get
			{
				return ywb_StudentId;
			} 
			set 
			{
				AuditCheck("Ywb_StudentId", value);
				ywb_StudentId=value; 
			}
		}
      
		private int ywb_CourseId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywb_CourseId 
		{ 
			get
			{
				return ywb_CourseId;
			} 
			set 
			{
				AuditCheck("Ywb_CourseId", value);
				ywb_CourseId=value; 
			}
		}
      
		private int ywb_LessonId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywb_LessonId 
		{ 
			get
			{
				return ywb_LessonId;
			} 
			set 
			{
				AuditCheck("Ywb_LessonId", value);
				ywb_LessonId=value; 
			}
		}
      
		private int ywb_LessonProgressId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywb_LessonProgressId 
		{ 
			get
			{
				return ywb_LessonProgressId;
			} 
			set 
			{
				AuditCheck("Ywb_LessonProgressId", value);
				ywb_LessonProgressId=value; 
			}
		}
      
		private int ywb_StudyTaskId;
		/// <summary>
        /// 
        /// </summary>
        public int Ywb_StudyTaskId 
		{ 
			get
			{
				return ywb_StudyTaskId;
			} 
			set 
			{
				AuditCheck("Ywb_StudyTaskId", value);
				ywb_StudyTaskId=value; 
			}
		}
      
		private int yws_SchoolId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_SchoolId 
		{ 
			get
			{
				return yws_SchoolId;
			} 
			set 
			{
				AuditCheck("Yws_SchoolId", value);
				yws_SchoolId=value; 
			}
		}
      
		private int yws_ClassId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_ClassId 
		{ 
			get
			{
				return yws_ClassId;
			} 
			set 
			{
				AuditCheck("Yws_ClassId", value);
				yws_ClassId=value; 
			}
		}
      
		private int yws_Source;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_Source 
		{ 
			get
			{
				return yws_Source;
			} 
			set 
			{
				AuditCheck("Yws_Source", value);
				yws_Source=value; 
			}
		}
      
		private int yws_WrongCount;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_WrongCount 
		{ 
			get
			{
				return yws_WrongCount;
			} 
			set 
			{
				AuditCheck("Yws_WrongCount", value);
				yws_WrongCount=value; 
			}
		}
      
		private int yws_WrongKnowledgeCount;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_WrongKnowledgeCount 
		{ 
			get
			{
				return yws_WrongKnowledgeCount;
			} 
			set 
			{
				AuditCheck("Yws_WrongKnowledgeCount", value);
				yws_WrongKnowledgeCount=value; 
			}
		}
      
		private int yws_RemoveCount;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_RemoveCount 
		{ 
			get
			{
				return yws_RemoveCount;
			} 
			set 
			{
				AuditCheck("Yws_RemoveCount", value);
				yws_RemoveCount=value; 
			}
		}
      
		private int yws_Status;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_Status 
		{ 
			get
			{
				return yws_Status;
			} 
			set 
			{
				AuditCheck("Yws_Status", value);
				yws_Status=value; 
			}
		}
      
		private DateTime yws_CreateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yws_CreateTime 
		{ 
			get
			{
				return yws_CreateTime;
			} 
			set 
			{
				AuditCheck("Yws_CreateTime", value);
				yws_CreateTime=value; 
			}
		}
      
		private DateTime yws_UpdateTime;
		/// <summary>
        /// 
        /// </summary>
        public DateTime Yws_UpdateTime 
		{ 
			get
			{
				return yws_UpdateTime;
			} 
			set 
			{
				AuditCheck("Yws_UpdateTime", value);
				yws_UpdateTime=value; 
			}
		}
      
    }

}

