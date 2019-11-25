using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class LoginInfo
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 校区
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 当前登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 当前登录地区
        /// </summary>
        public string LastLoginArea { get; set; }
        /// <summary>
        /// 当前登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}