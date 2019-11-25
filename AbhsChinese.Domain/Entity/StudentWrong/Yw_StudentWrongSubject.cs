using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentWrong
{
	[Table("Yw_StudentWrongSubject")]
    public partial class Yw_StudentWrongSubject:EntityBase
    {
        [Key]
        public int Yws_Id { get; set; }
		private int yws_StudentId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_StudentId 
		{ 
			get
			{
				return yws_StudentId;
			} 
			set 
			{
				AuditCheck("Yws_StudentId", value);
				yws_StudentId=value; 
			}
		}
      
		private int yws_CourseId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_CourseId 
		{ 
			get
			{
				return yws_CourseId;
			} 
			set 
			{
				AuditCheck("Yws_CourseId", value);
				yws_CourseId=value; 
			}
		}
      
		private int yws_LessonId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_LessonId 
		{ 
			get
			{
				return yws_LessonId;
			} 
			set 
			{
				AuditCheck("Yws_LessonId", value);
				yws_LessonId=value; 
			}
		}
      
		private int yws_WrongBookId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_WrongBookId 
		{ 
			get
			{
				return yws_WrongBookId;
			} 
			set 
			{
				AuditCheck("Yws_WrongBookId", value);
				yws_WrongBookId=value; 
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
      
		private int yws_WrongSubjectId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_WrongSubjectId 
		{ 
			get
			{
				return yws_WrongSubjectId;
			} 
			set 
			{
				AuditCheck("Yws_WrongSubjectId", value);
				yws_WrongSubjectId=value; 
			}
		}
      
		private int yws_SubjectType;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_SubjectType 
		{ 
			get
			{
				return yws_SubjectType;
			} 
			set 
			{
				AuditCheck("Yws_SubjectType", value);
				yws_SubjectType=value; 
			}
		}
      
		private int yws_KnowledgeId;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_KnowledgeId 
		{ 
			get
			{
				return yws_KnowledgeId;
			} 
			set 
			{
				AuditCheck("Yws_KnowledgeId", value);
				yws_KnowledgeId=value; 
			}
		}
      
		private string yws_StudentAnswer;
		/// <summary>
        /// 
        /// </summary>
        public string Yws_StudentAnswer 
		{ 
			get
			{
				return yws_StudentAnswer;
			} 
			set 
			{
				AuditCheck("Yws_StudentAnswer", value);
				yws_StudentAnswer=value; 
			}
		}
      
		private int yws_RemoveTryCount;
		/// <summary>
        /// 
        /// </summary>
        public int Yws_RemoveTryCount 
		{ 
			get
			{
				return yws_RemoveTryCount;
			} 
			set 
			{
				AuditCheck("Yws_RemoveTryCount", value);
				yws_RemoveTryCount=value; 
			}
		}
      
		private int yws_Status;
        /// <summary>
        /// <see cref="Enum.StudyWrongStatusEnum"/>
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

