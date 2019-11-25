using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    /// <summary>
    /// 学生课程数据详情
    /// </summary>
    public class DtoStudentCourseInfo
    {
        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseType { get; set; }

        /// <summary>
        /// 课程类型名称
        /// </summary>
        public string CourseTypeStr
        {
            get
            {
                return ((CourseCategoryEnum)CourseType).ToString();
            }
        }

        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 开始学习时间
        /// </summary>
        public DateTime CourseStartTime { get; set; }
        /// <summary>
        /// 课程是否学完
        /// </summary>
        public bool CourseFinshed { get; set; }
        /// <summary>
        /// 课时数量
        /// </summary>
        public int LessonsNum { get; set; }

        /// <summary>
        /// 课时完成数量
        /// </summary>
        public int LessonsFinishedNum { get; set; }

        /// <summary>
        /// 下一个需要学习的课程
        /// </summary>
        public int NextLessonNum { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 课程年级名称
        /// </summary>
        public string GradeStr
        {
            get
            {
                return ((GradeEnum)Grade).ToString();
            }
        }

        /// <summary>
        /// 学校id
        /// </summary>
        public int SchoolID { get; set; }

        /// <summary>
        /// 学校名
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 班级id
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 教师id
        /// </summary>
        public int TeacherID { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 教师头像
        /// </summary>
        public string TeacherImg { get; set; }

    }
}
