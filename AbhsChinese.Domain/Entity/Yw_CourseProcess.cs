using System;

namespace AbhsChinese.Domain.Entity
{
    [Table(nameof(Yw_CourseProcess))]
    public class Yw_CourseProcess : EntityBase
    {
        private int ycp_Action;

        private int ycp_CourseId;

        private DateTime ycp_CreateTime;

        private int ycp_Operator;

        private string ycp_Remark;

        private int ycp_Status;

        public int Ycp_Action
        {
            get { return ycp_Action; }
            set { AuditCheck(nameof(Ycp_Action), value); ycp_Action = value; }
        }

        public int Ycp_CourseId
        {
            get { return ycp_CourseId; }
            set { AuditCheck(nameof(Ycp_CourseId), value); ycp_CourseId = value; }
        }

        public DateTime Ycp_CreateTime
        {
            get { return ycp_CreateTime; }
            set { AuditCheck(nameof(Ycp_CreateTime), value); ycp_CreateTime = value; }
        }

        [Key]
        public int Ycp_Id { get; set; }

        public int Ycp_Operator
        {
            get { return ycp_Operator; }
            set { AuditCheck(nameof(Ycp_Operator), value); ycp_Operator = value; }
        }

        public string Ycp_Remark
        {
            get { return ycp_Remark; }
            set { AuditCheck(nameof(Ycp_Remark), value); ycp_Remark = value; }
        }

        public int Ycp_Status
        {
            get { return ycp_Status; }
            set { AuditCheck(nameof(Ycp_Status), value); ycp_Status = value; }
        }
    }
}