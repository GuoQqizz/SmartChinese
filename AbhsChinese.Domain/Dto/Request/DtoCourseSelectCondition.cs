using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoCourseSelectCondition 
    {
        public int StudentId
        {
            set;get;
        }

        private int schoolLevel;
        public int SchoolLevel
        {
            get
            {
                return schoolLevel;
            }
        }

        private int schoolId;
        public int SchoolId
        {
            get
            {
                return schoolId;
            }
        }

        public int Grade
        {
            set; get;
        }

        public int CourseType
        {
            set; get;
        }

        public DtoCourseSelectOrderEnum OrderBy
        {
            set;get;
        }

        public string GradeExpression
        {
            get
            {
                if (Grade > 0)
                {
                    return "and ycs_grade = @Grade";
                }
                else
                {
                    return "";
                }
            }
        }

        public string CourseTypeExpression
        {
            get
            {
                if (CourseType > 0)
                {
                    return "and ycs_coursetype = @CourseType";
                }
                else
                {
                    return "";
                }
            }
        }

        public void SetSchoolLevel(int level)
        {
            schoolLevel = level;
        }

        public void SetSchoolId(int schoolId)
        {
            this.schoolId = schoolId;
        }

        public string OrderExpression
        {
            get
            {
                if (OrderBy == DtoCourseSelectOrderEnum.Lastest)
                {
                    return "Ycs_PublishTime desc";
                }
                else if (OrderBy == DtoCourseSelectOrderEnum.SellCount)
                {
                    return "Ycs_SellCount desc";
                }
                else
                {
                    return "Ycs_Id asc";
                }
            }
        }

        public enum DtoCourseSelectOrderEnum
        {
            Default = 1,
            Lastest = 2,
            SellCount = 3
        }
    }
}
