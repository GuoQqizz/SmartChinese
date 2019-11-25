using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StudentLoginViewModel
    {
        public int Bsg_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Bsg_StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Bsg_LoginTime { get; set; }

        public string LoginTimeStr => Bsg_LoginTime.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 
        /// </summary>
        public string Bsg_LoginIp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bsg_LoginArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bsg_LoginDevice { get; set; }

        public string LoginDevice => CustomEnumHelper.Parse(typeof(LoginDeviceEnum), Bsg_LoginDevice);
    }
}