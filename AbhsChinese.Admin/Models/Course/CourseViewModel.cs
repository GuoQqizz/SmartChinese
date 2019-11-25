using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AbhsChinese.Admin.Models.Course
{
    [JsonSerializeWithPrefix("Ycs")]
    public class CourseViewModel
    {
        public int CourseType { get; set; }
        public string CourseTypeText
        {
            get { return CustomEnumHelper.Parse(typeof(CourseCategoryEnum), CourseType); }
        }
        public string Description { get; set; }
        public int Grade { get; set; }
        public string GradeText { get { return CustomEnumHelper.Parse(typeof(GradeEnum), Grade); } }

        public int Id { get; set; }
        public int LessonCount { get; set; }
        public int ApprovalLesCount { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public int Status { get; set; }
        public string StatusText
        {
            get { return CustomEnumHelper.Parse(typeof(CourseStatusEnum), Status); }
        }
        /// <summary>
        /// 课程封面
        /// </summary>
        public string CoverImage { get; set; }
        /// <summary>
        /// 课程封面
        /// </summary>
        public string CoverImageFull
        {
            get
            {
                if (CoverImage.HasValue())
                {
                    return ConfigurationManager.AppSettings["OssHostUrl"] + CoverImage;
                }
                else
                {
                    return CoverImage;
                }
            }
        }
        public Dictionary<string, decimal> Price { get; set; }
    }
}