using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_ResourceGroup")]
    public class Yw_ResourceGroup:EntityBase
    {
        [Key]
        public int Yrg_Id { get; set; }

        private string yrg_name;
        public string Yrg_Name
        {
            set
            {
                AuditCheck("Yrg_Name", value);
                yrg_name = value;
            }
            get
            {
                return yrg_name;
            }
        }

        private int yrg_grade;
        public int Yrg_Grade
        {
            set
            {
                AuditCheck("Yrg_Grade", value);
                yrg_grade = value;
            }
            get
            {
                return yrg_grade;
            }
        }

        private int yrg_mediacount;
        public int Yrg_MediaCount
        {
            set
            {
                AuditCheck("Yrg_MediaCount", value);
                yrg_mediacount = value;
            }
            get
            {
                return yrg_mediacount;
            }
        }

        private int yrg_textcount;
        public int Yrg_TextCount
        {
            set
            {
                AuditCheck("Yrg_MediaCount", value);
                yrg_textcount = value;
            }
            get
            {
                return yrg_textcount;
            }
        }

        private int yrg_subjectcount;
        public int Yrg_SubjectCount
        {
            set
            {
                AuditCheck("Yrg_SubjectCount", value);
                yrg_subjectcount = value;
            }
            get
            {
                return yrg_subjectcount;
            }
        }

        private int yrg_status;
        public int Yrg_Status
        {
            set
            {
                AuditCheck("Yrg_Status", value);
                yrg_status = value;
            }
            get
            {
                return yrg_status;
            }
        }

        private DateTime yrg_createtime;
        public DateTime Yrg_CreateTime
        {
            set
            {
                AuditCheck("Yrg_CreateTime", value);
                yrg_createtime = value;
            }
            get
            {
                return yrg_createtime;
            }
        }

        private int yrg_creator;
        public int Yrg_Creator
        {
            set
            {
                AuditCheck("Yrg_Creator", value);
                yrg_creator = value;
            }
            get
            {
                return yrg_creator;
            }
        }
    }
}
