using System;

namespace AbhsChinese.Domain.Entity.Subject
{
    [Table("Yw_SubjectContent")]
    public class Yw_SubjectContent : EntityBase
    {
        private string ysc_Answer;

        private string ysc_Content;

        private DateTime ysc_CreateTime;

        private int ysc_Creator;

        private int ysc_Editor;

        private string ysc_Explain;

        private int ysc_SubjectType;

        private DateTime ysc_UpdateTime;

        public string Ysc_Answer
        {
            set
            {
                AuditCheck("Ysc_Answer", value);
                ysc_Answer = value;
            }
            get
            {
                return ysc_Answer;
            }
        }

        public string Ysc_Content
        {
            set
            {
                AuditCheck("Ysc_Content", value);
                ysc_Content = value;
            }
            get
            {
                return ysc_Content;
            }
        }

        public DateTime Ysc_CreateTime
        {
            set
            {
                AuditCheck("Ysc_CreateTime", value);
                ysc_CreateTime = value;
            }
            get
            {
                return ysc_CreateTime;
            }
        }

        public int Ysc_Creator
        {
            set
            {
                AuditCheck("Ysc_Creator", value);
                ysc_Creator = value;
            }
            get
            {
                return ysc_Creator;
            }
        }

        public int Ysc_Editor
        {
            set
            {
                AuditCheck("Ysc_Editor", value);
                ysc_Editor = value;
            }
            get
            {
                return ysc_Editor;
            }
        }

        public string Ysc_Explain
        {
            set
            {
                AuditCheck("Ysc_Explain", value);
                ysc_Explain = value;
            }
            get
            {
                return ysc_Explain;
            }
        }

        [ExplicitKey]
        public int Ysc_SubjectId
        {
            set; get;
        }

        public int Ysc_SubjectType
        {
            set
            {
                AuditCheck("Ysc_SubjectType", value);
                ysc_SubjectType = value;
            }
            get
            {
                return ysc_SubjectType;
            }
        }

        public DateTime Ysc_UpdateTime
        {
            set
            {
                AuditCheck("Ysc_UpdateTime", value);
                ysc_UpdateTime = value;
            }
            get
            {
                return ysc_UpdateTime;
            }
        }
    }
}