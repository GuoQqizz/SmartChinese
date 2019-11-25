using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class SumStudentViewModel
    {
        public string Avatar { get; set; }

        public string Name { get; set; }

        public string School { get; set; }

        public int StudyTime { get; set; }

        public int StudyCount { get; set; }

        public int Coins { get; set; }
    }
}