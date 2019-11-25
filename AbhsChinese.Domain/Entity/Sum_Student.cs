using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Sum_Student")]
    public class Sum_Student : EntityBase
    {
        [Key]
        public int Sst_StudentId { get; set; }

        private DateTime sst_FirstOrderTime;
        public DateTime Sst_FirstOrderTime
        {
            set
            {
                AuditCheck("Sst_FirstOrderTime", value);
                sst_FirstOrderTime = value;
            }
            get
            {
                return sst_FirstOrderTime;
            }
        }

        private string sst_LastLoginIp;
        public string Sst_LastLoginIp
        {
            set
            {
                AuditCheck("Sst_LastLoginIp", value);
                sst_LastLoginIp = value;
            }
            get
            {
                return sst_LastLoginIp;
            }
        }

        private string sst_LastLoginArea;
        public string Sst_LastLoginArea
        {
            set
            {
                AuditCheck("Sst_LastLoginArea", value);
                sst_LastLoginArea = value;
            }
            get
            {
                return sst_LastLoginArea;
            }
        }

        private DateTime sst_LastLoginTime;
        public DateTime Sst_LastLoginTime
        {
            set
            {
                AuditCheck("Sst_LastLoginTime", value);
                sst_LastLoginTime = value;
            }
            get
            {
                return sst_LastLoginTime;
            }
        }

        private int sst_OwnCourseCount;
        public int Sst_OwnCourseCount
        {
            set
            {
                AuditCheck("Sst_OwnCourseCount", value);
                sst_OwnCourseCount = value;
            }
            get
            {
                return sst_OwnCourseCount;
            }
        }

        private int sst_ExperiencePoints;
        public int Sst_ExperiencePoints
        {
            set
            {
                AuditCheck("Sst_ExperiencePoints", value);
                sst_ExperiencePoints = value;
            }
            get
            {
                return sst_ExperiencePoints;
            }
        }

        private int sst_Coins;
        public int Sst_Coins
        {
            set
            {
                AuditCheck("Sst_Coins", value);
                sst_Coins = value;
            }
            get
            {
                return sst_Coins;
            }
        }

        private int sst_StudyDayCount;
        public int Sst_StudyDayCount
        {
            set
            {
                AuditCheck("Sst_StudyDayCount", value);
                sst_StudyDayCount = value;
            }
            get
            {
                return sst_StudyDayCount;
            }
        }

        private DateTime sst_UpdateTime;
        public DateTime Sst_UpdateTime
        {
            set
            {
                AuditCheck("Sst_UpdateTime", value);
                sst_UpdateTime = value;
            }
            get
            {
                return sst_UpdateTime;
            }
        }
    }
}
