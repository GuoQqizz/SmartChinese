using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models
{
    public class CookieUserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int Grades { get; set; }

        public List<int> GradesList { get; set; }
    }
}