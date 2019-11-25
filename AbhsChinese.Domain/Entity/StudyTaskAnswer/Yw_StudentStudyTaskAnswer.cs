using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentStudyTaskAnswer")]
    public class Yw_StudentStudyTaskAnswer : EntityBase
    {
        [Key]
        public int Yta_Id { get; set; }

        private int yta_StudentId;
        public int Yta_StudentId
        {
            get
            {
                return yta_StudentId;
            }
            set
            {
                AuditCheck("Yta_StudentId", value);
                yta_StudentId = value;
            }
        }

        private int yta_TaskId;
        public int Yta_TaskId
        {
            get
            {
                return yta_TaskId;
            }
            set
            {
                AuditCheck("Yta_TaskId", value);
                yta_TaskId = value;
            }
        }

        private int yta_StudentStudyTaskId;
        public int Yta_StudentStudyTaskId
        {
            get
            {
                return yta_StudentStudyTaskId;
            }
            set
            {
                AuditCheck("Yta_StudentStudyTaskId", value);
                yta_StudentStudyTaskId = value;
            }
        }

        private string yta_StudentAnswer;
        public string Yta_StudentAnswer
        {
            get
            {
                return yta_StudentAnswer;
            }
            set
            {
                AuditCheck("Yta_StudentAnswer", value);
                yta_StudentAnswer = value;
            }
        }

        private DateTime yta_CreateTime;
        public DateTime Yta_CreateTime
        {
            get
            {
                return yta_CreateTime;
            }
            set
            {
                AuditCheck("Yta_CreateTime", value);
                yta_CreateTime = value;
            }
        }
    }
}
