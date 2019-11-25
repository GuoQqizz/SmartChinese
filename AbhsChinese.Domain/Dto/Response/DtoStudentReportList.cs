using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoStudentReportList
    {
        /// <summary>
        /// 报告Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生Id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 报告类型
        /// </summary>
        public int ReportType { get; set; }

        /// <summary>
        /// 课时序号
        /// </summary>
        public int LessonIndex { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime FinishStudyTime { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 课时名称
        /// </summary>
        public string Ycl_Name { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Ycs_Name { get; set; }

        /// <summary>
        /// 报告类型
        /// </summary>
        public string ReportTypeStr => CustomEnumHelper.Parse(typeof(CourseReportEnum), ReportType);

        /// <summary>
        /// 学习内容
        /// </summary>
        public string StudyContent
        {
            get
            {
                if (ReportType == (int)CourseReportEnum.学习报告)
                {
                    return Ycs_Name + " - " + Ycl_Name;
                }
                else if (ReportType == (int)CourseReportEnum.任务报告)
                {
                    return Ycs_Name + " - " + Ycl_Name + "课后任务";
                }
                else if (ReportType == (int)CourseReportEnum.练习报告)
                {
                    return Ycs_Name + " - " + "课后练习";
                }
                return "";
            }
        }

        /// <summary>
        /// 学习时间
        /// </summary>
        public string StudyTime => FinishStudyTime.ToString("yyyy-MM-dd HH:mm:ss");


        /// <summary>
        /// 练习次数（与列表无关）
        /// </summary>

        public int PraticeCount { get; set; }
    }
}
