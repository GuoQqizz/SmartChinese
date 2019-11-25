using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Product")]
    public class Product : EntityBase
    {
        [Key]
        public int p_Id
        {
            set; get;
        }

        private string name;
        
        public string p_Name
        {
            set
            {
                AuditCheck("p_Name", value);
                name = value;
            }
            get
            {
                return name;
            }
        }

        private string desc;
        public string p_Desc
        {
            set
            {
                AuditCheck("p_Desc", value);
                desc = value;
            }
            get
            {
                return desc;
            }
        }

        private int catId;
        public int p_CatId
        {
            set
            {
                AuditCheck("p_CatId", value);
                catId = value;
            }
            get
            {
                return catId;
            }
        }

        private bool isTrue;
        public bool p_IsTrue
        {
            set
            {
                AuditCheck("p_IsTrue", value);
                isTrue = value;
            }
            get
            {
                return isTrue;
            }
        }

        private DateTime pTime;
        public DateTime p_time
        {
            set
            {
                AuditCheck("p_time", value);
                pTime = value;
            }
            get
            {
                return pTime;
            }
        }

        private decimal pMoney;
        public decimal p_money
        {
            set
            {
                AuditCheck("p_money", value);
                pMoney = value;
            }
            get
            {
                return pMoney;
            }
        }

        //public string TestName
        //{
        //    set;get;
        //}

        //[ForeignKey("CatId")]
        //public virtual ProductCategory ProductCategory
        //{
        //    set; get;
        //}
    }

    [Table("ProductCategory")]
    public class ProductCategory : EntityBase
    {
        [Key]
        public int pc_Id
        {
            set; get;
        }

        public string pc_Name
        {
            set; get;
        }
    }
}
