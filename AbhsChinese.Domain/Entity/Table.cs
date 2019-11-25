using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Table")]
    public class Table:EntityBase
    {
        [Key]
        public int Id
        {
            set;get;
        }

        private string name;
        public string Name
        {
            set
            {
                AuditCheck("Name", value);
                name = value;
            }
            get
            {
                return name;
            }
        }

        private int age;
        public int Age
        {
            set
            {
                AuditCheck("Age", value);
                age = value;
            }
            get
            {
                return age;
            }
        }

        private string sex;
        public string Sex
        {
            set
            {
                AuditCheck("Sex", value);
                sex = value;
            }
            get
            {
                return sex;
            }
        }

        private string phone;
        public string Phone
        {
            set
            {
                AuditCheck("Phone", value);
                phone = value;
            }
            get
            {
                return phone;
            }
        }
    }
}
