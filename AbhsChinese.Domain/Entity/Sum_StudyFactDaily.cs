using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Sum_StudyFactDaily")]
    public class Sum_StudyFactDaily : EntityBase
    {
        [Key]
        public int Ssd_Id { get; set; }

        private int ssd_StudentId;
        public int Ssd_StudentId
        {
            set
            {
                AuditCheck("Ssd_StudentId", value);
                ssd_StudentId = value;
            }
            get
            {
                return ssd_StudentId;
            }
        }

        private int ssd_ClassId;
        public int Ssd_ClassId
        {
            set
            {
                AuditCheck("Ssd_ClassId", value);
                ssd_ClassId = value;
            }
            get
            {
                return ssd_ClassId;
            }
        }

        private int ssd_SchoolId;
        public int Ssd_SchoolId
        {
            set
            {
                AuditCheck("Ssd_SchoolId", value);
                ssd_SchoolId = value;
            }
            get
            {
                return ssd_SchoolId;
            }
        }
        
        private DateTime ssd_Date;
        public DateTime Ssd_Date
        {
            set
            {
                AuditCheck("Ssd_Date", value);
                ssd_Date = value;
            }
            get
            {
                return ssd_Date;
            }
        }

        private int ssd_StudySeconds;
        public int Ssd_StudySeconds
        {
            set
            {
                AuditCheck("Ssd_StudySeconds", value);
                ssd_StudySeconds = value;
            }
            get
            {
                return ssd_StudySeconds;
            }
        }

        private int ssd_TaskCount;
        public int Ssd_TaskCount
        {
            set
            {
                AuditCheck("Ssd_TaskCount", value);
                ssd_TaskCount = value;
            }
            get
            {
                return ssd_TaskCount;
            }
        }

        private int ssd_PracticeCount;
        public int Ssd_PracticeCount
        {
            set
            {
                AuditCheck("Ssd_PracticeCount", value);
                ssd_PracticeCount = value;
            }
            get
            {
                return ssd_PracticeCount;
            }
        }

        private int ssd_SubjectCount;
        public int Ssd_SubjectCount
        {
            set
            {
                AuditCheck("Ssd_SubjectCount", value);
                ssd_SubjectCount = value;
            }
            get
            {
                return ssd_SubjectCount;
            }
        }

        private int ssd_WrongSubjectCount;
        public int Ssd_WrongSubjectCount
        {
            set
            {
                AuditCheck("Ssd_WrongSubjectCount", value);
                ssd_WrongSubjectCount = value;
            }
            get
            {
                return ssd_WrongSubjectCount;
            }
        }

        private int ssd_ExperiencePoints;
        public int Ssd_ExperiencePoints
        {
            set
            {
                AuditCheck("Ssd_ExperiencePoints", value);
                ssd_ExperiencePoints = value;
            }
            get
            {
                return ssd_ExperiencePoints;
            }
        }

        private int ssd_IncomeCoins;
        public int Ssd_IncomeCoins
        {
            set
            {
                AuditCheck("Ssd_IncomeCoins", value);
                ssd_IncomeCoins = value;
            }
            get
            {
                return ssd_IncomeCoins;
            }
        }

        private DateTime ssd_UpdateTime;
        public DateTime Ssd_UpdateTime
        {
            set
            {
                AuditCheck("Ssd_UpdateTime", value);
                ssd_UpdateTime = value;
            }
            get
            {
                return ssd_UpdateTime;
            }
        }
    }
}
