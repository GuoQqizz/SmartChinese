using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
	[Table("Yw_StudentCourseProgress")]
    public partial class Yw_StudentCourseProgress:EntityBase
    {
        [Key]
        public int Yps_Id { get; set; }
		private int yps_StudentId;
		/// <summary>
        /// ѧ��id
        /// </summary>
        public int Yps_StudentId 
		{ 
			get
			{
				return yps_StudentId;
			} 
			set 
			{
				AuditCheck("Yps_StudentId", value);
				yps_StudentId=value; 
			}
		}
      
		private int yps_SchoolId;
		/// <summary>
        /// ѧУid
        /// </summary>
        public int Yps_SchoolId 
		{ 
			get
			{
				return yps_SchoolId;
			} 
			set 
			{
				AuditCheck("Yps_SchoolId", value);
				yps_SchoolId=value; 
			}
		}
      
		private int yps_CourseId;
		/// <summary>
        /// �γ�id
        /// </summary>
        public int Yps_CourseId 
		{ 
			get
			{
				return yps_CourseId;
			} 
			set 
			{
				AuditCheck("Yps_CourseId", value);
				yps_CourseId=value; 
			}
		}
      
		private int yps_LessonFinishedCount;
		/// <summary>
        /// ��ʱ�������
        /// </summary>
        public int Yps_LessonFinishedCount 
		{ 
			get
			{
				return yps_LessonFinishedCount;
			} 
			set 
			{
				AuditCheck("Yps_LessonFinishedCount", value);
				yps_LessonFinishedCount=value; 
			}
		}
      
		private DateTime yps_StartStudyTime;
		/// <summary>
        /// ��ʼѧϰʱ��
        /// </summary>
        public DateTime Yps_StartStudyTime 
		{ 
			get
			{
				return yps_StartStudyTime;
			} 
			set 
			{
				AuditCheck("Yps_StartStudyTime", value);
				yps_StartStudyTime=value; 
			}
		}
      
		private DateTime yps_FinishStudyTime;
		/// <summary>
        /// ���ѧϰʱ��
        /// </summary>
        public DateTime Yps_FinishStudyTime 
		{ 
			get
			{
				return yps_FinishStudyTime;
			} 
			set 
			{
				AuditCheck("Yps_FinishStudyTime", value);
				yps_FinishStudyTime=value; 
			}
		}
      
		private DateTime yps_LastStudyTime;
		/// <summary>
        /// ���һ��ѧϰʱ��
        /// </summary>
        public DateTime Yps_LastStudyTime 
		{ 
			get
			{
				return yps_LastStudyTime;
			} 
			set 
			{
				AuditCheck("Yps_LastStudyTime", value);
				yps_LastStudyTime=value; 
			}
		}
      
		private bool yps_IsFinished;
		/// <summary>
        /// �γ��Ƿ����
        /// </summary>
        public bool Yps_IsFinished 
		{ 
			get
			{
				return yps_IsFinished;
			} 
			set 
			{
				AuditCheck("Yps_IsFinished", value);
				yps_IsFinished=value; 
			}
		}
      
		private DateTime yps_CreateTime;
		/// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime Yps_CreateTime 
		{ 
			get
			{
				return yps_CreateTime;
			} 
			set 
			{
				AuditCheck("Yps_CreateTime", value);
				yps_CreateTime=value; 
			}
		}
      
		private DateTime yps_UpdateTime;
		/// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime Yps_UpdateTime 
		{ 
			get
			{
				return yps_UpdateTime;
			} 
			set 
			{
				AuditCheck("Yps_UpdateTime", value);
				yps_UpdateTime=value; 
			}
		}
      
		private int yps_NextLessonIndex;
		/// <summary>
        /// ��һ��ʱ���
        /// </summary>
        public int Yps_NextLessonIndex 
		{ 
			get
			{
				return yps_NextLessonIndex;
			} 
			set 
			{
				AuditCheck("Yps_NextLessonIndex", value);
				yps_NextLessonIndex=value; 
			}
		}
      
		private int yps_Status;
		/// <summary>
        /// ״̬
        /// </summary>
        public int Yps_Status 
		{ 
			get
			{
				return yps_Status;
			} 
			set 
			{
				AuditCheck("Yps_Status", value);
				yps_Status=value; 
			}
		}
      
		private int yps_ClassId;
		/// <summary>
        /// �༶id
        /// </summary>
        public int Yps_ClassId 
		{ 
			get
			{
				return yps_ClassId;
			} 
			set 
			{
				AuditCheck("Yps_ClassId", value);
				yps_ClassId=value; 
			}
		}
      
		private int yps_OrderId;
		/// <summary>
        /// 
        /// </summary>
        public int Yps_OrderId 
		{ 
			get
			{
				return yps_OrderId;
			} 
			set 
			{
				AuditCheck("Yps_OrderId", value);
				yps_OrderId=value; 
			}
		}
      
    }

}

