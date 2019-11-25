using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentRecentSubject")]
    public class Yw_StudentRecentSubject : EntityBase
    {
        [Key]
        public int Yrs_Id { get; set; }

        private int yrs_StudentId;
        public int Yrs_StudentId
        {
            set
            {
                AuditCheck("Yrs_StudentId", value);
                yrs_StudentId = value;
            }
            get
            {
                return yrs_StudentId;
            }
        }

        private int yrs_SubjectId;
        public int Yrs_SubjectId
        {
            set
            {
                AuditCheck("Yrs_SubjectId", value);
                yrs_SubjectId = value;
            }
            get
            {
                return yrs_SubjectId;
            }
        }

        private bool yrs_IsLastest;
        public bool Yrs_IsLastest
        {
            set
            {
                AuditCheck("Yrs_IsLastest", value);
                yrs_IsLastest = value;
            }
            get
            {
                return yrs_IsLastest;
            }
        }

        private DateTime yrs_CreateTime;
        public DateTime Yrs_CreateTime
        {
            set
            {
                AuditCheck("Yrs_CreateTime", value);
                yrs_CreateTime = value;
            }
            get
            {
                return yrs_CreateTime;
            }
        }
    }
}
