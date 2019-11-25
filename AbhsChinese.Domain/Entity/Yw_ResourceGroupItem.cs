using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_ResourceGroupItem")]
    public class Yw_ResourceGroupItem : EntityBase
    {
        [Key]
        public int Ygi_Id { get; set; }

        private int ygi_groupid;
        public int Ygi_GroupId
        {
            set
            {
                AuditCheck("Ygi_GroupId", value);
                ygi_groupid = value;
            }
            get
            {
                return ygi_groupid;
            }
        }

        private int ygi_resourcetype;
        public int Ygi_ResourceType
        {
            set
            {
                AuditCheck("Ygi_ResourceType", value);
                ygi_resourcetype = value;
            }
            get
            {
                return ygi_resourcetype;
            }
        }

        private int ygi_resourceid;
        public int Ygi_ResourceId
        {
            set
            {
                AuditCheck("Ygi_ResourceId", value);
                ygi_resourceid = value;
            }
            get
            {
                return ygi_resourceid;
            }
        }

        private DateTime ygi_createtime;
        public DateTime Ygi_CreateTime
        {
            set
            {
                AuditCheck("Ygi_CreateTime", value);
                ygi_createtime = value;
            }
            get
            {
                return ygi_createtime;
            }
        }

        private int ygi_creator;
        public int Ygi_Creator
        {
            set
            {
                AuditCheck("Ygi_Creator", value);
                ygi_creator = value;
            }
            get
            {
                return ygi_creator;
            }
        }

        private DateTime ygi_updatetime;
        public DateTime Ygi_UpdateTime
        {
            set
            {
                AuditCheck("Ygi_UpdateTime", value);
                ygi_updatetime = value;
            }
            get
            {
                return ygi_updatetime;
            }
        }
        
        private int ygi_editor;
        public int Ygi_Editor
        {
            set
            {
                AuditCheck("Ygi_Editor", value);
                ygi_editor = value;
            }
            get
            {
                return ygi_editor;
            }
        }
    }
}
