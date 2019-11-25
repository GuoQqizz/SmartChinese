using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 订单交易表对应数据模型
    /// </summary>
    [Table("Yw_StudentOrderPay")]
    public class Yw_StudentOrderPay : EntityBase
    {
        private System.Int32 yop_studentid;
        private System.Int32 yop_courseid;
        private System.Int32 yop_orderid;
        private System.Int32 yop_batchid;
        private System.Int32 yop_paytype;
        private System.String yop_payouterid;
        private System.Decimal yop_amount;
        private System.DateTime yop_paytime;
        private System.Int32 yop_status;
        private System.DateTime yop_createtime;
        private System.DateTime yop_updatetime;

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Yop_Id
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Yop_StudentId
        {
            set
            {
                AuditCheck("Yop_StudentId", value);
                yop_studentid = value;
            }
            get
            {
                return yop_studentid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Yop_CourseId
        {
            set
            {
                AuditCheck("Yop_CourseId", value);
                yop_courseid = value;
            }
            get
            {
                return yop_courseid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Yop_OrderId
        {
            set
            {
                AuditCheck("Yop_OrderId", value);
                yop_orderid = value;
            }
            get
            {
                return yop_orderid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Yop_BatchId
        {
            set
            {
                AuditCheck("Yop_BatchId", value);
                yop_batchid = value;
            }
            get
            {
                return yop_batchid;
            }
        }
        /// <summary>
        /// 101微信支付201（1全场券2成单券3注册券4校区券）
        /// <see cref="AbhsChinese.Domain.Enum.StudentOrderPaymnentTypeEnum"/>
        /// </summary>
        public System.Int32 Yop_PayType
        {
            set
            {
                AuditCheck("Yop_PayType", value);
                yop_paytype = value;
            }
            get
            {
                return yop_paytype;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.String Yop_PayOuterId
        {
            set
            {
                AuditCheck("Yop_PayOuterId", value);
                yop_payouterid = value;
            }
            get
            {
                return yop_payouterid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Yop_Amount
        {
            set
            {
                AuditCheck("Yop_Amount", value);
                yop_amount = value;
            }
            get
            {
                return yop_amount;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime Yop_PayTime
        {
            set
            {
                AuditCheck("Yop_PayTime", value);
                yop_paytime = value;
            }
            get
            {
                return yop_paytime;
            }
        }
        /// <summary>
        /// 1未支付2已支付
        /// </summary>
        public System.Int32 Yop_Status
        {
            set
            {
                AuditCheck("Yop_Status", value);
                yop_status = value;
            }
            get
            {
                return yop_status;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime Yop_CreateTime
        {
            set
            {
                AuditCheck("Yop_CreateTime", value);
                yop_createtime = value;
            }
            get
            {
                return yop_createtime;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime Yop_UpdateTime
        {
            set
            {
                AuditCheck("Yop_UpdateTime", value);
                yop_updatetime = value;
            }
            get
            {
                return yop_updatetime;
            }
        }
    }
}
