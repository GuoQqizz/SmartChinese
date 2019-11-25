using System;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_SubjectProcess")]
    public class Yw_SubjectProcess : EntityBase
    {
        private int ysp_Action;

        private DateTime ysp_CreateTime;

        private string ysp_Mark;

        private int ysp_Operator;

        private string ysp_Remark;

        private int ysp_Status;

        private int ysp_SubjectId;

        public int Ysp_Action
        {
            get { return ysp_Action; }
            set { AuditCheck(nameof(Ysp_Action), value); ysp_Action = value; }
        }

        public DateTime Ysp_CreateTime
        {
            get { return ysp_CreateTime; }
            set { AuditCheck(nameof(Ysp_CreateTime), value); ysp_CreateTime = value; }
        }

        [Key]
        public int Ysp_Id { get; set; }

        public string Ysp_Mark
        {
            get { return ysp_Mark; }
            set { AuditCheck(nameof(Ysp_Mark), value); ysp_Mark = value; }
        }

        public int Ysp_Operator
        {
            get { return ysp_Operator; }
            set { AuditCheck(nameof(Ysp_Operator), value); ysp_Operator = value; }
        }

        public string Ysp_Remark
        {
            get { return ysp_Remark; }
            set { AuditCheck(nameof(Ysp_Remark), value); ysp_Remark = value; }
        }

        public int Ysp_Status
        {
            get { return ysp_Status; }
            set { AuditCheck(nameof(Ysp_Status), value); ysp_Status = value; }
        }

        public int Ysp_SubjectId
        {
            get { return ysp_SubjectId; }
            set { AuditCheck(nameof(Ysp_SubjectId), value); ysp_SubjectId = value; }
        }
    }
}