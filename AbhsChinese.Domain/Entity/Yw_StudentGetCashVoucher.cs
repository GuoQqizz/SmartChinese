using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentGetCashVoucher")]
    public class Yw_StudentGetCashVoucher : EntityBase
    {
        [Key]
        public int Ygv_Id { get; set; }

        private int ygv_studentid;
        public int Ygv_StudentId
        {
            set
            {
                AuditCheck("Ygv_StudentId", value);
                ygv_studentid = value;
            }
            get
            {
                return ygv_studentid;
            }
        }

        private int ygv_maxcashvoucherid;
        public int Ygv_MaxCashVoucherId
        {
            set
            {
                AuditCheck("Ygv_MaxCashVoucherId", value);
                ygv_maxcashvoucherid = value;
            }
            get
            {
                return ygv_maxcashvoucherid;
            }
        }
        
        private DateTime ysv_updatetime;
        public DateTime Ysv_UpdateTime
        {
            set
            {
                AuditCheck("ysv_updatetime", value);
                ysv_updatetime = value;
            }
            get
            {
                return ysv_updatetime;
            }
        }
    }
}
