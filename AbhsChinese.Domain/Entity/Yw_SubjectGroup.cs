using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_SubjectGroup")]
    public class Yw_SubjectGroup : EntityBase
    {
        [Key]
        public int Ysg_Id { get; set; }

        private int ysg_SubjectId;
        public int Ysg_SubjectId
        {
            set
            {
                AuditCheck("Ysg_SubjectId", value);
                ysg_SubjectId = value;
            }
            get
            {
                return ysg_SubjectId;
            }
        }

        private string ysg_RelSubjectId;
        public string Ysg_RelSubjectId
        {
            set
            {
                AuditCheck("Ysg_RelSubjectId", value);
                ysg_RelSubjectId = value;
            }
            get
            {
                return ysg_RelSubjectId;
            }
        }

        private DateTime ysg_CreateTime;
        public DateTime Ysg_CreateTime
        {
            set
            {
                AuditCheck("Ysg_CreateTime", value);
                ysg_CreateTime = value;
            }
            get
            {
                return ysg_CreateTime;
            }
        }

        private int ysg_Creator;
        public int Ysg_Creator
        {
            set
            {
                AuditCheck("Ysg_Creator", value);
                ysg_Creator = value;
            }
            get
            {
                return ysg_Creator;
            }
        }

        private DateTime ysg_UpdateTime;
        public DateTime Ysg_UpdateTime
        {
            set
            {
                AuditCheck("Ysg_UpdateTime", value);
                ysg_UpdateTime = value;
            }
            get
            {
                return ysg_UpdateTime;
            }
        }

        private int ysg_Editor;
        public int Ysg_Editor
        {
            set
            {
                AuditCheck("Ysg_Editor", value);
                ysg_Editor = value;
            }
            get
            {
                return ysg_Editor;
            }
        }
    }
}
