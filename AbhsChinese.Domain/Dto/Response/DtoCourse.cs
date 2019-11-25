using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    /// <summary>
    /// 课程完整信息实体类
    /// </summary>
    public class DtoCourse
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
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
        /// 年级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 年级字符串
        /// </summary>
        public string Grades
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
        /// 图片
        /// </summary>
        public string CoverImage { get; set; }
        /// <summary>
        /// 课时数量
        /// </summary>
        public int LessonCount { get; set; }
        /// <summary>
        /// 负责人id
        /// </summary>
        public int Owner { get; set; }
        /// <summary>
        /// 负责人名称
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 教研老师id
        /// </summary>
        public string Employees { get; set; }
        /// <summary>
        /// 教研老师名称
        /// </summary>
        public string EmployeesName { get; set; }
        /// <summary>
        /// 资源组id
        /// </summary>
        public int ResourceGroupId { get; set; }
        /// <summary>
        /// 资源组名称
        /// </summary>
        public string ResourceGroupName { get; set; }
        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 课程购买数
        /// </summary>
        public int SellCount { get; set; }
        /// <summary>
        /// 课程状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 课程状态字符串
        /// </summary>
        public string StatusStr
        {
            get { return ((CourseStatusEnum)Status).ToString(); }
        }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 修改人id
        /// </summary>
        public int Editor { get; set; } 
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string EditorName { get; set; }
    }
}
