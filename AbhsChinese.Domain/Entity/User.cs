using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbhsChinese.Domain.Entity
{
    [Table("User")]
    public class User : EntityBase
    {
        private string name;

        private int age;

        private string phone;

        private string hobby;

        private string email;

        private string resume;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id
        {
            set; get;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                AuditCheck("Name", value);
                name = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                AuditCheck("Age", value);
                age = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                AuditCheck("Phone", value);
                phone = value;
            }
        }

        public string Hobby
        {
            get
            {
                return hobby;
            }
            set
            {
                AuditCheck("Hobby", value);
                hobby = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                AuditCheck("Email", value);
                email = value;
            }
        }

        public string Resume
        {
            get
            {
                return resume;
            }
            set
            {
                AuditCheck("Resume", value);
                resume = value;
            }
        }
    }
}