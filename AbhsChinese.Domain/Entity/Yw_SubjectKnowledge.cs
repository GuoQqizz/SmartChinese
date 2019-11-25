using System;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_SubjectKnowledge")]
    public class Yw_SubjectKnowledge : EntityBase
    {
        private DateTime ysw_CreateTime;
        private int ysw_Creator;
        private int ysw_Editor;
        private int ysw_SubjectId;

        private bool ysw_IsMain;
        private int ysw_KnowledgeId;

        private DateTime ysw_UpdateTime;

        public DateTime Ysw_CreateTime
        {
            get { return ysw_CreateTime; }
            set { AuditCheck(nameof(Ysw_CreateTime), value); ysw_CreateTime = value; }
        }

        public int Ysw_Creator
        {
            get { return ysw_Creator; }
            set { AuditCheck(nameof(Ysw_Creator), value); ysw_Creator = value; }
        }

        public int Ysw_Editor
        {
            get { return ysw_Editor; }
            set { AuditCheck(nameof(Ysw_Editor), value); ysw_Editor = value; }
        }

        public int Ysw_SubjectId
        {
            get { return ysw_SubjectId; }
            set { AuditCheck(nameof(Ysw_SubjectId), value); ysw_SubjectId = value; }
        }

        [Key]
        public int Ysw_Id { get; set; }

        public bool Ysw_IsMain
        {
            get { return ysw_IsMain; }
            set { AuditCheck(nameof(Ysw_IsMain), value); ysw_IsMain = value; }
        }

        public int Ysw_KnowledgeId
        {
            get { return ysw_KnowledgeId; }
            set { AuditCheck(nameof(Ysw_KnowledgeId), value); ysw_KnowledgeId = value; }
        }

        public DateTime Ysw_UpdateTime
        {
            get { return ysw_UpdateTime; }
            set { AuditCheck(nameof(Ysw_UpdateTime), value); ysw_UpdateTime = value; }
        }
    }
}