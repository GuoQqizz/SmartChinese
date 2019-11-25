using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_Employee")]
    public class Bas_Employee : EntityBase
    {
        [Key]
        public int Bem_Id { get; set; }

        private string bem_account;
        public string Bem_Account
        {
            set
            {
                AuditCheck("Bem_Account", value);
                bem_account = value;
            }
            get
            {
                return bem_account;
            }
        }

        private string bem_name;
        public string Bem_Name
        {
            set
            {
                AuditCheck("Bem_Name", value);
                bem_name = value;
            }
            get
            {
                return bem_name;
            }
        }

        private string bem_phone;
        public string Bem_Phone
        {
            set
            {
                AuditCheck("Bem_Phone", value);
                bem_phone = value;
            }
            get
            {
                return bem_phone;
            }
        }

        private string bem_password;
        public string Bem_Password
        {
            set
            {
                AuditCheck("Bem_Password", value);
                bem_password = value;
            }
            get
            {
                return bem_password;
            }
        }
        //1：有效,2:无效,3:删除
        private int bem_status;
        public int Bem_Status
        {
            set
            {
                AuditCheck("Bem_Status", value);
                bem_status = value;
            }
            get
            {
                return bem_status;
            }
        }

        private int bem_sex;
        public int Bem_Sex
        {
            set
            {
                AuditCheck("Bem_Sex", value);
                bem_sex = value;
            }
            get
            {
                return bem_sex;
            }
        }

        private string bem_email;
        public string Bem_Email
        {
            set
            {
                AuditCheck("Bem_Email", value);
                bem_email = value;
            }
            get
            {
                return bem_email;
            }
        }

        private DateTime bem_lastlogintime = DateTime.Now;
        public DateTime Bem_LastLoginTime
        {
            set
            {
                AuditCheck("Bem_LastLoginTime", value);
                bem_lastlogintime = value;
            }
            get
            {
                return bem_lastlogintime;
            }
        }

        private string bem_lastloginip;
        public string Bem_LastLoginIp
        {
            set
            {
                AuditCheck("Bem_LastLoginIp", value);
                bem_lastloginip = value;
            }
            get
            {
                return bem_lastloginip;
            }
        }

        private string bem_lastloginarea;
        public string Bem_LastLoginArea
        {
            set
            {
                AuditCheck("Bem_LastLoginArea", value);
                bem_lastloginarea = value;
            }
            get
            {
                return bem_lastloginarea;
            }
        }

        private string bem_roles;
        public string Bem_Roles
        {
            set
            {
                AuditCheck("Bem_Roles", value);
                bem_roles = value;
            }
            get
            {
                return bem_roles;
            }
        }

        private string bem_remark;
        public string Bem_Remark
        {
            set
            {
                AuditCheck("Bem_Remark", value);
                bem_remark = value;
            }
            get
            {
                return bem_remark;
            }
        }

        private int bem_grades;
        public int Bem_Grades
        {
            set
            {
                AuditCheck("Bem_Grades", value);
                bem_grades = value;
            }
            get
            {
                return bem_grades;
            }
        }

        private DateTime bem_createtime =DateTime.Now;
        public DateTime Bem_CreateTime
        {
            set
            {
                AuditCheck("Bem_CreateTime", value);
                bem_createtime = value;
            }
            get
            {
                return bem_createtime;
            }
        }

        private DateTime bem_updatetime = DateTime.Now;
        public DateTime Bem_UpdateTime
        {
            set
            {
                AuditCheck("Bem_UpdateTime", value);
                bem_updatetime = value;
            }
            get
            {
                return bem_updatetime;
            }
        }

        private int bem_editor;
        public int Bem_Editor
        {
            set
            {
                AuditCheck("Bem_Editor", value);
                bem_editor = value;
            }
            get
            {
                return bem_editor;
            }
        }
    }
}
