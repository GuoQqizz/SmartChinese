using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoSumStudent
    {
        public string Name { get; set; }

        public string Avatar { get; set; }

        public int Sex { get; set; }

        public string School { get; set; }

        public int StudyMinutes { get; set; }

        public int StudyDayCount { get; set; }

        public int Coins { get; set; }

        /// <summary>
        /// 是否显示可领优惠券
        /// </summary>
        public bool ShowTicket { get; set; } = false;
        /// <summary>
        /// 称号
        /// </summary>
        public string Salutation { get; set; }
        public string AvatarStr => Avatar.ToOssPath();
    }
}
