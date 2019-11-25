using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoSelectCenterResult
    {
        public int CourseId
        {
            set; get;
        }

        public string CourseName
        {
            set; get;
        }

        public string CourseImage
        {
            set; get;
        }

        public decimal CoursePrice
        {
            set; get;
        }

        public int SellCount
        {
            set; get;
        }

        public int Grade
        {
            set; get;
        }

        public int CourseType
        {
            set; get;
        }

        public int LessonCount
        {
            set; get;
        }

        public decimal BaseVoucherPrice
        {
            set; get;
        }

        public decimal SchoolVoucherPrice
        {
            set; get;
        }
    }
}
