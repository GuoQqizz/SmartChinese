using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentTask")]
    public class Yw_StudentTask : EntityBase
    {
        [Key]
        public int Yuk_Id { get; set; }

        private int yuk_StudentId;
        public int Yuk_StudentId
        {
            get
            {
                return yuk_StudentId;
            }
            set
            {
                AuditCheck("Yuk_StudentId", value);
                yuk_StudentId = value;
            }
        }

        private int yuk_SchoolId;
        public int Yuk_SchoolId
        {
            get
            {
                return yuk_SchoolId;
            }
            set
            {
                AuditCheck("Yuk_SchoolId", value);
                yuk_SchoolId = value;
            }
        }

        private int yuk_TaskId;
        public int Yuk_TaskId
        {
            get
            {
                return yuk_TaskId;
            }
            set
            {
                AuditCheck("Yuk_TaskId", value);
                yuk_TaskId = value;
            }
        }

        private int yuk_CourseId;
        public int Yuk_CourseId
        {
            get
            {
                return yuk_CourseId;
            }
            set
            {
                AuditCheck("Yuk_CourseId", value);
                yuk_CourseId = value;
            }
        }

        private int yuk_LessonId;
        public int Yuk_LessonId
        {
            get
            {
                return yuk_LessonId;
            }
            set
            {
                AuditCheck("Yuk_LessonId", value);
                yuk_LessonId = value;
            }
        }

        private int yuk_LessonIndex;
        public int Yuk_LessonIndex
        {
            get
            {
                return yuk_LessonIndex;
            }
            set
            {
                AuditCheck("Yuk_LessonIndex", value);
                yuk_LessonIndex = value;
            }
        }

        private int yuk_TaskType;
        public int Yuk_TaskType
        {
            get
            {
                return yuk_TaskType;
            }
            set
            {
                AuditCheck("Yuk_TaskType", value);
                yuk_TaskType = value;
            }
        }

        private int yuk_SubjectCount;
        public int Yuk_SubjectCount
        {
            get
            {
                return yuk_SubjectCount;
            }
            set
            {
                AuditCheck("Yuk_SubjectCount", value);
                yuk_SubjectCount = value;
            }
        }

        private DateTime yuk_StartTime;
        public DateTime Yuk_StartTime
        {
            get
            {
                return yuk_StartTime;
            }
            set
            {
                AuditCheck("Yuk_StartTime", value);
                yuk_StartTime = value;
            }
        }

        private DateTime yuk_FinishTime;
        public DateTime Yuk_FinishTime
        {
            get
            {
                return yuk_FinishTime;
            }
            set
            {
                AuditCheck("Yuk_FinishTime", value);
                yuk_FinishTime = value;
            }
        }

        private DateTime yuk_ExpiredTime;
        public DateTime Yuk_ExpiredTime
        {
            get
            {
                return yuk_ExpiredTime;
            }
            set
            {
                AuditCheck("Yuk_ExpiredTime", value);
                yuk_ExpiredTime = value;
            }
        }

        private int yuk_StudentScore;
        public int Yuk_StudentScore
        {
            get
            {
                return yuk_StudentScore;
            }
            set
            {
                AuditCheck("Yuk_StudentScore", value);
                yuk_StudentScore = value;
            }
        }

        private int yuk_RightSubjectCount;
        public int Yuk_RightSubjectCount
        {
            get
            {
                return yuk_RightSubjectCount;
            }
            set
            {
                AuditCheck("Yuk_RightSubjectCount", value);
                yuk_RightSubjectCount = value;
            }
        }

        private int yuk_GainCoins;
        public int Yuk_GainCoins
        {
            get
            {
                return yuk_GainCoins;
            }
            set
            {
                AuditCheck("Yuk_GainCoins", value);
                yuk_GainCoins = value;
            }
        }

        private int yuk_Percent;
        public int Yuk_Percent
        {
            get
            {
                return yuk_Percent;
            }
            set
            {
                AuditCheck("Yuk_Percent", value);
                yuk_Percent = value;
            }
        }

        private int yuk_Status;
        public int Yuk_Status
        {
            get
            {
                return yuk_Status;
            }
            set
            {
                AuditCheck("Yuk_Status", value);
                yuk_Status = value;
            }
        }

        private DateTime yuk_CreateTime;
        public DateTime Yuk_CreateTime
        {
            get
            {
                return yuk_CreateTime;
            }
            set
            {
                AuditCheck("Yuk_CreateTime", value);
                yuk_CreateTime = value;
            }
        }
    }
}
