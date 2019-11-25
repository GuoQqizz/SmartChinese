using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentOrder")]
    public class Yw_StudentOrder: EntityBase
    {
        [Key]
        public int Yod_Id { get; set; }

        private string yod_OrderNo;
        public string Yod_OrderNo
        {
            set
            {
                AuditCheck("Yod_OrderNo", value);
                yod_OrderNo = value;
            }
            get
            {
                return yod_OrderNo;
            }
        }

        private int yod_OrderType;
        public int Yod_OrderType
        {
            set
            {
                AuditCheck("Yod_OrderType", value);
                yod_OrderType = value;
            }
            get
            {
                return yod_OrderType;
            }
        }

        private int yod_ReferOrderId;
        public int Yod_ReferOrderId
        {
            set
            {
                AuditCheck("Yod_ReferOrderId", value);
                yod_ReferOrderId = value;
            }
            get
            {
                return yod_ReferOrderId;
            }
        }

        private int yod_StudentId;
        public int Yod_StudentId
        {
            set
            {
                AuditCheck("Yod_StudentId", value);
                yod_StudentId = value;
            }
            get
            {
                return yod_StudentId;
            }
        }

        private int yod_CourseId;
        public int Yod_CourseId
        {
            set
            {
                AuditCheck("Yod_CourseId", value);
                yod_CourseId = value;
            }
            get
            {
                return yod_CourseId;
            }
        }

        private decimal yod_Amount;
        public decimal Yod_Amount
        {
            set
            {
                AuditCheck("Yod_Amount", value);
                yod_Amount = value;
            }
            get
            {
                return yod_Amount;
            }
        }

        private decimal yod_PayAmount;
        public decimal Yod_PayAmount
        {
            set
            {
                AuditCheck("Yod_PayAmount", value);
                yod_PayAmount = value;
            }
            get
            {
                return yod_PayAmount;
            }
        }

        private DateTime yod_OrderTime;
        public DateTime Yod_OrderTime
        {
            set
            {
                AuditCheck("Yod_OrderTime", value);
                yod_OrderTime = value;
            }
            get
            {
                return yod_OrderTime;
            }
        }

        private DateTime yod_PayTime;
        public DateTime Yod_PayTime
        {
            set
            {
                AuditCheck("Yod_PayTime", value);
                yod_PayTime = value;
            }
            get
            {
                return yod_PayTime;
            }
        }

        private int yod_Status;
        public int Yod_Status
        {
            set
            {
                AuditCheck("Yod_Status", value);
                yod_Status = value;
            }
            get
            {
                return yod_Status;
            }
        }

        private int yod_PayBatchId;
        public int Yod_PayBatchId
        {
            set
            {
                AuditCheck("Yod_PayBatchId", value);
                yod_PayBatchId = value;
            }
            get
            {
                return yod_PayBatchId;
            }
        }

        private DateTime yod_UpdateTime;
        public DateTime Yod_UpdateTime
        {
            set
            {
                AuditCheck("Yod_UpdateTime", value);
                yod_UpdateTime = value;
            }
            get
            {
                return yod_UpdateTime;
            }
        }
    }
}
