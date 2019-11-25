using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class RegisterInputModel: SmsInputModel
    {
        public string Name { get; set; }

        public int Grade { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 注册来源
        /// </summary>
        public RegisterRegSourceEnum Source { get; set; }

        /// <summary>
        /// 注册操作人（校区注册时需要）
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public StudentAccountSourceEnum PassportType { get; set; }
    }
}