using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_EmployeeRole")]
    public class Bas_EmployeeRole : EntityBase
    {
        [Key]
        public int ber_Id { get; set; }

        private int ber_employeeid { get; set; }
        public int Ber_EmployeeId
        {
            set
            {
                AuditCheck("Ber_EmployeeId", value);
                ber_employeeid = value;
            }
            get
            {
                return ber_employeeid;
            }
        }

        private int ber_roleid { get; set; }
        public int Ber_RoleId
        {
            set
            {
                AuditCheck("Ber_RoleId", value);
                ber_roleid = value;
            }
            get
            {
                return ber_roleid;
            }
        }

        private DateTime ber_createtime;
        public DateTime Ber_CreateTime
        {
            set
            {
                AuditCheck("Ber_CreateTime", value);
                ber_createtime = value;
            }
            get
            {
                return ber_createtime;
            }
        }

        private int ber_creator;
        public int Ber_Creator
        {
            set
            {
                AuditCheck("Ber_Creator", value);
                ber_creator = value;
            }
            get
            {
                return ber_creator;
            }
        }
    }
}
