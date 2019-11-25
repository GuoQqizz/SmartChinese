using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoKnowledgeRequest
    {
        public int Ykl_Id { get; set; }

        public string Ykl_Name { get; set; }

        public int Ykl_ParentId { get; set; }

        public int Ykl_Level { get; set; }

        public string Ykl_Keywords { get; set; }

        public int Ykl_ResourceId { get; set; }
        
        public int Ykl_Creator { get; set; }

        public int Ykl_Editor { get; set; }
    }
}
