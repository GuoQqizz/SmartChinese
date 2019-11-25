using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public int[] Grades { get; set; }
        public int TotalGrades { get; set; }

        public   List<DtoKeyValue<string, string>> RuleSource { get; set; }
    
    }
}