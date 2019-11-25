using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Knowledge
{
    public class KnowledgeInputModel
    {
        public int Ykl_Id { get; set; }
        
        public string Ykl_Name { get; set; }

        public int Ykl_ParentId { get; set; }
         
        public int Ykl_Level { get; set; }

        public string Ykl_Keywords { get; set; }

        public int Ykl_ResourceId { get; set; }

        public int ResourceId { get; set; }

        public int Ykl_Creator { get; set; }

        public int Ykl_Editor { get; set; }
    }
}