using System;

namespace AbhsChinese.Domain.Entity
{
    [Table("Sum_StudyFactDateCycle")]
    public class Sum_StudyFactDateCycle : EntityBase
    {
        [Key]
        public int Sdc_Id { get; set; }

        private int sdc_StudentId;
        public int Sdc_StudentId
        {
            set
            {
                AuditCheck("Sdc_StudentId", value);
                sdc_StudentId = value;
            }
            get
            {
                return sdc_StudentId;
            }
        }

        private int sdc_SchoolId;
        public int Sdc_SchoolId
        {
            set
            {
                AuditCheck("Sdc_SchoolId", value);
                sdc_SchoolId = value;
            }
            get
            {
                return sdc_SchoolId;
            }
        }

        private int sdc_ClassId;
        public int Sdc_ClassId
        {
            set
            {
                AuditCheck("Sdc_ClassId", value);
                sdc_ClassId = value;
            }
            get
            {
                return sdc_ClassId;
            }
        }

        private DateTime sdc_Date;
        public DateTime Sdc_Date
        {
            set
            {
                AuditCheck("Sdc_Date", value);
                sdc_Date = value;
            }
            get
            {
                return sdc_Date;
            }
        }

        private int sdc_CycleType;
        public int Sdc_CycleType
        {
            set
            {
                AuditCheck("Sdc_CycleType", value);
                sdc_CycleType = value;
            }
            get
            {
                return sdc_CycleType;
            }
        }

        private int sdc_StudyDayCount;
        public int Sdc_StudyDayCount
        {
            set
            {
                AuditCheck("Sdc_StudyDayCount", value);
                sdc_StudyDayCount = value;
            }
            get
            {
                return sdc_StudyDayCount;
            }
        }

        private int sdc_StudySeconds;
        public int Sdc_StudySeconds
        {
            set
            {
                AuditCheck("Sdc_StudySeconds", value);
                sdc_StudySeconds = value;
            }
            get
            {
                return sdc_StudySeconds;
            }
        }

        private int sdc_TaskCount;
        public int Sdc_TaskCount
        {
            set
            {
                AuditCheck("Sdc_TaskCount", value);
                sdc_TaskCount = value;
            }
            get
            {
                return sdc_TaskCount;
            }
        }

        private int sdc_PracticeCount;
        public int Sdc_PracticeCount
        {
            set
            {
                AuditCheck("Sdc_PracticeCount", value);
                sdc_PracticeCount = value;
            }
            get
            {
                return sdc_PracticeCount;
            }
        }

        private int sdc_SubjectCount;
        public int Sdc_SubjectCount
        {
            set
            {
                AuditCheck("Sdc_SubjectCount", value);
                sdc_SubjectCount = value;
            }
            get
            {
                return sdc_SubjectCount;
            }
        }

        private int sdc_WrongSubjectCount;
        public int Sdc_WrongSubjectCount
        {
            set
            {
                AuditCheck("Sdc_WrongSubjectCount", value);
                sdc_WrongSubjectCount = value;
            }
            get
            {
                return sdc_WrongSubjectCount;
            }
        }

        private int sdc_ExperiencePoints;
        public int Sdc_ExperiencePoints
        {
            set
            {
                AuditCheck("Sdc_ExperiencePoints", value);
                sdc_ExperiencePoints = value;
            }
            get
            {
                return sdc_ExperiencePoints;
            }
        }

        private int sdc_IncomeCoins;
        public int Sdc_IncomeCoins
        {
            set
            {
                AuditCheck("Sdc_IncomeCoins", value);
                sdc_IncomeCoins = value;
            }
            get
            {
                return sdc_IncomeCoins;
            }
        }

        private DateTime sdc_UpdateTime;
        public DateTime Sdc_UpdateTime
        {
            set
            {
                AuditCheck("Sdc_UpdateTime", value);
                sdc_UpdateTime = value;
            }
            get
            {
                return sdc_UpdateTime;
            }
        }
    }
}
