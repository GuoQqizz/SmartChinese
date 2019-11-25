using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class LoginInputModel
    {
        public string PassportKey { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string SmsCode { get; set; }
    }
}