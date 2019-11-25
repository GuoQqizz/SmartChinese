using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Course
{
    public class CourseSearch
    {
        public PagingObject Pagination { get; set; } = new PagingObject(1, 9);

        public bool HasMore { get; set; }

        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.GradeEnum"/>
        /// </summary>
        public int Grade { set; get; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.CourseCategoryEnum"/> 
        /// </summary>
        public int CourseType { set; get; }

        /// <summary>
        /// <see cref="AbhsChinese.Domain.Dto.Request.DtoCourseSelectCondition.DtoCourseSelectOrderEnum"/>
        /// </summary>
        public int OrderBy { set; get; }
    }
}