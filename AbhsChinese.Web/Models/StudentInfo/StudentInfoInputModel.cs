using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StudentInfoInputModel
    {
        public int StudentId { get; set; }

        public string HeadPhoto { get; set; }

        public string NickName { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public DateTime Birthday { get; set; }

        public int Grade { get; set; }

        public string StudySchool { get; set; }

        public int Bst_Province { get; set; }

        public int Bst_City { get; set; }

        public int Bst_County { get; set; }

        public string Address { get; set; }
    }
}