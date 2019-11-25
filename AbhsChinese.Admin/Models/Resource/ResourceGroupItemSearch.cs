using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceGroupItemSearch //:Search
    {
        public int Id { get; set; }

        public int ResourceType { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}