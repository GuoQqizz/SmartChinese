using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_CoursePrice")]
    public class Yw_CoursePrice : EntityBase
    {
        [Key]

        public int Yce_Id
        {
            set;get;
        }

        private int yce_CourseId;

        public int Yce_CourseId
        {
            set
            {
                AuditCheck("Yce_CourseId", value);
                yce_CourseId = value;
            }
            get
            {
                return yce_CourseId;
            }
        }

        private int yce_SchoolLevelId;
        public int Yce_SchoolLevelId
        {
            set
            {
                AuditCheck("Yce_SchoolLevelId", value);
                yce_SchoolLevelId = value;
            }
            get
            {
                return yce_SchoolLevelId;
            }
        }

        private decimal yce_Price;
        public decimal Yce_Price
        {
            set
            {
                AuditCheck("Yce_Price", value);
                yce_Price = value;
            }
            get
            {
                return yce_Price;
            }
        }

        private DateTime yce_CreateTime;
        public DateTime Yce_CreateTime
        {
            set
            {
                AuditCheck("Yce_CreateTime", value);
                yce_CreateTime = value;
            }
            get
            {
                return yce_CreateTime;
            }
        }

        private int yce_Creator;
        public int Yce_Creator
        {
            set
            {
                AuditCheck("Yce_Creator", value);
                yce_Creator = value;
            }
            get
            {
                return yce_Creator;
            }
        }

        private DateTime yce_UpdateTime;
        public DateTime Yce_UpdateTime
        {
            set
            {
                AuditCheck("Yce_UpdateTime", value);
                yce_UpdateTime = value;
            }
            get
            {
                return yce_UpdateTime;
            }
        }

        private int yce_Editor;
        public int Yce_Editor
        {
            set
            {
                AuditCheck("Yce_Editor", value);
                yce_Editor = value;
            }
            get
            {
                return yce_Editor;
            }
        }
    }
}
