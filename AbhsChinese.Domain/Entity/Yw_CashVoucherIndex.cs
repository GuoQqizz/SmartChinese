using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_CashVoucherIndex")]
    public class Yw_CashVoucherIndex : EntityBase
    {
        [Key]
        public int Ycx_Id { get; set; }

        private int ycx_courseid;
        public int Ycx_CourseId
        {
            set
            {
                AuditCheck("Ycx_CourseId", value);
                ycx_courseid = value;
            }
            get
            {
                return ycx_courseid;
            }
        }

        private int ycx_schoolid;
        public int Ycx_SchoolId
        {
            set
            {
                AuditCheck("Ycx_SchoolId", value);
                ycx_schoolid = value;
            }
            get
            {
                return ycx_schoolid;
            }
        }

        private DateTime ycx_expiredate;
        public DateTime Ycx_ExpireDate
        {
            set
            {
                AuditCheck("Ycx_ExpireDate", value);
                ycx_expiredate = value;
            }
            get
            {
                return ycx_expiredate;
            }
        }

        private int ycx_voucherid;
        public int Ycx_VoucherId
        {
            set
            {
                AuditCheck("Ycx_VoucherId", value);
                ycx_voucherid = value;
            }
            get
            {
                return ycx_voucherid;
            }
        }

        private DateTime ycw_createtime;
        public DateTime Ycw_CreateTime
        {
            set
            {
                AuditCheck("Ycw_CreateTime", value);
                ycw_createtime = value;
            }
            get
            {
                return ycw_createtime;
            }
        }
    }
}
