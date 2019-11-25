using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoSumStudentTip
    {
        public int StudentId { get; set; }
        public string Phone { get; set; }
        public DateTime FirstOrderTime { get; set; }

        public string LastLoginIp { get; set; }

        public string LastLoginArea { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int OwnCourseCount { get; set; }

        public int FinishCourseCount { get; set; }

        public int ExperiencePoints { get; set; }

        public int Coins { get; set; }

        public int StudyMinutes { get; set; }

        public int StudyDayCount { get; set; }

        public string Name { get; set; }

        public string School { get; set; }

        public string Bst_Avatar { get; set; }

        public int Sex { get; set; }

        public string Avatar => string.IsNullOrEmpty(Bst_Avatar) ? "" : ConfigurationManager.AppSettings["OssHostUrl"] + Bst_Avatar;

        public string PrevLoginTime { get; set; }

        public string CurrentLoginTime => LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
