using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_CoursePriceAudit")]
    public class Yw_CoursePriceAudit : EntityBase
    {
        [Key]
        public int Ypa_Id { get; set; }

        private int ypa_CourseId;
        public int Ypa_CourseId
        {
            set
            {
                AuditCheck("Ypa_CourseId", value);
                ypa_CourseId = value;
            }
            get
            {
                return ypa_CourseId;
            }
        }

        private int ypa_SchoolLevelId;
        public int Ypa_SchoolLevelId
        {
            set
            {
                AuditCheck("Ypa_SchoolLevelId", value);
                ypa_SchoolLevelId = value;
            }
            get
            {
                return ypa_SchoolLevelId;
            }
        }

        private decimal ypa_OldPrice;
        public decimal Ypa_OldPrice
        {
            set
            {
                AuditCheck("Ypa_OldPrice", value);
                ypa_OldPrice = value;
            }
            get
            {
                return ypa_OldPrice;
            }
        }

        private decimal ypa_NewPrice;
        public decimal Ypa_NewPrice
        {
            set
            {
                AuditCheck("Ypa_NewPrice", value);
                ypa_NewPrice = value;
            }
            get
            {
                return ypa_NewPrice;
            }
        }

        private DateTime ypa_CreateTime;
        public DateTime Ypa_CreateTime
        {
            set
            {
                AuditCheck("Ypa_CreateTime", value);
                ypa_CreateTime = value;
            }
            get
            {
                return ypa_CreateTime;
            }
        }

        private int ypa_Creator;
        public int Ypa_Creator
        {
            set
            {
                AuditCheck("Ypa_Creator", value);
                ypa_Creator = value;
            }
            get
            {
                return ypa_Creator;
            }
        }
    }
}
