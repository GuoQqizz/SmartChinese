using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_Knowledge")]
    public class Yw_Knowledge : EntityBase
    {
        [Key]
        public int Ykl_Id { get; set; }

        private string ykl_name;
        public string Ykl_Name
        {
            set
            {
                AuditCheck("Ykl_Name", value);
                ykl_name = value;
            }
            get
            {
                return ykl_name;
            }
        }

        private int ykl_parentid;
        public int Ykl_ParentId
        {
            set
            {
                AuditCheck("Ykl_ParentId", value);
                ykl_parentid = value;
            }
            get
            {
                return ykl_parentid;
            }
        }

        private bool ykl_isleaf;
        public bool Ykl_IsLeaf
        {
            set
            {
                AuditCheck("Ykl_IsLeaf", value);
                ykl_isleaf = value;
            }
            get
            {
                return ykl_isleaf;
            }
        }

        private string ykl_path;
        public string Ykl_Path
        {
            set
            {
                AuditCheck("Ykl_Path", value);
                ykl_path = value;
            }
            get
            {
                return ykl_path;
            }
        }

        private int ykl_level;
        public int Ykl_Level
        {
            set
            {
                AuditCheck("Ykl_Level", value);
                ykl_level = value;
            }
            get
            {
                return ykl_level;
            }
        }

        private string ykl_keywords;
        public string Ykl_Keywords
        {
            set
            {
                AuditCheck("Ykl_Keywords", value);
                ykl_keywords = value;
            }
            get
            {
                return ykl_keywords;
            }
        }

        private int ykl_resourceid;
        public int Ykl_ResourceId
        {
            set
            {
                AuditCheck("Ykl_ResourceId", value);
                ykl_resourceid = value;
            }
            get
            {
                return ykl_resourceid;
            }
        }

        private DateTime ykl_createtime;
        public DateTime Ykl_CreateTime
        {
            set
            {
                AuditCheck("Ykl_CreateTime", value);
                ykl_createtime = value;
            }
            get
            {
                return ykl_createtime;
            }
        }

        private int ykl_creator;
        public int Ykl_Creator
        {
            set
            {
                AuditCheck("Ykl_Creator", value);
                ykl_creator = value;
            }
            get
            {
                return ykl_creator;
            }
        }
        
        private DateTime ykl_updatetime;
        public DateTime Ykl_UpdateTime
        {
            set
            {
                AuditCheck("Ykl_UpdateTime", value);
                ykl_updatetime = value;
            }
            get
            {
                return ykl_updatetime;
            }
        }

        private int ykl_editor;
        public int Ykl_Editor
        {
            set
            {
                AuditCheck("Ykl_Editor", value);
                ykl_editor = value;
            }
            get
            {
                return ykl_editor;
            }
        }
    }
}
