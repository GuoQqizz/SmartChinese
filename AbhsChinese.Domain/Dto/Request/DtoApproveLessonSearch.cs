using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoApproveLessonSearch : DtoSearch
    {
        /// <summary>
        /// 课时编号
        /// </summary>
        public string LessonNumber { get; set; }

        /// <summary>
        /// 根据课时编号拆分的课程id
        /// </summary>
        public int CourseID
        {
            get
            {
                int i = 0;
                if (!string.IsNullOrEmpty(LessonNumber))
                {
                    int.TryParse(LessonNumber.Split('-')[0], out i);
                }
                return i;
            }
        }
        /// <summary>
        /// 根据课时编号拆分的课程序号
        /// </summary>
        public int LessonIndex
        {
            get
            {
                if (!string.IsNullOrEmpty(LessonNumber) && LessonNumber.Contains("-"))
                {
                    int i = 0;
                    int.TryParse(LessonNumber.Split('-')[1], out i);
                    return i;
                }
                return 0;
            }
        }

        /// <summary>
        /// 课时或制作人搜索
        /// </summary>
        public string LessonOrProducerName { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseType { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int Grade { get; set; }
    }
}
