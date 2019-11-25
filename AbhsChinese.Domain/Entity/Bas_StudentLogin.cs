using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_StudentLogin")]
    public partial class Bas_StudentLogin : EntityBase
    {
        [Key]
        public int Bsg_Id { get; set; }
        private int bsg_studentid;
        /// <summary>
        /// 
        /// </summary>
        public int Bsg_StudentId
        {
            get
            {
                return bsg_studentid;
            }
            set
            {
                AuditCheck("Bsg_StudentId", value);
                bsg_studentid = value;
            }
        }
        
        private DateTime bsg_logintime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bsg_LoginTime
        {
            get
            {
                return bsg_logintime;
            }
            set
            {
                AuditCheck("Bsg_LoginTime", value);
                bsg_logintime = value;
            }
        }

        private string bsg_loginip;
        /// <summary>
        /// 
        /// </summary>
        public string Bsg_LoginIp
        {
            get
            {
                return bsg_loginip;
            }
            set
            {
                AuditCheck("Bsg_LoginIp", value);
                bsg_loginip = value;
            }
        }

        private string bsg_loginarea;
        /// <summary>
        /// 
        /// </summary>
        public string Bsg_LoginArea
        {
            get
            {
                return bsg_loginarea;
            }
            set
            {
                AuditCheck("Bsg_LoginArea", value);
                bsg_loginarea = value;
            }
        }

        private int bsg_logindevice;
        /// <summary>
        /// 
        /// </summary>
        public int Bsg_LoginDevice
        {
            get
            {
                return bsg_logindevice;
            }
            set
            {
                AuditCheck("Bsg_LoginDevice", value);
                bsg_logindevice = value;
            }
        }
    }
}

