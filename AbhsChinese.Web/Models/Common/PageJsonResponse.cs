using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Common
{
    public class PageJsonResponse<T> where T : class, new()
    {
        public T Data { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}