using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    /// <summary>
    /// 课程审批列表数据模型
    /// </summary>
    public class DtoLessonApprove
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课程年级
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 课程年级字符串
        /// </summary>
        public string GradeStr
        {
            get
            {
                List<string> strs = new List<string>();
                if ((Grade & (int)GradeEnum.一年级) != 0)
                {
                    strs.Add(GradeEnum.一年级.ToString());
                }
                if ((Grade & (int)GradeEnum.二年级) != 0)
                {
                    strs.Add(GradeEnum.二年级.ToString());
                }
                if ((Grade & (int)GradeEnum.三年级) != 0)
                {
                    strs.Add(GradeEnum.三年级.ToString());
                }
                if ((Grade & (int)GradeEnum.四年级) != 0)
                {
                    strs.Add(GradeEnum.四年级.ToString());
                }
                if ((Grade & (int)GradeEnum.五年级) != 0)
                {
                    strs.Add(GradeEnum.五年级.ToString());
                }
                if ((Grade & (int)GradeEnum.六年级) != 0)
                {
                    strs.Add(GradeEnum.六年级.ToString());
                }
                if ((Grade & (int)GradeEnum.初一) != 0)
                {
                    strs.Add(GradeEnum.初一.ToString());
                }
                if ((Grade & (int)GradeEnum.初二) != 0)
                {
                    strs.Add(GradeEnum.初二.ToString());
                }
                if ((Grade & (int)GradeEnum.初三) != 0)
                {
                    strs.Add(GradeEnum.初三.ToString());
                }
                if ((Grade & (int)GradeEnum.高一) != 0)
                {
                    strs.Add(GradeEnum.高一.ToString());
                }
                if ((Grade & (int)GradeEnum.高二) != 0)
                {
                    strs.Add(GradeEnum.高二.ToString());
                }
                if ((Grade & (int)GradeEnum.高三) != 0)
                {
                    strs.Add(GradeEnum.高三.ToString());
                }
                return string.Join(",", strs.ToArray());
            }
        }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseType { get; set; }
        /// <summary>
        /// 课程类型名称
        /// </summary>
        public string CourseTypeStr
        {
            get { return ((CourseCategoryEnum)CourseType).ToString(); }
        }
        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonID { get; set; }
        /// <summary>
        /// 课时序号
        /// </summary>
        public int LessonIndex { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
        /// <summary>
        /// 课时制作人id
        /// </summary>
        public int LessonProducer { get; set; }
        /// <summary>
        /// 课时制作人名称
        /// </summary>
        public string LessonProducerName { get; set; }
        /// <summary>
        /// 课程状态
        /// </summary>
        public int LessonStatus { get; set; }
        /// <summary>
        /// 课时状态文本
        /// </summary>
        public string LessonStatusStr
        {
            get
            {
                return ((LessonStatusEnum)LessonStatus).ToString();
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 更新时间字符串
        /// </summary>
        public string UpdateDateStr { get { return UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"); } }

    }
}
