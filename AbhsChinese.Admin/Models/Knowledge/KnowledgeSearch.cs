using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbhsChinese.Admin.Models.Common;

namespace AbhsChinese.Admin.Models.Knowledge
{
    public class KnowledgeSearch:Search
    {
        public int Id { get; set; }

        public string NameOrKey { get; set; }

        public int KnowledgeType { get; set; }
    }
}