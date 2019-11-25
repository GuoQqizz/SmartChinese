using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.Student
{
    public class DtoStudentRegister
    {
        public string Name { get; set; }

        public int Grade { get; set; }

        public string Phone { get; set; }

        public string SmsCode { get; set; }

        public string Password { get; set; }

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

        public int SchoolId { get; set; }
    }
}
