using System;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_SubjectIndex")]
    public class Yw_SubjectIndex : EntityBase
    {
        private DateTime ysi_CreateTime;

        private int ysi_Creator;

        private int ysi_Difficulty;

        private int ysi_Grade;

        private string ysi_Keyword;

        private int ysi_SubjectType;

        private int ysi_SubjectId;
        private int ysi_Status;
        public int Ysi_Status
        {
            get { return ysi_Status; }
            set { AuditCheck(nameof(Ysi_Status), value); ysi_Status = value; }
        }
        public DateTime Ysi_CreateTime
        {
            get { return ysi_CreateTime; }
            set { AuditCheck("Ysi_CreateTime", value); ysi_CreateTime = value; }
        }

        public int Ysi_Creator
        {
            get { return ysi_Creator; }
            set { AuditCheck("Ysi_Creator", value); ysi_Creator = value; }
        }

        public int Ysi_Difficulty
        {
            get { return ysi_Difficulty; }
            set { AuditCheck("Ysi_Difficulty", value); ysi_Difficulty = value; }
        }

        [Key]
        public int Ysi_Id
        {
            set; get;
        }

        public string Ysi_Keyword
        {
            get { return ysi_Keyword; }
            set { AuditCheck("Ysi_Keyword", value); ysi_Keyword = value; }
        }

        public int Ysi_SubjectType
        {
            get { return ysi_SubjectType; }
            set { AuditCheck("Ysi_SubjectType", value); ysi_SubjectType = value; }
        }

        public int Ysi_Grade
        {
            get { return ysi_Grade; }
            set { AuditCheck("Ysi_Grade", value); ysi_Grade = value; }
        }

        public int Ysi_SubjectId
        {
            get { return ysi_SubjectId; }
            set { AuditCheck("Ysi_SubjectId", value); ysi_SubjectId = value; }
        }
    }
}