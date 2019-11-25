using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Knowledge
{
    public class KnowledgeTree
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int ChildId { get; set; }
    }
}