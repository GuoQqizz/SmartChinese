using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Bas_StudentPassport")]
    public partial class Bas_StudentPassport : EntityBase
    {
        [Key]
        public int Bsp_Id { get; set; }
        private int bsp_StudentId;
        /// <summary>
        /// 
        /// </summary>
        public int Bsp_StudentId
        {
            get
            {
                return bsp_StudentId;
            }
            set
            {
                AuditCheck("Bsp_StudentId", value);
                bsp_StudentId = value;
            }
        }

        private int bsp_PassportType;
        /// <summary>
        /// 
        /// </summary>
        public int Bsp_PassportType
        {
            get
            {
                return bsp_PassportType;
            }
            set
            {
                AuditCheck("Bsp_PassportType", value);
                bsp_PassportType = value;
            }
        }

        private string bsp_PassportKey;
        /// <summary>
        /// 
        /// </summary>
        public string Bsp_PassportKey
        {
            get
            {
                return bsp_PassportKey;
            }
            set
            {
                AuditCheck("Bsp_PassportKey", value);
                bsp_PassportKey = value;
            }
        }

        private string bsp_Password;
        /// <summary>
        /// 
        /// </summary>
        public string Bsp_Password
        {
            get
            {
                return bsp_Password;
            }
            set
            {
                AuditCheck("Bsp_Password", value);
                bsp_Password = value;
            }
        }

        private int bsp_Status;
        /// <summary>
        /// 
        /// </summary>
        public int Bsp_Status
        {
            get
            {
                return bsp_Status;
            }
            set
            {
                AuditCheck("Bsp_Status", value);
                bsp_Status = value;
            }
        }

        private DateTime bsp_CreateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bsp_CreateTime
        {
            get
            {
                return bsp_CreateTime;
            }
            set
            {
                AuditCheck("Bsp_CreateTime", value);
                bsp_CreateTime = value;
            }
        }

        private DateTime bsp_UpdateTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Bsp_UpdateTime
        {
            get
            {
                return bsp_UpdateTime;
            }
            set
            {
                AuditCheck("Bsp_UpdateTime", value);
                bsp_UpdateTime = value;
            }
        }

    }
}

