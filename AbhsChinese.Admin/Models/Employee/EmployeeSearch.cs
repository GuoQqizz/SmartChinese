using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Employee
{
    public class EmployeeSearch : Search
    {
        public string AccountOrNameOrPhone { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}