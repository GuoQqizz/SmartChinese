using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_CourseSubjectRelation")]
    public class Yw_CourseSubjectRelation : EntityBase
    {
        [Key]
        public int Ysr_Id { get; set; }

        private int ysr_CourseId;
        public int Ysr_CourseId
        {
            set
            {
                AuditCheck("Ysr_CourseId", value);
                ysr_CourseId = value;
            }
            get
            {
                return ysr_CourseId;
            }
        }

        private int ysr_LessonId;
        public int Ysr_LessonId
        {
            set
            {
                AuditCheck("Ysr_LessonId", value);
                ysr_LessonId = value;
            }
            get
            {
                return ysr_LessonId;
            }
        }

        private int ysr_LessonIndex;
        public int Ysr_LessonIndex
        {
            set
            {
                AuditCheck("Ysr_LessonIndex", value);
                ysr_LessonIndex = value;
            }
            get
            {
                return ysr_LessonIndex;
            }
        }

        private int ysr_SubjectId;
        public int Ysr_SubjectId
        {
            set
            {
                AuditCheck("Ysr_SubjectId", value);
                ysr_SubjectId = value;
            }
            get
            {
                return ysr_SubjectId;
            }
        }

        private int ysr_SubjectType;
        public int Ysr_SubjectType
        {
            set
            {
                AuditCheck("Ysr_SubjectType", value);
                ysr_SubjectType = value;
            }
            get
            {
                return ysr_SubjectType;
            }
        }

        private bool ysr_IsDirect;
        public bool Ysr_IsDirect
        {
            set
            {
                AuditCheck("Ysr_IsDirect", value);
                ysr_IsDirect = value;
            }
            get
            {
                return ysr_IsDirect;
            }
        }

        private int ysr_DirectSubjectId;
        public int Ysr_DirectSubjectId
        {
            set
            {
                AuditCheck("Ysr_DirectSubjectId", value);
                ysr_DirectSubjectId = value;
            }
            get
            {
                return ysr_DirectSubjectId;
            }
        }

        private DateTime ysr_CreateTime;
        public DateTime Ysr_CreateTime
        {
            set
            {
                AuditCheck("Ysr_CreateTime", value);
                ysr_CreateTime = value;
            }
            get
            {
                return ysr_CreateTime;
            }
        }
    }
}
