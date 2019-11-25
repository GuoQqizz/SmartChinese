using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_ResourceIndex")]
    public class Yw_ResourceIndex : EntityBase
    {
        [Key]
        public int Yrx_Id { get; set; }

        private string yrx_keyword;
        public string Yrx_Keyword
        {
            set
            {
                AuditCheck("Yrx_Keyword", value);
                yrx_keyword = value;
            }
            get
            {
                return yrx_keyword;
            }
        }

        private int yrx_grade;
        public int Yrx_Grade
        {
            set
            {
                AuditCheck("Yrx_Grade", value);
                yrx_grade = value;
            }
            get
            {
                return yrx_grade;
            }
        }

        private int yrx_resourcetype;
        public int Yrx_ResourceType
        {
            set
            {
                AuditCheck("Yrx_ResourceType", value);
                yrx_resourcetype = value;
            }
            get
            {
                return yrx_resourcetype;
            }
        }

        private int yrx_resourceid;
        public int Yrx_ResourceId
        {
            set
            {
                AuditCheck("Yrx_ResourceId", value);
                yrx_resourceid = value;
            }
            get
            {
                return yrx_resourceid;
            }
        }

        private int yrx_resourcecategory;
        public int Yrx_ResourceCategory
        {
            set
            {
                AuditCheck("Yrx_ResourceCategory", value);
                yrx_resourcecategory = value;
            }
            get
            {
                return yrx_resourcecategory;
            }
        }

        private DateTime yrx_createtime;
        public DateTime Yrx_CreateTime
        {
            set
            {
                AuditCheck("Yrx_CreateTime", value);
                yrx_createtime = value;
            }
            get
            {
                return yrx_createtime;
            }
        }

        private int yrx_creator;
        public int Yrx_Creator
        {
            set
            {
                AuditCheck("Yrx_Creator", value);
                yrx_creator = value;
            }
            get
            {
                return yrx_creator;
            }
        }
    }
}
