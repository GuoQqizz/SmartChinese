using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceToCourseSearch:Search
    {
        public int CourseId { get; set; }

        public int MediaType { get; set; }

        public int TextType { get; set; }

        public int SubjectType { get; set; }

        public string NameOrKey { get; set; }
    }
}