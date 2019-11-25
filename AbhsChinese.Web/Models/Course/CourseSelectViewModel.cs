using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Course
{
    public class CourseSelectViewModel
    {
        public int StudentId { get; set; }
        public string SearchInfo { get; set; }
        public List<CourseSelectAdvertisingViewModel> AdvertisingList { get; set; }
    }
    public class CourseSelectAdvertisingViewModel
    {
        public string ImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public int Ad_Id { set; get; }
        public int CourseId { set; get; }
    }
}