using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_Role")]
    public class Bas_Role : EntityBase
    {
        [Key]
        public int Bro_Id { set; get; }

        private string bro_name;
        public string Bro_Name
        {
            set
            {
                AuditCheck("Bro_Name", value);
                bro_name = value;
            }
            get
            {
                return bro_name;
            }
        }

        private string bro_code;
        public string Bro_code
        {
            set
            {
                AuditCheck("Bro_code", value);
                bro_code = value;
            }
            get
            {
                return bro_code;
            }
        }

        private int bro_status;
        public int Bro_Status
        {
            set
            {
                AuditCheck("Bro_Status", value);
                bro_status = value;
            }
            get
            {
                return bro_status;
            }
        }

        private string bro_permissions;
        public string Bro_Permissions
        {
            set
            {
                AuditCheck("Bro_Permissions", value);
                bro_permissions = value;
            }
            get
            {
                return bro_permissions;
            }
        }

        private bool bro_issystem;
        public bool Bro_IsSystem
        {
            set
            {
                AuditCheck("Bro_IsSystem", value);
                bro_issystem = value;
            }
            get
            {
                return bro_issystem;
            }
        }

        private DateTime bro_createtime;
        public DateTime Bro_CreateTime
        {
            set
            {
                AuditCheck("Bro_CreateTime", value);
                bro_createtime = value;
            }
            get
            {
                return bro_createtime;
            }
        }

        private DateTime bro_updatetime;
        public DateTime Bro_UpdateTime
        {
            set
            {
                AuditCheck("Bro_UpdateTime", value);
                bro_updatetime = value;
            }
            get
            {
                return bro_updatetime;
            }
        }

        private int bro_editor;
        public int Bro_Editor
        {
            set
            {
                AuditCheck("Bro_Editor", value);
                bro_editor = value;
            }
            get
            {
                return bro_editor;
            }
        }
    }
}
