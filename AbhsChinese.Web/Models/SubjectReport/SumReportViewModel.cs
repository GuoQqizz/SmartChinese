using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class SumReportViewModel
    {
        /// <summary>
        /// 报告Id
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 报告类型（int）
        /// </summary>
        public int ReportType { get; set; }
        /// <summary>
        /// 报告类型（string）
        /// </summary>
        public string ReportTypeStr { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 课程标题
        /// </summary>
        public string CourseTitle
        {
            get
            {
                if (ReportType == (int)CourseReportEnum.学习报告 || ReportType == (int)CourseReportEnum.任务报告)
                {
                    return CourseName + " - " + LessonName;
                }
                else if (ReportType == (int)CourseReportEnum.练习报告)
                {
                    return CourseName + " - 第" + PraticeCount + "次练习";
                }
                return "";
            }
        }

        /// <summary>
        /// 练习报告次数
        /// </summary>
        public int PraticeCount { get; set; }
        /// <summary>
        /// 学习时间
        /// </summary>
        public string StudyDate { get; set; }

        /// <summary>
        /// 获得星数
        /// </summary>
        public int ResultStars { get; set; }

        public int TotalScore { get; set; }

        /// <summary>
        /// 获得总星数
        /// </summary>
        public int TotalStars { get; set; }

        /// <summary>
        /// 学习时长
        /// </summary>
        public int StudyTime { get; set; }

        /// <summary>
        /// 获得金币
        /// </summary>
        public int ResultCoins { get; set; }

        /// <summary>
        /// 获得总金币数
        /// </summary>
        public int TotalCoins { get; set; }

        /// <summary>
        /// 练习题数量
        /// </summary>
        public int SubjectCount { get; set; }

        /// <summary>
        /// 优秀率（五星率）
        /// </summary>
        public double ExcellentRates { get; set; }

        public List<TopKnowledge> Knowledge { get; set; }

        public EvaluateEnum Evaluate { get; set; }

        public int ImporveCount { get; set; }

        public string ImporveKnow { get; set; }

        public string GoodKnow { get; set; }

        public string StudyAdvice { get; set; }

        //来源
        public int Source { get; set; }

        //路径
        public string Path { get; set; }

        public int Origin { get; set; }
    }

    public class TopKnowledge
    {
        public int TopId { get; set; }

        public string TopName { get; set; }

        public string Path { get; set; }

        public List<ChildKnowledge> ChildKnowledge { get; set; }
    }

    public class ChildKnowledge
    {
        public int ChildId { get; set; }

        public string ChildName { get; set; }
        /// <summary>
        /// 知识点得分
        /// </summary>
        public double ResultScore { get; set; }

        /// <summary>
        /// 知识点称号描述
        /// </summary>
        public int Desc { get; set; }
    }
}