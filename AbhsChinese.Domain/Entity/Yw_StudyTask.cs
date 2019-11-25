using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudyTask")]
    public class Yw_StudyTask : EntityBase
    {
        [Key]
        public int Ysk_Id { get; set; }

        private int ysk_CourseId;
        public int Ysk_CourseId
        {
            get
            {
                return ysk_CourseId;
            }
            set
            {
                AuditCheck("Ysk_CourseId", value);
                ysk_CourseId = value;
            }
        }

        private int ysk_LessonId;
        public int Ysk_LessonId
        {
            get
            {
                return ysk_LessonId;
            }
            set
            {
                AuditCheck("Ysk_LessonId", value);
                ysk_LessonId = value;
            }
        }

        private int ysk_LessonIndex;
        public int Ysk_LessonIndex
        {
            get
            {
                return ysk_LessonIndex;
            }
            set
            {
                AuditCheck("Ysk_LessonIndex", value);
                ysk_LessonIndex = value;
            }
        }

        private int ysk_LessonProgressId;
        public int Ysk_LessonProgressId
        {
            get
            {
                return ysk_LessonProgressId;
            }
            set
            {
                AuditCheck("Ysk_LessonProgressId", value);
                ysk_LessonProgressId = value;
            }
        }

        private int ysk_TaskType;
        public int Ysk_TaskType
        {
            get
            {
                return ysk_TaskType;
            }
            set
            {
                AuditCheck("Ysk_TaskType", value);
                ysk_TaskType = value;
            }
        }

        private int ysk_SchoolId;
        public int Ysk_SchoolId
        {
            get
            {
                return ysk_SchoolId;
            }
            set
            {
                AuditCheck("Ysk_SchoolId", value);
                ysk_SchoolId = value;
            }
        }

        private int ysk_ClassId;
        public int Ysk_ClassId
        {
            get
            {
                return ysk_ClassId;
            }
            set
            {
                AuditCheck("Ysk_ClassId", value);
                ysk_ClassId = value;
            }
        }

        private int ysk_SubjectCount;
        public int Ysk_SubjectCount
        {
            get
            {
                return ysk_SubjectCount;
            }
            set
            {
                AuditCheck("Ysk_SubjectCount", value);
                ysk_SubjectCount = value;
            }
        }

        private string ysk_SubjectIds;
        public string Ysk_SubjectIds
        {
            get
            {
                return ysk_SubjectIds;
            }
            set
            {
                AuditCheck("Ysk_SubjectIds", value);
                ysk_SubjectIds = value;
            }
        }

        private int ysk_Score;
        public int Ysk_Score
        {
            get
            {
                return ysk_Score;
            }
            set
            {
                AuditCheck("Ysk_Score", value);
                ysk_Score = value;
            }
        }

        private DateTime ysk_CreateTime;
        public DateTime Ysk_CreateTime
        {
            get
            {
                return ysk_CreateTime;
            }
            set
            {
                AuditCheck("Ysk_CreateTime", value);
                ysk_CreateTime = value;
            }
        }

        private DateTime ysk_ExpiredTime;
        public DateTime Ysk_ExpiredTime
        {
            get
            {
                return ysk_ExpiredTime;
            }
            set
            {
                AuditCheck("Ysk_ExpiredTime", value);
                ysk_ExpiredTime = value;
            }
        }

        private int ysk_Status;
        public int Ysk_Status
        {
            get
            {
                return ysk_Status;
            }
            set
            {
                AuditCheck("Ysk_Status", value);
                ysk_Status = value;
            }
        }

        private int ysk_TeacherId;
        public int Ysk_TeacherId
        {
            get
            {
                return ysk_TeacherId;
            }
            set
            {
                AuditCheck("Ysk_TeacherId", value);
                ysk_TeacherId = value;
            }
        }
    }
}
