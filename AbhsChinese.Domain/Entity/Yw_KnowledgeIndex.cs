using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_KnowledgeIndex")]
    public class Yw_KnowledgeIndex : EntityBase
    {
        [Key]
        public int Yki_Id { get; set; }

        private string yki_keyword;
        public string Yki_Keyword
        {
            set
            {
                AuditCheck("Yki_Keyword", value);
                yki_keyword = value;
            }
            get
            {
                return yki_keyword;
            }
        }

        private int yki_level;
        public int Yki_Level
        {
            set
            {
                AuditCheck("Yki_Level", value);
                yki_level = value;
            }
            get
            {
                return yki_level;
            }
        }

        private int yki_knowledgeid;
        public int Yki_KnowledgeId
        {
            set
            {
                AuditCheck("Yki_KnowledgeId", value);
                yki_knowledgeid = value;
            }
            get
            {
                return yki_knowledgeid;
            }
        }
        
        private DateTime yki_createtime;
        public DateTime Yki_CreateTime
        {
            set
            {
                AuditCheck("Yki_CreateTime", value);
                yki_createtime = value;
            }
            get
            {
                return yki_createtime;
            }
        }

        private int yki_creator;
        public int Yki_Creator
        {
            set
            {
                AuditCheck("Yki_Creator", value);
                yki_creator = value;
            }
            get
            {
                return yki_creator;
            }
        }
    }
}
