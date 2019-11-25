using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_ReadRecord")]
    public class Yw_ReadRecord : EntityBase
    {
        [Key]
        public int Yrr_Id { get; set; }

        private int yrr_StudentId;
        public int Yrr_StudentId
        {
            set
            {
                AuditCheck("Yrr_StudentId", value);
                yrr_StudentId = value;
            }
            get
            {
                return yrr_StudentId;
            }
        }

        private int yrr_CourseId;
        public int Yrr_CourseId
        {
            set
            {
                AuditCheck("Yrr_CourseId", value);
                yrr_CourseId = value;
            }
            get
            {
                return yrr_CourseId;
            }
        }

        private int yrr_LessonId;
        public int Yrr_LessonId
        {
            set
            {
                AuditCheck("Yrr_LessonId", value);
                yrr_LessonId = value;
            }
            get
            {
                return yrr_LessonId;
            }
        }

        private int yrr_UnitId;
        public int Yrr_UnitId
        {
            set
            {
                AuditCheck("Yrr_UnitId", value);
                yrr_UnitId = value;
            }
            get
            {
                return yrr_UnitId;
            }
        }

        private string yrr_ActionId;
        public string Yrr_ActionId
        {
            set
            {
                AuditCheck("Yrr_ActionId", value);
                yrr_ActionId = value;
            }
            get
            {
                return yrr_ActionId;
            }
        }

        private int yrr_Score;
        public int Yrr_Score
        {
            set
            {
                AuditCheck("Yrr_Score", value);
                yrr_Score = value;
            }
            get
            {
                return yrr_Score;
            }
        }

        private int yrr_BestScore;
        public int Yrr_BestScore
        {
            set
            {
                AuditCheck("Yrr_BestScore", value);
                yrr_BestScore = value;
            }
            get
            {
                return yrr_BestScore;
            }
        }
        
        private string yrr_Url;
        public string Yrr_Url
        {
            set
            {
                AuditCheck("Yrr_Url", value);
                yrr_Url = value;
            }
            get
            {
                return yrr_Url;
            }
        }

        private string yrr_BestUrl;
        public string Yrr_BestUrl
        {
            set
            {
                AuditCheck("Yrr_BestUrl", value);
                yrr_BestUrl = value;
            }
            get
            {
                return yrr_BestUrl;
            }
        }

        private DateTime yrr_CreateTime;
        public DateTime Yrr_CreateTime
        {
            set
            {
                AuditCheck("Yrr_CreateTime", value);
                yrr_CreateTime = value;
            }
            get
            {
                return yrr_CreateTime;
            }
        }
    }
}
