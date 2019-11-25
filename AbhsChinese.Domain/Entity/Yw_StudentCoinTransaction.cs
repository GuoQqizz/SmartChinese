using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentCoinTransaction")]
    public class Yw_StudentCoinTransaction : EntityBase
    {
        [Key]
        public int Ycn_Id { get; set; }

        private int ycn_StudentId;
        public int Ycn_StudentId
        {
            set
            {
                AuditCheck("Ycn_StudentId", value);
                ycn_StudentId = value;
            }
            get
            {
                return ycn_StudentId;
            }
        }

        private int ycn_TransactionType;
        public int Ycn_TransactionType
        {
            set
            {
                AuditCheck("Ycn_TransactionType", value);
                ycn_TransactionType = value;
            }
            get
            {
                return ycn_TransactionType;
            }
        }

        private int ycn_ReferId;
        public int Ycn_ReferId
        {
            set
            {
                AuditCheck("Ycn_ReferId", value);
                ycn_ReferId = value;
            }
            get
            {
                return ycn_ReferId;
            }
        }

        private int ycn_TransactionFactor;
        public int Ycn_TransactionFactor
        {
            set
            {
                AuditCheck("Ycn_TransactionFactor", value);
                ycn_TransactionFactor = value;
            }
            get
            {
                return ycn_TransactionFactor;
            }
        }

        private int ycn_TransactionAmount;
        public int Ycn_TransactionAmount
        {
            set
            {
                AuditCheck("Ycn_TransactionAmount", value);
                ycn_TransactionAmount = value;
            }
            get
            {
                return ycn_TransactionAmount;
            }
        }

        private DateTime ycn_TransactionTime;
        public DateTime Ycn_TransactionTime
        {
            set
            {
                AuditCheck("Ycn_TransactionTime", value);
                ycn_TransactionTime = value;
            }
            get
            {
                return ycn_TransactionTime;
            }
        }

        private DateTime ycn_CreateTime;
        public DateTime Ycn_CreateTime
        {
            set
            {
                AuditCheck("Ycn_CreateTime", value);
                ycn_CreateTime = value;
            }
            get
            {
                return ycn_CreateTime;
            }
        }
    }
}
