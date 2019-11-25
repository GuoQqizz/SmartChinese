using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_PayLog")]
    public class Yw_PayLog : EntityBase
    {
        [Key]
        public int Ypg_Id { get; set; }

        private int ypg_OrderId;
        public int Ypg_OrderId
        {
            set
            {
                AuditCheck("Ypg_OrderId", value);
                ypg_OrderId = value;
            }
            get
            {
                return ypg_OrderId;
            }
        }

        private int ypg_OrderPayId;
        public int Ypg_OrderPayId
        {
            set
            {
                AuditCheck("Ypg_OrderPayId", value);
                ypg_OrderPayId = value;
            }
            get
            {
                return ypg_OrderPayId;
            }
        }

        private string ypg_OrderNo;
        public string Ypg_OrderNo
        {
            set
            {
                AuditCheck("Ypg_OrderNo", value);
                ypg_OrderNo = value;
            }
            get
            {
                return ypg_OrderNo;
            }
        }
        

        private int ypg_PayStep;
        public int Ypg_PayStep
        {
            set
            {
                AuditCheck("Ypg_PayStep", value);
                ypg_PayStep = value;
            }
            get
            {
                return ypg_PayStep;
            }
        }

        private string ypg_RequestData;
        public string Ypg_RequestData
        {
            set
            {
                AuditCheck("Ypg_RequestData", value);
                ypg_RequestData = value;
            }
            get
            {
                return ypg_RequestData;
            }
        }

        private string ypg_ResponseData;
        public string Ypg_ResponseData
        {
            set
            {
                AuditCheck("Ypg_ResponseData", value);
                ypg_ResponseData = value;
            }
            get
            {
                return ypg_ResponseData;
            }
        }

        private DateTime ypg_RequestTime;
        public DateTime Ypg_RequestTime
        {
            set
            {
                AuditCheck("Ypg_RequestTime", value);
                ypg_RequestTime = value;
            }
            get { return ypg_RequestTime; }
        }

        private DateTime ypg_ResponseTime;
        public DateTime Ypg_ResponseTime
        {
            set
            {
                AuditCheck("Ypg_ResponseTime", value);
                ypg_ResponseTime = value;
            }
            get { return ypg_ResponseTime; }
        }

        private int ypg_Status;
        public int Ypg_Status
        {
            set
            {
                AuditCheck("Ypg_Status", value);
                ypg_Status = value;
            }
            get
            {
                return ypg_Status;
            }
        }
    }
}
