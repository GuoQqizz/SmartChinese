using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentCashVoucher")]
    public class Yw_StudentCashVoucher : EntityBase
    {
        [Key]
        public int Ysv_Id { get; set; }

        private int ysv_studentid;
        public int Ysv_StudentId
        {
            set
            {
                AuditCheck("Ysv_StudentId", value);
                ysv_studentid = value;
            }
            get
            {
                return ysv_studentid;
            }
        }

        private int ysv_cashvoucherid;
        public int Ysv_CashVoucherId
        {
            set
            {
                AuditCheck("Ysv_CashVoucherId", value);
                ysv_cashvoucherid = value;
            }
            get
            {
                return ysv_cashvoucherid;
            }
        }

        private string ysv_voucherno;
        public string Ysv_VoucherNo
        {
            set
            {
                AuditCheck("Ysv_VoucherNo", value);
                ysv_voucherno = value;
            }
            get
            {
                return ysv_voucherno;
            }
        }

        private int ysv_vouchertype;
        public int Ysv_VoucherType
        {
            set
            {
                AuditCheck("Ysv_VoucherType", value);
                ysv_vouchertype = value;
            }
            get
            {
                return ysv_vouchertype;
            }
        }

        private DateTime ysv_expiredate;
        public DateTime Ysv_ExpireDate
        {
            set
            {
                AuditCheck("Ysv_ExpireDate", value);
                ysv_expiredate = value;
            }
            get
            {
                return ysv_expiredate;
            }
        }

        private int ysv_gottype;
        public int Ysv_GotType
        {
            set
            {
                AuditCheck("Ysv_GotType", value);
                ysv_gottype = value;
            }
            get
            {
                return ysv_gottype;
            }
        }

        private int ysv_gotreferid;
        public int Ysv_GotReferId
        {
            set
            {
                AuditCheck("Ysv_GotReferId", value);
                ysv_gotreferid = value;
            }
            get
            {
                return ysv_gotreferid;
            }
        }

        private int ysv_usedtype;
        public int Ysv_UsedType
        {
            set
            {
                AuditCheck("Ysv_UsedType", value);
                ysv_usedtype = value;
            }
            get
            {
                return ysv_usedtype;
            }
        }

        private int ysv_usedreferid;
        public int Ysv_UsedReferId
        {
            set
            {
                AuditCheck("Ysv_UsedReferId", value);
                ysv_usedreferid = value;
            }
            get
            {
                return ysv_usedreferid;
            }
        }

        private string ysv_usedreferno;
        public string Ysv_UsedReferNo
        {
            set
            {
                AuditCheck("Ysv_UsedReferNo", value);
                ysv_usedreferno = value;
            }
            get
            {
                return ysv_usedreferno;
            }
        }

        private int ysv_status;
        public int Ysv_Status
        {
            set
            {
                AuditCheck("Ysv_Status", value);
                ysv_status = value;
            }
            get
            {
                return ysv_status;
            }
        }

        private DateTime ysv_takentime;
        public DateTime Ysv_TakenTime
        {
            set
            {
                AuditCheck("Ysv_TakenTime", value);
                ysv_takentime = value;
            }
            get
            {
                return ysv_takentime;
            }
        }

        private DateTime ysv_usedtime;
        public DateTime Ysv_UsedTime
        {
            set
            {
                AuditCheck("Ysv_UsedTime", value);
                ysv_usedtime = value;
            }
            get
            {
                return ysv_usedtime;
            }
        }

        private DateTime ysv_updatetime;
        public DateTime Ysv_UpdateTime
        {
            set
            {
                AuditCheck("Ysv_UpdateTime", value);
                ysv_updatetime = value;
            }
            get
            {
                return ysv_updatetime;
            }
        }
    }
}
