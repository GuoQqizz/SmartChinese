using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.Subject
{
    [Table("Yw_Subject")]
    public class Yw_Subject : EntityBase
    {
        [Key]
        public int Ysj_Id
        {
            set; get;
        }

        private int ysj_Grade;
        public int Ysj_Grade
        {
            set
            {
                AuditCheck("Ysj_Grade", value);
                ysj_Grade = value;
            }
            get
            {
                return ysj_Grade;
            }
        }

        private int ysj_SubjectType;
        public int Ysj_SubjectType
        {
            set
            {
                AuditCheck("Ysj_SubjectType", value);
                ysj_SubjectType = value;
            }
            get
            {
                return ysj_SubjectType;
            }
        }

        private int ysj_Difficulty;
        public int Ysj_Difficulty
        {
            set
            {
                AuditCheck("Ysj_Difficulty", value);
                ysj_Difficulty = value;
            }
            get
            {
                return ysj_Difficulty;
            }
        }

        private int ysj_Status;
        public int Ysj_Status
        {
            set
            {
                AuditCheck("Ysj_Status", value);
                ysj_Status = value;
            }
            get
            {
                return ysj_Status;
            }
        }

        private string ysj_Name;
        public string Ysj_Name
        {
            set
            {
                AuditCheck("Ysj_Name", value);
                ysj_Name = value;
            }
            get
            {
                return ysj_Name;
            }
        }

        private string ysj_Keywords;
        public string Ysj_Keywords
        {
            set
            {
                AuditCheck("Ysj_Keywords", value);
                ysj_Keywords = value;
            }
            get
            {
                return ysj_Keywords;
            }
        }

        private int ysj_MainKnowledgeId;
        public int Ysj_MainKnowledgeId
        {
            set
            {
                AuditCheck("Ysj_MainKnowledgeId", value);
                ysj_MainKnowledgeId = value;
            }
            get
            {
                return ysj_MainKnowledgeId;
            }
        }

        private int ysj_GroupItemCount;
        public int Ysj_GroupItemCount
        {
            set
            {
                AuditCheck("Ysj_GroupItemCount", value);
                ysj_GroupItemCount = value;
            }
            get
            {
                return ysj_GroupItemCount;
            }
        }

        private DateTime ysj_CreateTime;
        public DateTime Ysj_CreateTime
        {
            set
            {
                AuditCheck("Ysj_CreateTime", value);
                ysj_CreateTime = value;
            }
            get
            {
                return ysj_CreateTime;
            }
        }

        private int ysj_Creator;
        public int Ysj_Creator
        {
            set
            {
                AuditCheck("Ysj_Creator", value);
                ysj_Creator = value;
            }
            get
            {
                return ysj_Creator;
            }
        }

        private DateTime ysj_UpdateTime;
        public DateTime Ysj_UpdateTime
        {
            set
            {
                AuditCheck("Ysj_UpdateTime", value);
                ysj_UpdateTime = value;
            }
            get
            {
                return ysj_UpdateTime;
            }
        }

        private int ysj_Editor;
        public int Ysj_Editor
        {
            set
            {
                AuditCheck("Ysj_Editor", value);
                ysj_Editor = value;
            }
            get
            {
                return ysj_Editor;
            }
        }
    }
}
