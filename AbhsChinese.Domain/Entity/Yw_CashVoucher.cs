using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_CashVoucher")]
    public class Yw_CashVoucher : EntityBase
    {
        [Key]
        public int Ycv_Id { get; set; }

        private string ycv_name;
        public string Ycv_Name
        {
            set
            {
                AuditCheck("Ycv_Name", value);
                ycv_name = value;
            }
            get
            {
                return ycv_name;
            }
        }

        private int ycv_vouchertype;
        public int Ycv_VoucherType
        {
            set
            {
                AuditCheck("Ycv_VoucherType", value);
                ycv_vouchertype = value;
            }
            get
            {
                return ycv_vouchertype;
            }
        }

        private int ycv_schoolid;
        public int Ycv_SchoolId
        {
            set
            {
                AuditCheck("Ycv_SchoolId", value);
                ycv_schoolid = value;
            }
            get
            {
                return ycv_schoolid;
            }
        }

        private int ycv_publishcount;
        public int Ycv_PublishCount
        {
            set
            {
                AuditCheck("Ycv_PublishCount", value);
                ycv_publishcount = value;
            }
            get
            {
                return ycv_publishcount;
            }
        }

        private decimal ycv_amount;
        public decimal Ycv_Amount
        {
            set
            {
                AuditCheck("Ycv_Amount", value);
                ycv_amount = value;
            }
            get
            {
                return ycv_amount;
            }
        }

        private int ycv_limitbyperson;
        public int Ycv_LimitByPerson
        {
            set
            {
                AuditCheck("Ycv_LimitByPerson", value);
                ycv_limitbyperson = value;
            }
            get
            {
                return ycv_limitbyperson;
            }
        }

        private decimal ycv_orderamountlimit;
        public decimal Ycv_OrderAmountLimit
        {
            set
            {
                AuditCheck("Ycv_OrderAmountLimit", value);
                ycv_orderamountlimit = value;
            }
            get
            {
                return ycv_orderamountlimit;
            }
        }

        private int ycv_expiretype;
        public int Ycv_ExpireType
        {
            set
            {
                AuditCheck("Ycv_ExpireType", value);
                ycv_expiretype = value;
            }
            get
            {
                return ycv_expiretype;
            }
        }

        private DateTime ycv_expiredate;
        public DateTime Ycv_ExpireDate
        {
            set
            {
                AuditCheck("Ycv_ExpireDate", value);
                ycv_expiredate = value;
            }
            get
            {
                return ycv_expiredate;
            }
        }

        private int ycv_expiredaycount;
        public int Ycv_ExpireDayCount
        {
            set
            {
                AuditCheck("Ycv_ExpireDayCount", value);
                ycv_expiredaycount = value;
            }
            get
            {
                return ycv_expiredaycount;
            }
        }

        private int ycv_applyscopetype;
        public int Ycv_ApplyScopeType
        {
            set
            {
                AuditCheck("Ycv_ApplyScopeType", value);
                ycv_applyscopetype = value;
            }
            get
            {
                return ycv_applyscopetype;
            }
        }

        private int ycv_applygrade;
        public int Ycv_ApplyGrade
        {
            set
            {
                AuditCheck("Ycv_ApplyGrade", value);
                ycv_applygrade = value;
            }
            get
            {
                return ycv_applygrade;
            }
        }

        private int ycv_coursetype;
        public int Ycv_CourseType
        {
            set
            {
                AuditCheck("Ycv_CourseType", value);
                ycv_coursetype = value;
            }
            get
            {
                return ycv_coursetype;
            }
        }

        private int ycv_courseid;
        public int Ycv_CourseId
        {
            set
            {
                AuditCheck("Ycv_CourseId", value);
                ycv_courseid = value;
            }
            get
            {
                return ycv_courseid;
            }
        }

        private int ycv_relatedcourseid;
        public int Ycv_RelatedCourseId
        {
            set
            {
                AuditCheck("Ycv_RelatedCourseId", value);
                ycv_relatedcourseid = value;
            }
            get
            {
                return ycv_relatedcourseid;
            }
        }

        private int ycv_usewithvouchertype;
        public int Ycv_UseWithVoucherType
        {
            set
            {
                AuditCheck("Ycv_UseWithVoucherType", value);
                ycv_usewithvouchertype = value;
            }
            get
            {
                return ycv_usewithvouchertype;
            }
        }

        private string ycv_remark;
        public string Ycv_Remark
        {
            set
            {
                AuditCheck("Ycv_Remark", value);
                ycv_remark = value;
            }
            get
            {
                return ycv_remark;
            }
        }

        private int ycv_takencount;
        public int Ycv_TakenCount
        {
            set
            {
                AuditCheck("Ycv_TakenCount", value);
                ycv_takencount = value;
            }
            get
            {
                return ycv_takencount;
            }
        }

        private string ycv_coursekey;
        public string Ycv_CourseKey
        {
            set
            {
                AuditCheck("Ycv_CourseKey", value);
                ycv_coursekey = value;
            }
            get
            {
                return ycv_coursekey;
            }
        }

        private int ycv_status;
        public int Ycv_Status
        {
            set
            {
                AuditCheck("Ycv_Status", value);
                ycv_status = value;
            }
            get
            {
                return ycv_status;
            }
        }

        private DateTime ycv_createtime;
        public DateTime Ycv_CreateTime
        {
            set
            {
                AuditCheck("Ycv_CreateTime", value);
                ycv_createtime = value;
            }
            get
            {
                return ycv_createtime;
            }
        }

        private int ycv_creator;
        public int Ycv_Creator
        {
            set
            {
                AuditCheck("Ycv_Creator", value);
                ycv_creator = value;
            }
            get
            {
                return ycv_creator;
            }
        }

        private DateTime ycv_updatetime;
        public DateTime Ycv_UpdateTime
        {
            set
            {
                AuditCheck("Ycv_UpdateTime", value);
                ycv_updatetime = value;
            }
            get
            {
                return ycv_updatetime;
            }
        }

        private int ycv_editor;
        public int Ycv_Editor
        {
            set
            {
                AuditCheck("Ycv_Editor", value);
                ycv_editor = value;
            }
            get
            {
                return ycv_editor;
            }
        }
    }
}
